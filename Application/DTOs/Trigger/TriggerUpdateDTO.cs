﻿using Application.DTOs.Condition;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Trigger
{
    public class TriggerUpdateDTO 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TriggerType TriggerType { get; set; }
        public string? ApiUrl { get; set; }
        public string? NotificationText { get; set; }
        public List<ConditionUpdateDTO> Conditions { get; set; }
        public bool IsActive { get; set; }
        
    }
}
