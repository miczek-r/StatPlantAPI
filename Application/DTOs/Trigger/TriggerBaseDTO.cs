using Application.DTOs.Condition;
using Application.DTOs.Device;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Trigger
{
    public class TriggerBaseDTO
    {
        public string Name { get; set; }
        public TriggerType TriggerType { get; set; }
        public Interval Interval { get; set; }
        public List<ConditionBaseDTO> Conditions { get; set; }
    }
}
