using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LxUtilities.Definitions.Core.Domain.Entity;
using LxUtilities.Definitions.Persistence;

namespace LxUtilities.Services.Persistence.EF
{
    public abstract class RelationalRepositoryBase<TDbContext> : IRepository where TDbContext : DbContext
    {
        protected readonly DbContext DbContext;

        protected RelationalRepositoryBase(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ICollection<TEntity> List<TEntity>(Func<IEnumerable<TEntity>, ICollection<TEntity>> queryFunc)
            where TEntity : class, IEntity, new()
        {
            var query = DbContext.Set<GenericRelationalModel<TEntity>>().Select(x => x.Entity);
            var list = queryFunc == null ? query.ToList() : queryFunc(query);
            return list;
        }

        public TEntity FirstOrDefault<TEntity>(Func<TEntity, bool> queryExpression)
            where TEntity : class, IEntity, new()
        {
            var dataEntity = DbContext.Set<GenericRelationalModel<TEntity>>()
                .Select(x => x.Entity).FirstOrDefault(x => queryExpression(x));
            return dataEntity;
        }

        public TEntity SingleOrDefault<TEntity>(Func<TEntity, bool> queryExpression)
            where TEntity : class, IEntity, new()
        {
            var dataEntity = DbContext.Set<GenericRelationalModel<TEntity>>()
                .Select(x => x.Entity).SingleOrDefault(x => queryExpression(x));
            return dataEntity;
        }

        public TEntity AddOrUpdate<TEntity>(TEntity entity, Func<TEntity, bool> queryExpression, bool saveChanges = true)
            where TEntity : class, IEntity, new()
        {
            var existing = DbContext.Set<GenericRelationalModel<TEntity>>().FirstOrDefault(x => queryExpression(x.Entity));
            var persistenceModel = new GenericRelationalModel<TEntity>();

            if (existing == null)
            {
                DbContext.Set<GenericRelationalModel<TEntity>>().Attach(persistenceModel);
                DbContext.Entry(persistenceModel).State = EntityState.Added;
            }
            else
            {
                persistenceModel.SetId(existing.Id);
                DbContext.Entry(existing).State = EntityState.Detached;
                DbContext.Set<GenericRelationalModel<TEntity>>().Attach(persistenceModel);
                DbContext.Entry(persistenceModel).State = EntityState.Modified;
            }

            if (saveChanges)
                DbContext.SaveChanges();

            return entity;
        }

        public TEntity AddOrUpdateByKey<TEntity>(TEntity entity, bool saveChanges = true)
            where TEntity : class, IEntity, new()
        {
            return AddOrUpdate(entity, existing => existing.Key == entity.Key, saveChanges);
        }

        public void Delete<TEntity>(Func<TEntity, bool> queryExpression, bool saveChanges = true)
            where TEntity : class, IEntity, new()
        {
            var existing =
                DbContext.Set<GenericRelationalModel<TEntity>>().SingleOrDefault(x => queryExpression(x.Entity));
            if (existing != null)
                DbContext.Entry(existing).State = EntityState.Deleted;

            if (saveChanges)
                DbContext.SaveChanges();
        }

        public void DeleteByKey<TEntity>(Guid key, bool saveChanges = true)
            where TEntity : class, IEntity, new()
        {
            Delete<TEntity>(existing => existing.Key == key, saveChanges);
        }
    }
}