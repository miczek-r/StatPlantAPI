using Application.DTOs.Device;
using Application.Exceptions;
using Application.IServices;
using AutoMapper;
using Core.Entities;
using Core.IRepositories;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDeviceRepository _repository;

        public DeviceService(IMapper mapper, IHttpContextAccessor contextAccessor, IDeviceRepository repository)
        {
            _mapper = mapper;
            _contextAccessor = contextAccessor;
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
            string? userId = GetCurrentUserId();
            if (userId is null)
            {
                throw new AccessForbiddenException("You must be logged in");
            }
            Device? device = await _repository.GetBySpecAsync(new DeviceSpecification(x => x.Id == id));

            if (device is null)
            {
                throw new ObjectNotFoundException("Hub does not exists");
            }
            if (!device.Hub.Users.Any(x => x.Id == userId))
            {
                throw new AccessForbiddenException("You do not have access to this hub");
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

        private string? GetCurrentUserId()
        {
            var identity = (ClaimsIdentity?)_contextAccessor.HttpContext?.User.Identity;
            string? userId = null;
            if (identity is not null && identity.IsAuthenticated)
            {
                userId = identity.Claims?.FirstOrDefault(
                    x => x.Type.Contains("nameidentifier")
                    )?.Value;
            }
            return userId;
        }
    }
}
