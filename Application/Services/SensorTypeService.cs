using Application.DTOs.SensorType;
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
    public class SensorTypeService : ISensorTypeService
    {
        private readonly IMapper _mapper;
        private readonly ISensorTypeRepository _repository;

        public SensorTypeService(IMapper mapper, ISensorTypeRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<int> Create(SensorTypeCreateDTO sensorCreateDTO)
        {
            SensorType sensorType = _mapper.Map<SensorType>(sensorCreateDTO);
            await _repository.AddAsync(sensorType);
            await _repository.SaveAsync();
            return sensorType.Id;
        }

        public async Task<IEnumerable<SensorTypeBaseDTO>> GetAll()
        {
            IEnumerable<SensorType> sensorTypes = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<SensorTypeBaseDTO>>(sensorTypes);
        }

        public async Task Remove(int id)
        {
            SensorType? sensorType = await _repository.GetByIdAsync(id);
            if(sensorType is null)
            {
                throw new ObjectNotFoundException("Sensor Type does not exists");
            }

            _repository.Delete(sensorType);
            await _repository.SaveAsync();
        }
    }
}
