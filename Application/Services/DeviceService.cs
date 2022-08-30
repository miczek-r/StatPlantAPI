using Application.DTOs.Device;
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
    public class DeviceService : IDeviceService
    {
        private readonly IMapper _mapper;
        private readonly IDeviceRepository _repository;

        public DeviceService(IMapper mapper, IDeviceRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<int> Create(DeviceCreateDTO deviceCreateDTO)
        {
            Device device = _mapper.Map<Device>(deviceCreateDTO);
            await _repository.AddAsync(device);
            await _repository.SaveAsync();

            return device.Id;
        }

        public async Task<IEnumerable<DeviceBaseDTO>> GetAll()
        {
            IEnumerable<Device> devices = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<DeviceBaseDTO>>(devices);
        }

        public async Task<DeviceBaseDTO> GetById(int id)
        {
            Device? device = await _repository.GetByIdAsync(id);
            if(device is null)
            {
                throw new ObjectNotFoundException("Device does not exists");
            }
            return _mapper.Map<DeviceBaseDTO>(device);
        }

        public async Task Remove(int id)
        {
            Device? device = await _repository.GetByIdAsync(id);

            if (device is null)
            {
                throw new ObjectNotFoundException("Device does not exists");
            }

            _repository.Delete(device);
            await _repository.SaveAsync();
        }
    }
}
