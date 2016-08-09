using System.ComponentModel.DataAnnotations;
using LxUtilities.Definitions.Core.Domain.Entity;

namespace LxUtilities.Definitions.Persistence
{
    public abstract class GenericRelationalModel<TEntity> : IRelationalModel<TEntity>
        where TEntity : class, IEntity
    {
        protected GenericRelationalModel()
        {
        }

        protected GenericRelationalModel(TEntity entity)
        {
            SetEntity(entity);
        }

        [Required]
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