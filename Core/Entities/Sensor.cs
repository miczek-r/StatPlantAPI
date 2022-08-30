using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Sensor : BaseEntity
    {
        public string SensorName { get; set; }
        public SensorType SensorType { get; set; }
    }
}
