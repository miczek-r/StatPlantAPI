using Application.DTOs.Hub;
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
    public class HubService : IHubService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IHubRepository _repository;

        public HubService(IMapper mapper, IHttpContextAccessor contextAccessor, IHubRepository repository)
        {
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _repository = repository;
        }

        public async Task<int> Create(HubCreateDTO hubCreateDTO)
        {
            Hub hub = _mapper.Map<Hub>(hubCreateDTO);

            await _repository.AddAsync(hub);
            await _repository.SaveAsync();

            return hub.Id;
        }

        public async Task<IEnumerable<HubLiteDTO>> GetAll()
        {
            string? userId = GetCurrentUserId();
            if (userId is null)
            {
                throw new AccessForbiddenException("You must be logged in");
            }
            IEnumerable<Hub> hubs = await _repository.GetAllBySpecAsync(new HubSpecification(x => x.Users.Any(y=> y.Id == userId)));
            return _mapper.Map<IEnumerable<HubLiteDTO>>(hubs);
        }

        public async Task<HubBaseDTO> GetById(int id)
        {
            string? userId = GetCurrentUserId();
            if (userId is null)
            {
                throw new AccessForbiddenException("You must be logged in");
            }
            Hub? hub = await _repository.GetBySpecAsync(new HubSpecification(x => x.Id == id ));

            if (hub is null)
            {
                throw new ObjectNotFoundException("Hub does not exists");
            }
            if (!hub.Users.Any(x => x.Id == userId))
            {
                throw new AccessForbiddenException("You do not have access to this hub");
            }

            return _mapper.Map<HubBaseDTO>(hub);
        }

        public async Task Remove(int id)
        {
            Hub? hub = await _repository.GetByIdAsync(id);

            if (hub is null)
            {
                throw new ObjectNotFoundException("Hub does not exists");
            }

            _repository.Delete(hub);
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
