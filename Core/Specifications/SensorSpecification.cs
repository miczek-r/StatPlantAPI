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
    public class SensorSpecification : BaseSpecification<Sensor>
    {
        public SensorSpecification(Expression<Func<Sensor, bool>> criteria)
        : base(criteria)
        {
            AddInclude(x => x.SensorType);
        }
    }
}
