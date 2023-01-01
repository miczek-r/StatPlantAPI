using Application.DTOs.Notification;
using Application.DTOs.Trigger;
using Application.Exceptions;
using Application.IServices;
using AutoMapper;
using Core.Entities;
using Core.IRepositories;
using Core.Specifications;
using Core.Specifications.Base;
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
        private readonly ISensorDataRepository _sensorDataRepository;
        private readonly INotificationService _notificationService;

        public TriggerService(IMapper mapper, ITriggerRepository repository, ISensorDataRepository sensorDataRepository, INotificationService notificationService)
        {
            _mapper = mapper;
            _repository = repository;
            _sensorDataRepository = sensorDataRepository;
            _notificationService = notificationService;
        }

        public async Task<int> Create(TriggerCreateDTO triggerCreateDTO)
        {
            Trigger trigger = _mapper.Map<Trigger>(triggerCreateDTO);
            trigger.IsActive = false;
            trigger.HasBeenCalled = false;
            await _repository.AddAsync(trigger);
            await _repository.SaveAsync();

            return trigger.Id;
        }

        public async Task<TriggerBaseDTO> Get(int triggerId)
        {
            Trigger? trigger = await _repository.GetBySpecAsync(new TriggerSpecification(x => x.Id == triggerId));
            if (trigger is null)
            {
                throw new ObjectNotFoundException("Trigger does not exists");
            }
            return _mapper.Map<TriggerBaseDTO>(trigger);
        }

        public async Task<IEnumerable<TriggerLiteDTO>> GetAll(int deviceId)
        {
            IEnumerable<Trigger> triggers = await _repository.GetAllBySpecAsync(new TriggerSpecification(x => x.DeviceId == deviceId));

            return _mapper.Map<IEnumerable<TriggerLiteDTO>>(triggers);
        }

        public async Task Remove(int triggerId)
        {
            Trigger? trigger = await _repository.GetByIdAsync(triggerId);

            if (trigger is null)
            {
                throw new ObjectNotFoundException("Trigger does not exists");
            }

            _repository.Delete(trigger);
            await _repository.SaveAsync();
        }

        public async Task Update(TriggerUpdateDTO triggerUpdateDTO)
        {
            Trigger? trigger = await _repository.GetBySpecAsync(new TriggerSpecificationFull(x=> x.Id == triggerUpdateDTO.Id));

            if (trigger is null)
            {
                throw new ObjectNotFoundException("Trigger does not exists");
            }
            trigger.Name = triggerUpdateDTO.Name;
            trigger.ApiUrl = triggerUpdateDTO.ApiUrl;
            trigger.NotificationText = triggerUpdateDTO.NotificationText;
            trigger.IsActive = triggerUpdateDTO.IsActive;
            trigger.TriggerType = triggerUpdateDTO.TriggerType;
            trigger.HasBeenCalled = false;
            foreach(var condition in triggerUpdateDTO.Conditions)
            {
                if (condition.Id == 0 || !trigger.Conditions.Any(item => item.Id == condition.Id))
                    trigger.Conditions.Add(_mapper.Map<Condition>(condition));
            }
            for(int i = trigger.Conditions.Count-1; i>=0;i--)
                if (!triggerUpdateDTO.Conditions.Any(item => item.Id == trigger.Conditions[i].Id))
                    trigger.Conditions.Remove(trigger.Conditions[i]);
            _repository.Update(trigger);
            await _repository.SaveAsync();
        }

        public async Task CheckAllTriggers()
        {
            IEnumerable<Trigger> triggers = await _repository.GetAllBySpecAsync(new TriggerSpecificationFull(x => x.IsActive));
            foreach (Trigger trigger in triggers)
            {
                if (!await CheckAllConditions(trigger)) {
                    trigger.HasBeenCalled = false;
                    _repository.Update(trigger);
                    await _repository.SaveAsync();
                    continue;
                }

                if (trigger.HasBeenCalled) continue;

                switch (trigger.TriggerType)
                {
                    case Core.Enums.TriggerType.API_CALL:
                        //TODO: Implement
                        break;
                    case Core.Enums.TriggerType.NOTIFICATION:
                        await AddNotifications(trigger.Device.Hub.Users, trigger.Name, trigger.NotificationText);
                        break;
                    case Core.Enums.TriggerType.BOTH:
                        //TODO: Implement
                        await AddNotifications(trigger.Device.Hub.Users, trigger.Name, trigger.NotificationText);
                        break;
                }

                trigger.HasBeenCalled = true;
                _repository.Update(trigger);
                await _repository.SaveAsync();
            }
        }

        private async Task<bool> CheckAllConditions(Trigger trigger)
        {
            foreach (Condition condition in trigger.Conditions)
            {
                SensorData sensorData = (await _sensorDataRepository.GetByLambdaAsync((data) => data.Device.Id == trigger.DeviceId && data.Sensor.SensorType.Id == condition.SensorTypeId)).OrderByDescending(record => record.DateOfMeasurement).First();
                switch (condition.Inequality)
                {
                    case Core.Enums.Inequality.LOWER:
                        if (sensorData.Value >= condition.Value) return false;
                        break;
                    case Core.Enums.Inequality.HIGHER:
                        if (sensorData.Value <= condition.Value) return false;
                        break;
                    case Core.Enums.Inequality.EQUAL:
                        if (sensorData.Value != condition.Value) return false;
                        break;
                }
            }
            return true;
        }

        private async Task AddNotifications(List<User> users, string triggerName, string notificationText)
        {
            foreach (User user in users)
            {
                NotificationCreateDTO notification = new() { Text = notificationText, Title = $"{triggerName} has been called", UserId = user.Id };
                await _notificationService.Create(notification);
            }
        }
    }
}
