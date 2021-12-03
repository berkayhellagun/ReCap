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
    public class EfEntityRepositoryBase<TEntity,TContex>:IEntityRepository<TEntity>
        where TEntity:class,IEntity,new()
        where TContex:DbContext,new()
    {
        public void Add(TEntity entity)
        {
            using (TContex dB = new TContex())
            {
                var entityAdded = dB.Entry(entity);
                entityAdded.State = EntityState.Added;
                dB.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContex dB = new TContex())
            {
                var entityDeleted = dB.Entry(entity);
                entityDeleted.State = EntityState.Deleted;
                dB.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContex dB = new TContex())
            {
                return dB.Set<TEntity>().SingleOrDefault(filter);
            }
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

        public void Update(TEntity entity)
        {
            using (TContex dB = new TContex())
            {
                var entityUpdated = dB.Entry(entity);
                entityUpdated.State = EntityState.Modified;
                dB.SaveChanges();
            }
        }
    }
}
