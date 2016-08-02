using System;

namespace LxUtilities.Definitions.Domain.Entity
{
    public abstract class EntityBase : IEntity
    {
        protected EntityBase()
        {
        }

        protected EntityBase(Guid key)
        {
            Key = key;
        }

        public Guid Key { get; protected set; }

        public void SetKey(Guid key)
        {
            Key = key;
        }
    }
}