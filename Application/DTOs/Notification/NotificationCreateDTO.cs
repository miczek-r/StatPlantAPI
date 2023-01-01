using Core.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Notification
{
    public class NotificationCreateDTO
    {
        public string UserId { get; set; }
        [SwaggerSchema("The notification title", Nullable = false)]
        public string Title { get; set; }
        [SwaggerSchema("The notification text", Nullable = false)]
        public string Text { get; set; }
    }
}
