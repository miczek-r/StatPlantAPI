using Core.Entities;
using Core.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class HubSpecification : BaseSpecification<Hub>
    {
        public HubSpecification(Expression<Func<Hub, bool>> criteria)
        : base(criteria)
        {
            AddInclude(x => x.Devices);
            AddInclude(x => x.Users);
            AddInclude($"{nameof(Hub.Devices)}.{nameof(Device.SensorData)}");
            AddInclude($"{nameof(Hub.Devices)}.{nameof(Device.SensorData)}.{nameof(SensorData.Sensor)}");
        }
    }
}
