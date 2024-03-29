﻿using Core.Entities.Base;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Notification: BaseEntity
    {
        public DateTime CreatedTime { get; set; }
        public NotificationStatus Status { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }   
        public string UserId { get; set; }
    }
}
