using Application.DTOs.Trigger;
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
    public class TriggerService : ITriggerService
    {
        private readonly IMapper _mapper;
        private readonly ITriggerRepository _repository;

        public TriggerService(IMapper mapper, ITriggerRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<int> Create(TriggerCreateDTO triggerCreateDTO)
        {
            Trigger trigger = _mapper.Map<Trigger>(triggerCreateDTO);
            await _repository.AddAsync(trigger);
            await _repository.SaveAsync();

            return trigger.Id;
        }

        public async Task<TriggerBaseDTO> Get(int triggerId)
        {
            Trigger? trigger = await _repository.GetByIdAsync(triggerId);
            if (trigger is null)
            {
                throw new ObjectNotFoundException("Trigger does not exists");
            }
            return _mapper.Map<TriggerBaseDTO>(trigger);
        }

        public async Task<IEnumerable<TriggerLiteDTO>> GetAll(int deviceId)
        {
            IEnumerable<Trigger> triggers = await _repository.GetByLambdaAsync(x => x.DeviceId == deviceId);

            return _mapper.Map<IEnumerable<TriggerLiteDTO>>(triggers);
        }

        public async Task Remove(int triggerId)
        {
            Trigger? trigger = await _repository.GetByIdAsync(triggerId);

            if(trigger is null)
            {
                throw new ObjectNotFoundException("Trigger does not exists");
            }

            _repository.Delete(trigger);
            await _repository.SaveAsync();
        }

        public async Task Update(TriggerUpdateDTO triggerUpdateDTO)
        {
            Trigger? trigger = await _repository.GetByIdAsync(triggerUpdateDTO.Id);

            if(trigger is null)
            {
                throw new ObjectNotFoundException("Trigger does not exists");
            }
            _repository.Update(_mapper.Map<Trigger>(triggerUpdateDTO));
            await _repository.SaveAsync();
        }
    }
}
