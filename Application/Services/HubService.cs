using Application.DTOs.Hub;
using Application.Exceptions;
using Application.IServices;
using AutoMapper;
using Core.Entities;
using Core.IRepositories;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        private readonly IHashingService _hashingService;

        public HubService(IMapper mapper, IHttpContextAccessor contextAccessor, IHubRepository repository, UserManager<User> userManager, IHashingService hashingService)
        {
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _repository = repository;
            _userManager = userManager;
            _hashingService = hashingService;   
        }

        public async Task<int> Create(HubCreateDTO hubCreateDTO)
        {
            Hub hub = _mapper.Map<Hub>(hubCreateDTO);

            hub.Password = _hashingService.HashPassword(hubCreateDTO.Password);

            await _repository.AddAsync(hub);
            await _repository.SaveAsync();

            return hub.Id;
        }

        public async Task Join(HubJoinDTO hubJoinDTO)
        {
            string? userId = GetCurrentUserId();
            if (userId is null)
            {
                throw new AccessForbiddenException("You must be logged in");
            }
            User? user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new ObjectNotFoundException("Your id does not match any user");
            }
            Hub? hub = await _repository.GetBySpecAsync(new HubSpecification(hub => hub.MacAddress == hubJoinDTO.MacAddress));
            if(hub is null)
            {
                throw new ObjectNotFoundException("Hub with provided mac address does not exists");
            }
            if(hub.Users.Any(user=> user.Id == userId))
            {
                throw new ObjectAlreadyExistsException("You are already a part of this hub");
            }
            if(!_hashingService.VerifyPassword(hubJoinDTO.Password, hub.Password))
            {
                throw new AccessForbiddenException("You passed wrong password for this hub");
            }
            hub.Users.Add(user);
            _repository.Update(hub);
            await _repository.SaveAsync();
        }

        public async Task Leave(int hubId)
        {
            string? userId = GetCurrentUserId();
            if (userId is null)
            {
                throw new AccessForbiddenException("You must be logged in");
            }
            User? user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new ObjectNotFoundException("Your id does not match any user");
            }
            Hub? hub = await _repository.GetBySpecAsync(new HubSpecification(hub => hub.Id == hubId));
            if (hub is null)
            {
                throw new ObjectNotFoundException("Hub with provided mac address does not exists");
            }
            if (!hub.Users.Any(user => user.Id == userId))
            {
                throw new ObjectAlreadyExistsException("You are already not a part of this hub");
            }
            hub.Users.Remove(user);
            _repository.Update(hub);
            await _repository.SaveAsync();
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
