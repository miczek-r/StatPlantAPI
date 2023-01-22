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
    public class SensorDataSpecification : BaseSpecification<SensorData>
    {
        public SensorDataSpecification(Expression<Func<SensorData, bool>> criteria): base(criteria)
        {

        }
    }
}
