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
        [SwaggerSchema("The notification creation time", Format = "date-time")]
        public DateTime CreatedTime { get; set; }
        [SwaggerSchema("The notification status", Nullable = false)]
        public NotificationStatus Name { get; set; }
        [SwaggerSchema("The notification title", Nullable = false)]
        public string Title { get; set; }
        [SwaggerSchema("The notification text", Nullable = false)]
        public string Text { get; set; }
    }
}
