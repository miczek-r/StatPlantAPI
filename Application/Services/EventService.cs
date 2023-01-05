using Application.DTOs.Event;
using Application.DTOs.Notification;
using Application.Exceptions;
using Application.IServices;
using AutoMapper;
using Core.Entities;
using Core.IRepositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EventService : IEventService
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _repository;
        private readonly INotificationService _notificationService;
        private readonly IHttpContextAccessor _contextAccessor;
        public EventService(IMapper mapper, IEventRepository repository, INotificationService notificationService, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _repository = repository;
            _notificationService = notificationService;
            _contextAccessor = contextAccessor;
        }

        public async Task<int> Create(EventCreateDTO eventCreateDTO)
        {
            Event eventObj = _mapper.Map<Event>(eventCreateDTO);
            string? userId = GetCurrentUserId();
            if (userId is null)
            {
                throw new AccessForbiddenException("You must be logged in");
            }
            if (eventObj.EndTime.HasValue && eventObj.StartTime >= eventObj.EndTime.Value)
            {
                throw new ObjectValidationException("End Time must be in future of Start Time");
            }
            if (eventObj.StartTime < DateTime.Now)
            {
                throw new ObjectValidationException("Past Events can't be edited");
            }
            eventObj.UserId = userId;

            await _repository.AddAsync(eventObj);
            await _repository.SaveAsync();

            return eventObj.Id;
        }

        public async Task<EventBaseDTO> Get(int eventId)
        {
            
            Event? eventObj = await _repository.GetByIdAsync(eventId); 
            string? userId = GetCurrentUserId();
            if (userId is null)
            {
                throw new AccessForbiddenException("You must be logged in");
            }
            if (eventObj is null)
            {
                throw new ObjectNotFoundException("Event does not exists");
            }
            if (eventObj.UserId != userId)
            {
                throw new AccessForbiddenException("You dont have access to this event");
            }
            return _mapper.Map<EventBaseDTO>(eventObj);
        }

        public async Task<IEnumerable<EventLiteDTO>> GetEventsForMonth(EventGetByDateDTO getByDateDTO)
        {
            string? userId = GetCurrentUserId();
            if (userId is null)
            {
                throw new AccessForbiddenException("You must be logged in");
            }
            IEnumerable<Event> events = await _repository.GetByLambdaAsync(x => x.UserId == userId && (x.StartTime.Year == getByDateDTO.Date.Year && x.StartTime.Month == getByDateDTO.Date.Month || (x.EndTime.HasValue && x.EndTime>= getByDateDTO.Date && x.StartTime<= getByDateDTO.Date)));

            return _mapper.Map<IEnumerable<EventLiteDTO>>(events);
        }
        public async Task<IEnumerable<EventBaseDTO>> GetEventsForDay(EventGetByDateDTO getByDateDTO)
        {
            string? userId = GetCurrentUserId();
            if (userId is null)
            {
                throw new AccessForbiddenException("You must be logged in");
            }
            IEnumerable<Event> events = await _repository.GetByLambdaAsync(x => x.UserId == userId && (x.StartTime.Date == getByDateDTO.Date.Date || (x.EndTime.HasValue && x.EndTime >= getByDateDTO.Date && x.StartTime <= getByDateDTO.Date)));
            return _mapper.Map<IEnumerable<EventBaseDTO>>(events);
        }

        public async Task Remove(int eventId)
        {
            Event? eventObj = await _repository.GetByIdAsync(eventId);
            string? userId = GetCurrentUserId();
            if (userId is null)
            {
                throw new AccessForbiddenException("You must be logged in");
            }
            if (eventObj is null)
            {
                throw new ObjectNotFoundException("Event does not exists");
            }
            if (eventObj.UserId != userId)
            {
                throw new AccessForbiddenException("You dont have access to this event");
            }

            _repository.Delete(eventObj);
            await _repository.SaveAsync();
        }

        public async Task Update(EventBaseDTO eventUpdateDTO)
        {
            Event? eventObj = await _repository.GetByIdAsync(eventUpdateDTO.Id);
            string? userId = GetCurrentUserId();
            if (userId is null)
            {
                throw new AccessForbiddenException("You must be logged in");
            }
            if (eventObj is null)
            {
                throw new ObjectNotFoundException("Event does not exists");
            }
            if (eventObj.UserId != userId)
            {
                throw new AccessForbiddenException("You dont have access to this event");
            }
            if(eventObj.EndTime.HasValue && eventObj.StartTime >= eventObj.EndTime.Value)
            {
                throw new ObjectValidationException("End Time must be in future of Start Time");
            }
            if (eventObj.StartTime < DateTime.Now)
            {
                throw new ObjectValidationException("Past Events can't be edited");
            }

            eventObj.Title = eventUpdateDTO.Title;
            eventObj.Details = eventUpdateDTO.Details;
            eventObj.StartTime = eventUpdateDTO.StartTime;
            eventObj.EndTime = eventUpdateDTO.EndTime;

            _repository.Update(eventObj);
            await _repository.SaveAsync();
        }

        public async Task CheckAllEvents()
        {
            IEnumerable<Event> events = await _repository.GetByLambdaAsync(x => (!x.IsNotificationSent && x.StartTime < DateTime.Now));
            foreach (Event eventObj in events)
            {
                NotificationCreateDTO notification = new() { Text = $"The event: {eventObj.Title} has started", Title = $"{eventObj.Title} has started", UserId = eventObj.UserId };
                await _notificationService.Create(notification);
                eventObj.IsNotificationSent = true;
                _repository.Update(eventObj);
                await _repository.SaveAsync();
            }
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
