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
    public class ConditionSpecification : BaseSpecification<Condition>
    {
        public ConditionSpecification(Expression<Func<Condition, bool>> criteria): base(criteria) { }
    }
}
