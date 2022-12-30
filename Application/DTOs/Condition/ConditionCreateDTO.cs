using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Condition
{
    public class ConditionCreateDTO
    {
        public Inequality Inequality { get; set; }
        public float Value { get; set; }
        public int SensorTypeId { get; set; }
    }
}
