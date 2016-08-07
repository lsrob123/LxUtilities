using LxUtilities.Definitions.Core.Domain.Entity;

namespace LxUtilities.Definitions.Persistence
{
    public interface IRelationalModel
    {
        long Id { get; }
        void SetId(long id);
    }

    public interface IRelationalModel<TEntity> : IRelationalModel, IStoredEntityModel<TEntity>
        where TEntity : class, IEntity
    {
    }
}