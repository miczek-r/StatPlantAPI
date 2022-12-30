using Application.DTOs.SensorType;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Condition
{
    public class ConditionBaseDTO
    {
        public int Id { get; set; }
        public Inequality Ineqality { get; set; }
        public SensorTypeBaseDTO SensorType { get; set; }
        public float Value { get; set; }
    }
}
