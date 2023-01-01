using Core.Entities.Base;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Condition : BaseEntity
    {
        public Inequality Inequality { get; set; }
        public SensorType SensorType { get; set; }
        public int SensorTypeId { get; set; }
        public float Value { get; set; }
        public int TriggerId { get; set; }
        public Trigger Trigger { get; set; }
    }
}
