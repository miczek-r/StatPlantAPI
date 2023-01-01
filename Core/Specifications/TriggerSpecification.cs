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
    public class TriggerSpecification : BaseSpecification<Trigger>
    {
        public TriggerSpecification(Expression<Func<Trigger, bool>> criteria)
        : base(criteria)
        {
            AddInclude(x => x.Conditions);
        }
    }
    public class TriggerSpecificationFull : BaseSpecification<Trigger>
    {
        public TriggerSpecificationFull(Expression<Func<Trigger, bool>> criteria)
        : base(criteria)
        {
            AddInclude(x => x.Conditions);
            AddInclude(x => x.Device); 
            AddInclude($"{nameof(Trigger.Device)}.{nameof(Device.Hub)}");
            AddInclude($"{nameof(Trigger.Device)}.{nameof(Device.Hub)}.{nameof(Hub.Users)}");
        }
    }
}
