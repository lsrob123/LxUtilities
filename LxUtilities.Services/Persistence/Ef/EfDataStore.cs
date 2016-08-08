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
    public abstract class EfDataStore<TDbContext> : IRelationalDataStore where TDbContext : DbContext
    {
        protected readonly DbContext DbContext;
        protected readonly IMappingService MappingService;

        protected EfDataStore(TDbContext dbContext, IMappingService mappingService)
        {
            DbContext = dbContext;
            MappingService = mappingService;
        }

        public ICollection<TEntity> List<TEntity>(
            Func<IQueryable<IRelationalModel<TEntity>>, IQueryable<IRelationalModel<TEntity>>> queryFunc)
            where TEntity : class, IEntity, new()
        {
            var dbSet = GetQueryable<TEntity>();

            var list = queryFunc?.Invoke(dbSet).Select(x => x.Entity).ToList() ?? dbSet.Select(x => x.Entity).ToList();
            return list;
        }

        public TEntity FirstOrDefault<TEntity>(
            Expression<Func<IRelationalModel<TEntity>, bool>> queryExpression)
            where TEntity : class, IEntity, new()
        {
            var model = GetQueryable<TEntity>().FirstOrDefault(queryExpression);
            return model?.Entity;
        }

        public TEntity SingleOrDefault<TEntity>(
            Expression<Func<IRelationalModel<TEntity>, bool>> queryExpression)
            where TEntity : class, IEntity, new()
        {
            var model = GetQueryable<TEntity>().SingleOrDefault(queryExpression);
            return model?.Entity;
        }

        public TEntity AddOrUpdate<TEntity>(TEntity entity,
            Expression<Func<IRelationalModel<TEntity>, bool>> queryExpression, bool saveChanges = true)
            where TEntity : class, IEntity, new()
        {
            var queryable = GetQueryable<TEntity>();
            var existing = queryable.FirstOrDefault(queryExpression);
            //var persistenceModel = MappingService.Map(entity);
            var persistenceModel =
                (IRelationalModel<TEntity>)Activator.CreateInstance(GetRelationalModelType<TEntity>());
            persistenceModel.SetEntity(entity);

            var dbSet = GetDbSet<TEntity>();
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

        public void Delete<TEntity>(Func<IRelationalModel<TEntity>, bool> queryExpression, bool saveChanges = true)
            where TEntity : class, IEntity, new()
        {
            var existing = GetQueryable<TEntity>().SingleOrDefault(x => queryExpression(x));
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
            var relationalModelType = GetRelationalModelType<TEntity>();

            var dbSet = DbContext.Set(relationalModelType);
            return dbSet;
        }

        private Type GetRelationalModelType<TEntity>() where TEntity : class, IEntity
        {
            var entityType = typeof (TEntity);
            var relationalModelType = MappingService.GetRelationalModelType(entityType);
            if (relationalModelType == null)
                throw new NullReferenceException($"{entityType.FullName} has no matching relational model type");
            return relationalModelType;
        }

        /// <summary>
        ///     Get DbSet for read and write operations
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        protected virtual IQueryable<IRelationalModel<TEntity>> GetQueryable<TEntity>()
            where TEntity : class, IEntity
        {
            var relationalModelType = GetRelationalModelType<TEntity>();

            var method = DbContext.GetType().GetMethod("Set", new Type[] {}).MakeGenericMethod(relationalModelType);
            var dbSet = (IQueryable<IRelationalModel<TEntity>>) method.Invoke(DbContext, new object[] {});

            return dbSet;
        }
    }
}