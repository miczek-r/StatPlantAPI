using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class SensorCondition : BaseEntity
    {
        public float ThresholdValue { get; set; }
        public bool IsTriggered { get; set; }
        public string ApiUrl { get; set; }
        public Device Device { get; set; }
        public int DeviceId { get; set; }
        public SensorType SensorType { get; set; }
        public int SensorTypeId { get; set; }
    }
}
