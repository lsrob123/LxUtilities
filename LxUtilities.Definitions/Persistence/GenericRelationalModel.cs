using LxUtilities.Definitions.Core.Domain.Entity;

namespace LxUtilities.Definitions.Persistence
{
    public class GenericRelationalModel<TEntity> : IRelationalModel<TEntity>
        where TEntity : class, IEntity
    {
        public GenericRelationalModel()
        {
        }

        public GenericRelationalModel(TEntity entity)
        {
            SetEntity(entity);
        }

        public TEntity Entity { get; protected set; }
        public long Id { get; protected set; }

        public void SetId(long id)
        {
            Id = id;
        }

        public void SetEntity(TEntity entity)
        {
            Entity = entity;
        }
    }
}