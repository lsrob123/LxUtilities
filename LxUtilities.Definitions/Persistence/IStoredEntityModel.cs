using LxUtilities.Definitions.Core.Domain.Entity;

namespace LxUtilities.Definitions.Persistence
{
    public interface IStoredEntityModel<TEntity>
        where TEntity : class, IEntity
    {
        TEntity Entity { get; }
        void SetEntity(TEntity entity);
    }
}