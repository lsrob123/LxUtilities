using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using LxUtilities.Definitions.Core.Domain.Entity;
using LxUtilities.Definitions.Mapping;
using LxUtilities.Definitions.Persistence;

namespace LxUtilities.Services.Persistence.EF
{
    public abstract class DataStore<TDbContext> : IRelationalDataStore where TDbContext : DbContext
    {
        protected readonly DbContext DbContext;
        protected readonly IMappingService MappingService;

        protected DataStore(TDbContext dbContext, IMappingService mappingService)
        {
            DbContext = dbContext;
            MappingService = mappingService;
        }

        public ICollection<TEntity> List<TEntity>(
            Func<IQueryable<TEntity>, IQueryable<TEntity>> queryFunc)
            where TEntity : class, IEntity, new()
        {
            var dbSet = DbContext.Set<TEntity>();

            var entities = new List<TEntity>(queryFunc == null ? dbSet : queryFunc(dbSet));

            return entities;
        }

        public TEntity FirstOrDefault<TEntity>(
            Expression<Func<TEntity, bool>> queryExpression)
            where TEntity : class, IEntity, new()
        {
            var dbSet = DbContext.Set<TEntity>();
            var entity = dbSet.FirstOrDefault(queryExpression);
            return entity;
        }

        public TEntity SingleOrDefault<TEntity>(
            Expression<Func<TEntity, bool>> queryExpression)
            where TEntity : class, IEntity, new()
        {
            var dbSet = DbContext.Set<TEntity>();
            var entity = dbSet.SingleOrDefault(queryExpression);
            return entity;
        }

        public TEntity AddOrUpdate<TEntity>(TEntity entity,
            Expression<Func<TEntity, bool>> queryExpression, bool saveChanges = true)
            where TEntity : class, IEntity, new()
        {
            var dbSet = DbContext.Set<TEntity>();
            var existing = dbSet.FirstOrDefault(queryExpression);

            if (existing == null)
            {
                dbSet.Attach(entity);
                DbContext.Entry(entity).State = EntityState.Added;
            }
            else
            {
                entity.SetId(existing.Id);
                DbContext.Entry(existing).State = EntityState.Detached;
                dbSet.Attach(entity);
                DbContext.Entry(entity).State = EntityState.Modified;
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
            var dbSet = DbContext.Set<TEntity>();
            var existing = dbSet.SingleOrDefault(x => queryExpression(x));
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