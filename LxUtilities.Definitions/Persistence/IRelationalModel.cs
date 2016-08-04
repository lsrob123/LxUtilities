using LxUtilities.Definitions.Core.Domain.Entity;

namespace LxUtilities.Definitions.Persistence
{
    public interface IRelationalModel
    {
        long Id { get; }
        void SetId(long id);
    }

    public interface IRelationalModel<out TEntity> : IRelationalModel
        where TEntity: class, IEntity
    {
        TEntity Entity { get; }
    }
}