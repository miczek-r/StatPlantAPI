using Core.Entities;
using Core.IRepositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TriggerRepository : Repository<Trigger>, ITriggerRepository
    {
        public TriggerRepository(IdentityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
