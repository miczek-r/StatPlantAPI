using Application.DTOs.SensorData;
using Application.Exceptions;
using Application.IServices;
using AutoMapper;
using Core.Entities;
using Core.IRepositories;
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

        public SensorDataService(IMapper mapper, ISensorDataRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<int> Create(SensorDataCreateDTO sensorCreateDTO)
        {
            SensorData sensorData = _mapper.Map<SensorData>(sensorCreateDTO);

            await _repository.AddAsync(sensorData);
            await _repository.SaveAsync();

            return sensorData.Id;
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
