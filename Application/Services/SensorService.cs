using Application.DTOs.Sensor;
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
    public class SensorService : ISensorService
    {
        private readonly IMapper _mapper;
        private readonly ISensorRepository _repository;

        public SensorService(IMapper mapper, ISensorRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<int> Create(SensorCreateDTO sensorCreateDTO)
        {
            Sensor sensor = _mapper.Map<Sensor>(sensorCreateDTO);

            await _repository.AddAsync(sensor);

            return sensor.Id;
        }

        public async Task<SensorBaseDTO> GetById(int id)
        {
            Sensor? sensor = await _repository.GetByIdAsync(id);
            if(sensor is null)
            {
                throw new ObjectNotFoundException("Sensor does not exists");
            }

            return _mapper.Map<SensorBaseDTO>(sensor);
        }

        public async Task<IEnumerable<SensorBaseDTO>> GetAll()
        {
            IEnumerable<Sensor> sensors = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<SensorBaseDTO>>(sensors);
        }

        public async Task Remove(int id)
        {
            Sensor? sensor = await _repository.GetByIdAsync(id);
            if (sensor is null)
            {
                throw new ObjectNotFoundException("Sensor does not exists");
            }

            _repository.Delete(sensor);
            await _repository.SaveAsync();
        }
    }
}
