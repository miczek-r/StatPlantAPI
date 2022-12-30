using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Device : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UUID { get; set; }
        public Hub Hub { get; set; }
        public List<SensorData> SensorData { get;set;}
        public List<Trigger> Triggers { get; set; }
    }
}
