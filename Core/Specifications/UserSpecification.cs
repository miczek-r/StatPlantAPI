﻿using Core.Entities;
using Core.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class UserSpecification: BaseSpecification<User>
    {
        public UserSpecification(Expression<Func<User,bool>> criteria): base(criteria)
        {

        }
    }
}
