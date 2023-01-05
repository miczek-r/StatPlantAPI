using Application.DTOs.Device;
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
        private readonly ISensorTypeRepository _sensorTypeRepository;
        private readonly IDeviceService _deviceService;

        public SensorDataService(IMapper mapper, ISensorDataRepository repository, ISensorRepository sensorRepository, IDeviceRepository deviceRepository, ISensorTypeRepository sensorTypeRepository, IDeviceService deviceService)
        {
            _mapper = mapper;
            _repository = repository;
            _sensorRepository = sensorRepository;
            _deviceRepository = deviceRepository;
            _deviceService = deviceService;
            _sensorTypeRepository = sensorTypeRepository;
        }

        public async Task Create(SensorDataCreateDTO sensorCreateDTO)
        {
            foreach (var singleSensorData in sensorCreateDTO.Data){
            Sensor sensor = await _sensorRepository.GetBySpecAsync(new SensorSpecification(x => x.SensorType.TypeName == singleSensorData.Key));
                Device? device = await _deviceRepository.GetBySpecAsync(new DeviceSpecification(x => x.UUID == sensorCreateDTO.Id));
                if(device is null)
                {
                    int deviceId = await _deviceService.Create(new DeviceCreateDTO() { Name="New Device", UUID = sensorCreateDTO.Id, HubMacAddress = sensorCreateDTO.MacAddress });
                    device = await _deviceRepository.GetByIdAsync(deviceId);
                }
                SensorData sensorData = new()
                {
                    DateOfMeasurement = DateTime.UtcNow,
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

        public async Task<SensorDataDetailsDTO> GetDetails(SensorDataGetDetailsDTO sensorDataGetDetailsDTO)
        {
            Device? device = await _deviceRepository.GetByIdAsync(sensorDataGetDetailsDTO.DeviceId);
            if(device is null)
            {
                throw new ObjectNotFoundException("This device does not exists");
            }
            SensorType? sensorType = await _sensorTypeRepository.GetByIdAsync(sensorDataGetDetailsDTO.SensorType);
            if(sensorType is null)
            {
                throw new ObjectNotFoundException("This sensor type does not exists");
            }
            IEnumerable<SensorData> sensorData = await _repository.GetByLambdaAsync((data) => sensorDataGetDetailsDTO.StartDate <= data.DateOfMeasurement && sensorDataGetDetailsDTO.EndDate >= data.DateOfMeasurement && data.Device.Id == sensorDataGetDetailsDTO.DeviceId && data.Sensor.SensorType.Id == sensorDataGetDetailsDTO.SensorType);
            SensorDataDetailsDTO result = new()
            {
                SensorDataRecords = _mapper.Map<IEnumerable<SensorDataRecordDTO>>(sensorData).ToList(),
                TypeOfSensor = sensorType.TypeName,
                MeasurementUnit = sensorType.Unit,
                DeviceInformations = $"{device.Name} - {device.UUID}",
                LatestRecordedValue = _mapper.Map<SensorDataRecordDTO>((await _repository.GetByLambdaAsync((data) => data.Device.Id == sensorDataGetDetailsDTO.DeviceId && data.Sensor.SensorType.Id == sensorDataGetDetailsDTO.SensorType)).OrderByDescending(record => record.DateOfMeasurement).First())
            };
            return result;
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
