using Application.DTOs.Hub;
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
    public class HubService : IHubService
    {
        private readonly IMapper _mapper;
        private readonly IHubRepository _repository;

        public HubService(IMapper mapper, IHubRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<int> Create(HubCreateDTO hubCreateDTO)
        {
            Hub hub = _mapper.Map<Hub>(hubCreateDTO);

            await _repository.AddAsync(hub);
            await _repository.SaveAsync();

            return hub.Id;
        }

        public async Task<IEnumerable<HubBaseDTO>> GetAll()
        {
            IEnumerable<Hub> hubs = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<HubBaseDTO>>(hubs);
        }

        public async Task<HubBaseDTO> GetById(int id)
        {
            Hub? hub = await _repository.GetByIdAsync(id);

            if (hub is null)
            {
                throw new ObjectNotFoundException("Hub does not exists");
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
    }
}
