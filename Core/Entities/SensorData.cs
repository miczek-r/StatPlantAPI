using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class SensorData : BaseEntity
    {
        public Device Device { get; set; }
        public Sensor Sensor { get; set; }
        public float Value { get; set; }
        public DateTime DateOfMeasurement { get; set; }
    }
}
