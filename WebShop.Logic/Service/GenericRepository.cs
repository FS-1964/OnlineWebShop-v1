using System;
using System.Collections.Generic;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebShop.DAL.Datacontext;


namespace WebShop.Logic.Service
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected DatabaseContext context;

        public GenericRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public virtual async Task<T> Add(T entity)
        {
           
            var addentity= await context.AddAsync(entity);
            return addentity.Entity;
        }

        public virtual async Task<IEnumerable<T>> All()
        {

            return await context.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>()
                
                .Where(predicate).ToListAsync();
        }

       

        public virtual async Task<T>Get(Guid? id)
        {
            return await context.FindAsync<T>(id);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> parentpredikat, params string[] including)
        {
            var query = context.Set<T>().Where(parentpredikat).AsQueryable();
            if (including != null)
                including.ToList().ForEach(include =>
                {
                    if (!string.IsNullOrEmpty(include))
                        query = query.Include(include);
                });
            return query;
        }

        public IQueryable<T> GetInclude(Expression<Func<T, bool>> sourcepredicate, params string[] including)
        {
            var query = context.Set<T>().Where(sourcepredicate).AsQueryable();
            if (including != null)
                including.ToList().ForEach(include =>
                {
                    if (!string.IsNullOrEmpty(include))
                        query = query.Include(include);
                });
            return query;
        }

        public virtual void Remove(T entity)
        {
              context.Remove(entity);
        }
        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            context.RemoveRange(entities);
        }

        public virtual async Task<int> SaveChanges()
        {
            var dbresult= await context.SaveChangesAsync();
            return dbresult;
        }

        public virtual T Update(T entity)
        {
            return context.Update(entity)
                .Entity;
        }
    }
}
