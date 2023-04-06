using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesSystem.Core.Repositories;

namespace SalesSystem.Data.Repositories
{
    public class Repository<TEntity> : IRepositoryGeneral<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        public Repository(DbContext context)
        {
            this.Context = context;
        }
        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }
        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }        
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return  await Context.Set<TEntity>().ToListAsync();
        }
        public ValueTask<TEntity> GetByIdAsync(int id)
        {
            return Context.Set<TEntity>().FindAsync(id);
        }
        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
        public async Task<bool> UpdateAsync(TEntity entity, TEntity updated)
        {
            if (entity == null || updated == null)
            {
                return false;
            }

            var entry = Context.Entry(entity);
            object[] keyParts = entry.Metadata.FindPrimaryKey()
                         .Properties
                         .Select(p => entry.Property(p.Name).CurrentValue)
                         .ToArray();
            TEntity existing = Context.Set<TEntity>().Find(keyParts);            
            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(updated);
            }
            else
            {
                await Context.Set<TEntity>().AddAsync(entity);
            }
            Context.SaveChanges();
            return true;
        }
    }
}