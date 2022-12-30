using Application.DTOs.Condition;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Trigger
{
    public class TriggerCreateDTO
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public TriggerType TriggerType { get; set; }    
        public Interval Interval { get; set; }
        public List<ConditionCreateDTO> Conditions { get; set; }
    }
}
