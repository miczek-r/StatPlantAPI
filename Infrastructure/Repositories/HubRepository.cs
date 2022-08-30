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
    public class HubRepository : Repository<Hub>, IHubRepository
    {
        public HubRepository(IdentityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
