using System;
using System.Collections.Generic;

namespace LxUtilities.Definitions.Persistence
{
    public interface IRepository
    {
        ICollection<TModel> List<TModel>(Func<IEnumerable<TModel>, ICollection<TModel>> queryFunc)
            where TModel : class, IRelationalModel, new();

        TModel FirstOrDefault<TModel>(Func<TModel, bool> queryExpression)
            where TModel : class, IRelationalModel, new();

        TModel SingleOrDefault<TModel>(Func<TModel, bool> queryExpression)
            where TModel : class, IRelationalModel, new();

        TModel AddOrUpdate<TModel>(TModel dataEntity, Func<TModel, bool> queryExpression, bool saveChanges = true)
            where TModel : class, IRelationalModel, new();

        TModel AddOrUpdateByKey<TModel>(TModel dataEntity, bool saveChanges = true)
            where TModel : class, IRelationalModel, new();

        void Delete<TModel>(Func<TModel, bool> queryExpression, bool saveChanges = true)
            where TModel : class, IRelationalModel, new();

        void DeleteByKey<TModel>(Guid key, bool saveChanges = true)
            where TModel : class, IRelationalModel, new();
    }
}