using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebShop.Logic.Service
{
    public interface IRepository<T>
    {
        Task<T> Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        T Update(T entity);
        Task<T> Get(Guid? id);
        IQueryable<T> GetInclude(Expression<Func<T, bool>> parentpredicate, params string[] including);
        Task<IEnumerable<T>> All();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll(Expression<Func<T, bool>> parentpredicate, params string[] including);
        Task<int> SaveChanges();
    }
}
