using Application.DTOs.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationBaseDTO>> GetAll();
        Task<NotificationBaseDTO> GetById(int id);
        Task<int> Create(NotificationCreateDTO notificationCreateDTO);
        Task Remove(int id);
    
    }
}
