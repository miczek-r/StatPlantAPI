using Application.DTOs.Notification;
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
    public class NotificationService : INotificationService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly INotificationRepository _repository;

        public NotificationService(IMapper mapper, IHttpContextAccessor contextAccessor, INotificationRepository repository)
        {
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _repository = repository;
        }

        public async Task<int> Create(NotificationCreateDTO notificationCreateDTO)
        {
            Notification notification = _mapper.Map<Notification>(notificationCreateDTO);

            await _repository.AddAsync(notification);
            await _repository.SaveAsync();

            return notification.Id;
        }

        public async Task<IEnumerable<NotificationBaseDTO>> GetAll()
        {
            string? userId = GetCurrentUserId();
            if (userId is null)
            {
                throw new AccessForbiddenException("You must be logged in");
            }
            IEnumerable<Notification> notifications = await _repository.GetAllBySpecAsync(new NotificationSpecification(x => x.UserId == userId));
            return _mapper.Map<IEnumerable<NotificationBaseDTO>>(notifications);
        }

        public async Task<NotificationBaseDTO> GetById(int id)
        {
            Notification? notification = await _repository.GetByIdAsync(id);
            if(notification is null)
            {
                throw new ObjectNotFoundException("Notification does not exists");
            }

            return _mapper.Map<NotificationBaseDTO>(notification);
        }

        public async Task Remove(int id)
        {
            Notification? notification = await _repository.GetByIdAsync(id);
            if (notification is null)
            {
                throw new ObjectNotFoundException("Notification does not exists");
            }

            _repository.Delete(notification);
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
