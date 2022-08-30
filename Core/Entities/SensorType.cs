using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class SensorType: BaseEntity
    {
        public string TypeName { get; set; }
        public string Unit { get; set; }

    }
}
