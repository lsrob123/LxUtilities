using System;
using System.Collections.Generic;
using LxUtilities.Definitions.Core.Domain.Entity;

namespace LxUtilities.Definitions.Persistence
{
    public interface IRepository
    {
        ICollection<TEntity> List<TEntity>(Func<IEnumerable<IStoredEntityModel<TEntity>>, ICollection<IStoredEntityModel<TEntity>>> queryFunc)
            where TEntity : class, IEntity, new();

        TEntity FirstOrDefault<TEntity>(Func<IStoredEntityModel<TEntity>, bool> queryExpression)
            where TEntity : class, IEntity, new();

        TEntity SingleOrDefault<TEntity>(Func<IStoredEntityModel<TEntity>, bool> queryExpression)
            where TEntity : class, IEntity, new();

        TEntity AddOrUpdate<TEntity>(TEntity entity, Func<IStoredEntityModel<TEntity>, bool> queryExpression, bool saveChanges = true)
            where TEntity : class, IEntity, new();

        TEntity AddOrUpdateByKey<TEntity>(TEntity entity, bool saveChanges = true)
            where TEntity : class, IEntity, new();

        void Delete<TEntity>(Func<IStoredEntityModel<TEntity>, bool> queryExpression, bool saveChanges = true)
            where TEntity : class, IEntity, new();

        void DeleteByKey<TEntity>(Guid key, bool saveChanges = true)
            where TEntity : class, IEntity, new();
    }
}