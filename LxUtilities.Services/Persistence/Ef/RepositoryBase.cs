using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LxUtilities.Definitions.Core.Domain.Entity;
using LxUtilities.Definitions.Mapping;
using LxUtilities.Definitions.Persistence;

namespace LxUtilities.Services.Persistence.EF
{
    public abstract class RelationalRepositoryBase<TDbContext> : IRepository where TDbContext : DbContext
    {
        protected readonly DbContext DbContext;
        protected readonly IMappingService MappingService;

        protected RelationalRepositoryBase(TDbContext dbContext, IMappingService mappingService)
        {
            DbContext = dbContext;
            MappingService = mappingService;
        }

        public ICollection<TEntity> List<TEntity>(
            Func<IEnumerable<IStoredEntityModel<TEntity>>, ICollection<IStoredEntityModel<TEntity>>> queryFunc)
            where TEntity : class, IEntity, new()
        {
            var dbSet = GetDbSetWithStoredEntityModels<TEntity>();

            var list = queryFunc?.Invoke(dbSet).Select(x => x.Entity).ToList() ?? dbSet.Select(x => x.Entity).ToList();
            return list;
        }

        public TEntity FirstOrDefault<TEntity>(
            Func<IStoredEntityModel<TEntity>, bool> queryExpression)
            where TEntity : class, IEntity, new()
        {
            var storedEntityModel = GetDbSetWithStoredEntityModels<TEntity>().FirstOrDefault(x => queryExpression(x));
            return storedEntityModel?.Entity;
        }

        public TEntity SingleOrDefault<TEntity>(
            Func<IStoredEntityModel<TEntity>, bool> queryExpression)
            where TEntity : class, IEntity, new()
        {
            var storedEntityModel = GetDbSetWithStoredEntityModels<TEntity>().SingleOrDefault(x => queryExpression(x));
            return storedEntityModel?.Entity;
        }

        public TEntity AddOrUpdate<TEntity>(TEntity entity,
            Func<IStoredEntityModel<TEntity>, bool> queryExpression, bool saveChanges = true)
            where TEntity : class, IEntity, new()
        {
            var dbSet = GetDbSetWithRelationalModels<TEntity>();
            var existing = dbSet.FirstOrDefault(x => queryExpression(x));
            var persistenceModel = MappingService.Map(entity);

            if (existing == null)
            {
                dbSet.Attach(persistenceModel);
                DbContext.Entry(persistenceModel).State = EntityState.Added;
            }
            else
            {
                persistenceModel.SetId(existing.Id);
                DbContext.Entry(existing).State = EntityState.Detached;
                dbSet.Attach(persistenceModel);
                DbContext.Entry(persistenceModel).State = EntityState.Modified;
            }

            if (saveChanges)
                DbContext.SaveChanges();

            return persistenceModel.Entity;
        }

        public TEntity AddOrUpdateByKey<TEntity>(TEntity entity, bool saveChanges = true)
            where TEntity : class, IEntity, new()
        {
            return AddOrUpdate(entity, existing => existing.Entity.Key == entity.Key, saveChanges);
        }

        public void Delete<TEntity>(Func<IStoredEntityModel<TEntity>, bool> queryExpression, bool saveChanges = true)
            where TEntity : class, IEntity, new()
        {
            var existing = GetDbSetWithRelationalModels<TEntity>().SingleOrDefault(x => queryExpression(x));
            if (existing != null)
                DbContext.Entry(existing).State = EntityState.Deleted;

            if (saveChanges)
                DbContext.SaveChanges();
        }

        public void DeleteByKey<TEntity>(Guid key, bool saveChanges = true)
            where TEntity : class, IEntity, new()
        {
            Delete<TEntity>(existing => existing.Entity.Key == key, saveChanges);
        }

        protected virtual DbSet GetDbSet<TEntity>() where TEntity : class, IEntity
        {
            var entityType = typeof (TEntity);
            var relationalModelType = MappingService.GetRelationalModelType(entityType);
            if (relationalModelType == null)
                throw new NullReferenceException($"{entityType.FullName} has no matching relational model type");

            return DbContext.Set(relationalModelType);
        }

        /// <summary>
        ///     Get DbSet for read and write operations
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        protected virtual DbSet<IRelationalModel<TEntity>> GetDbSetWithRelationalModels<TEntity>()
            where TEntity : class, IEntity
        {
            return GetDbSet<TEntity>().Cast<IRelationalModel<TEntity>>();
        }

        /// <summary>
        ///     Get DbSet for read-only operations
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        protected virtual DbSet<IStoredEntityModel<TEntity>> GetDbSetWithStoredEntityModels<TEntity>()
            where TEntity : class, IEntity
        {
            return GetDbSet<TEntity>().Cast<IStoredEntityModel<TEntity>>();
        }
    }
}