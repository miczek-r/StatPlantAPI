using Core.Entities.Base;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Trigger : BaseEntity
    {
        public string Name { get; set; }
        public TriggerType TriggerType { get; set; }
        public Device Device { get; set; }
        public int DeviceId { get; set; }
        public Interval Interval { get; set; }
        public List<Condition> Conditions { get; set; }
    }
}
