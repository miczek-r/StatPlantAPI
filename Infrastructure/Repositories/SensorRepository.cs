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
    public class SensorRepository : Repository<Sensor>, ISensorRepository
    {
        public SensorRepository(IdentityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
