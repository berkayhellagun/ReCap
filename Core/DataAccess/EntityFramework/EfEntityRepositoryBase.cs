using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContex> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContex : DbContext, new()
    {
        public bool Add(TEntity entity)
        {
            using (TContex dB = new TContex())
            {
                var entityAdded = dB.Entry(entity);
                entityAdded.State = EntityState.Added;
                return dB.SaveChanges() > 0;
            }
        }
        public Task<bool> AsyncAdd(TEntity entity)
        {
            //firstly async add run async then run sync add
            return Task.Run(() => { return Add(entity); });
        }

        public bool Delete(TEntity entity)
        {
            using (TContex dB = new TContex())
            {
                var entityDeleted = dB.Entry(entity);
                entityDeleted.State = EntityState.Deleted;
                return dB.SaveChanges() > 0;
            }
        }
        public Task<bool> AsyncDelete(TEntity entity)
        {
            return Task.Run(() => { return Delete(entity); });
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContex dB = new TContex())
            {
                return dB.Set<TEntity>().SingleOrDefault(filter);
            }
        }
        public Task<TEntity> AsyncGet(Expression<Func<TEntity, bool>> filter)
        {
            return Task.Run(() => { return Get(filter); });
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContex dB = new TContex())
            {
                return filter == null
                    ? dB.Set<TEntity>().ToList()
                    : dB.Set<TEntity>().Where(filter).ToList();
            }
        }
        public Task<List<TEntity>> AsyncGetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return Task.Run(() => { return GetAll(filter); });
        }

        public bool Update(TEntity entity)
        {
            using (TContex dB = new TContex())
            {
                var entityUpdated = dB.Entry(entity);
                entityUpdated.State = EntityState.Modified;
                return dB.SaveChanges() > 0;
            }
        }
        public Task<bool> AsyncUpdate(TEntity entity)
        {
            return Task.Run(() => { return Update(entity); });
        }
    }
}
