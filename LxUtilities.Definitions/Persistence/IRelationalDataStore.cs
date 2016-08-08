using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LxUtilities.Definitions.Core.Domain.Entity;

namespace LxUtilities.Definitions.Persistence
{
    public interface IRelationalDataStore
    {
        ICollection<TEntity> List<TEntity>(Func<IQueryable<IRelationalModel<TEntity>>, IQueryable<IRelationalModel<TEntity>>> queryFunc)
            where TEntity : class, IEntity, new();

        TEntity FirstOrDefault<TEntity>(Expression<Func<IRelationalModel<TEntity>, bool>> queryExpression)
            where TEntity : class, IEntity, new();

        TEntity SingleOrDefault<TEntity>(Expression<Func<IRelationalModel<TEntity>, bool>> queryExpression)
            where TEntity : class, IEntity, new();

        TEntity AddOrUpdate<TEntity>(TEntity entity, Expression<Func<IRelationalModel<TEntity>, bool>> queryExpression, bool saveChanges = true)
            where TEntity : class, IEntity, new();

        TEntity AddOrUpdateByKey<TEntity>(TEntity entity, bool saveChanges = true)
            where TEntity : class, IEntity, new();

        void Delete<TEntity>(Func<IRelationalModel<TEntity>, bool> queryExpression, bool saveChanges = true)
            where TEntity : class, IEntity, new();

        void DeleteByKey<TEntity>(Guid key, bool saveChanges = true)
            where TEntity : class, IEntity, new();
    }
}