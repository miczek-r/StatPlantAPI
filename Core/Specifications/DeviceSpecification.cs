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
    public class DeviceSpecification : BaseSpecification<Device>
    {
        public DeviceSpecification(Expression<Func<Device, bool>> criteria)
        : base(criteria)
        {
            AddInclude(x => x.Triggers);
            AddInclude(x => x.Hub);
            AddInclude(x => x.SensorData);
            AddInclude($"{nameof(Device.Hub)}.{nameof(Hub.Users)}");
            AddInclude($"{nameof(Device.SensorData)}.{nameof(SensorData.Sensor)}");
            AddInclude($"{nameof(Device.SensorData)}.{nameof(SensorData.Sensor)}.{nameof(Sensor.SensorType)}");
        }
    }
}
