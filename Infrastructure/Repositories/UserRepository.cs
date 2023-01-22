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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IdentityDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<User?> GetByIdAsync(int id)
        {
            return null;
        }
    }
}
