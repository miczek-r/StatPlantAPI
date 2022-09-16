using Application.DTOs.SensorData;
using Application.Exceptions;
using Application.IServices;
using AutoMapper;
using Core.Entities;
using Core.IRepositories;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SensorDataService : ISensorDataService
    {
        private readonly IMapper _mapper;
        private readonly ISensorDataRepository _repository;
        private readonly ISensorRepository _sensorRepository;
        private readonly IDeviceRepository _deviceRepository;

        public SensorDataService(IMapper mapper, ISensorDataRepository repository, ISensorRepository sensorRepository, IDeviceRepository deviceRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _sensorRepository = sensorRepository;
            _deviceRepository = deviceRepository;
        }

        public async Task Create(SensorDataCreateDTO sensorCreateDTO)
        {
            foreach (var singleSensorData in sensorCreateDTO.Data){
            Sensor sensor = await _sensorRepository.GetBySpecAsync(new SensorSpecification(x => x.SensorType.TypeName == singleSensorData.Key));
                Device device = await _deviceRepository.GetByIdAsync(sensorCreateDTO.Id);
                SensorData sensorData = new()
                {
                    DateOfMeasurement = DateTime.Now,
                    Sensor = sensor,
                    Device = device,
                    Value = singleSensorData.Value
                };
                await _repository.AddAsync(sensorData);
            }

            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<SensorDataBaseDTO>> GetAll()
        {
            IEnumerable<SensorData> sensorData = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<SensorDataBaseDTO>>(sensorData);
        }

        public async Task Remove(int id)
        {
            SensorData? sensorData = await _repository.GetByIdAsync(id);

            if(sensorData is null)
            {
                throw new ObjectNotFoundException("Sensor Data not found");
            }

            _repository.Delete(sensorData);
            await _repository.SaveAsync();
        }
    }
}
