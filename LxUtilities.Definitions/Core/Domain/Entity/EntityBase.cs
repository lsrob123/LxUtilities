using System;
using LxUtilities.Definitions.Core.Domain.Messaging;
using LxUtilities.Definitions.Core.Messaging;

namespace LxUtilities.Definitions.Core.Domain.Entity
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

        public virtual void SetKey(Guid key)
        {
            Key = key;
        }

        public virtual void RaiseEvent(IDomainEvent domainEvent)
        {
            MediatorLocator.Default.Publish(domainEvent);
        }
    }
}