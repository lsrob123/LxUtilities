using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LxUtilities.Definitions.Persistence;

namespace LxUtilities.Services.Persistence.Ef
{
    public abstract class RepositoryBase<TDbContext> : IRepository where TDbContext : DbContext
    {
        protected readonly Func<TDbContext> DbContextFactory;

        protected RepositoryBase(Func<TDbContext> dbContextFactory)
        {
            DbContextFactory = dbContextFactory;
        }

        public ICollection<TModel> List<TModel>(Func<IEnumerable<TModel>, ICollection<TModel>> queryFunc)
            where TModel : class, IRelationalModel, new()
        {
            using (var context = DbContextFactory())
            {
                var query = context.Set<TModel>();
                var list = queryFunc == null ? query.ToList() : queryFunc(query);
                return list;
            }
        }

        public TModel FirstOrDefault<TModel>(Func<TModel, bool> queryExpression)
            where TModel : class, IRelationalModel, new()
        {
            using (var context = DbContextFactory())
            {
                var dataEntity = context.Set<TModel>().FirstOrDefault(x => queryExpression(x));
                return dataEntity;
            }
        }

        public TModel SingleOrDefault<TModel>(Func<TModel, bool> queryExpression)
            where TModel : class, IRelationalModel, new()
        {
            using (var context = DbContextFactory())
            {
                var dataEntity = context.Set<TModel>().SingleOrDefault(x => queryExpression(x));
                return dataEntity;
            }
        }

        public TModel AddOrUpdate<TModel>(TModel dataEntity, Func<TModel, bool> queryExpression, bool saveChanges = true)
            where TModel : class, IRelationalModel, new()
        {
            using (var context = DbContextFactory())
            {
                var existingDataEntity = context.Set<TModel>().SingleOrDefault(x => queryExpression(x));

                if (existingDataEntity == null)
                {
                    context.Set<TModel>().Attach(dataEntity);
                    context.Entry(dataEntity).State = EntityState.Added;
                }
                else
                {
                    dataEntity.SetId(existingDataEntity.Id);
                    context.Entry(existingDataEntity).State = EntityState.Detached;
                    context.Set<TModel>().Attach(dataEntity);
                    context.Entry(dataEntity).State = EntityState.Modified;
                }

                if (saveChanges)
                    context.SaveChanges();

                return dataEntity;
            }
        }

        public TModel AddOrUpdateByKey<TModel>(TModel dataEntity, bool saveChanges = true)
            where TModel : class, IRelationalModel, new()
        {
            return AddOrUpdate(dataEntity, existing => existing.Key == dataEntity.Key, saveChanges);
        }

        public void Delete<TModel>(Func<TModel, bool> queryExpression, bool saveChanges = true)
            where TModel : class, IRelationalModel, new()
        {
            using (var context = DbContextFactory())
            {
                var existingDataEntity = context.Set<TModel>().SingleOrDefault(x => queryExpression(x));
                if (existingDataEntity != null)
                    context.Entry(existingDataEntity).State = EntityState.Deleted;

                if (saveChanges)
                    context.SaveChanges();
            }
        }

        public void DeleteByKey<TModel>(Guid key, bool saveChanges = true)
            where TModel : class, IRelationalModel, new()
        {
            Delete<TModel>(existing => existing.Key == key, saveChanges);
        }
    }
}