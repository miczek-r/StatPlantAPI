using Core.IRepositories.Base;
using Core.Specifications.Base;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IdentityDbContext _dbContext;

        public Repository(IdentityDbContext dbContext) => _dbContext = dbContext;

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);

        public async Task<IReadOnlyList<T>> GetAllAsync() => await _dbContext.Set<T>().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllBySpecAsync(ISpecification<T> spec)
        {
            IQueryable<T> secondaryResult = AddSpecIncludes(spec);

            return (spec.IgnoreQueryFilter)
                ? await secondaryResult.IgnoreQueryFilters().Where(spec.Criteria).ToListAsync()
                : await secondaryResult.Where(spec.Criteria).ToListAsync();
        }



        public async Task<T?> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

        public async Task<IReadOnlyList<T>> GetByLambdaAsync(Expression<Func<T, bool>> predicate) => await _dbContext.Set<T>().Where(predicate).ToListAsync();

        public async Task<T?> GetBySpecAsync(ISpecification<T> spec)
        {
            IQueryable<T> secondaryResult = AddSpecIncludes(spec);

            return (spec.IgnoreQueryFilter)
                ? await secondaryResult.IgnoreQueryFilters().Where(spec.Criteria).SingleOrDefaultAsync()
                : await secondaryResult.Where(spec.Criteria).SingleOrDefaultAsync();
        }

        public void Update(T entity) => _dbContext.Entry(entity).State = EntityState.Modified;

        public async Task SaveAsync() => await _dbContext.SaveChangesAsync();


        private IQueryable<T> AddSpecIncludes(ISpecification<T> spec)
        {
            IQueryable<T> queryableResultWithIncludes = spec.Includes.Aggregate(
                            _dbContext.Set<T>().AsQueryable(),
                            (current, include) => current.Include(include));

            IQueryable<T> secondaryResult = spec.IncludeStrings.Aggregate(
                queryableResultWithIncludes,
                (current, include) => current.Include(include));
            return secondaryResult;
        }
    }
}
