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
    public class SensorTypeRepository : Repository<SensorType>, ISensorTypeRepository
    {
        public SensorTypeRepository(IdentityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
