using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);

        Task<bool> AsyncAdd(T entity);
        Task<bool> AsyncUpdate(T entity);
        Task<bool> AsyncDelete(T entity);
        Task<List<T>> AsyncGetAll(Expression<Func<T, bool>> filter = null);
        Task<T> AsyncGet(Expression<Func<T, bool>> filter);
    }
}
