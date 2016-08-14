using System;
using System.Collections.Generic;
using LxUtilities.Definitions.Core.Domain.Messaging;
using LxUtilities.Definitions.Core.Messaging;

namespace LxUtilities.Definitions.Core.Domain.Entity
{
    public abstract class EntityBase : IEntity
    {
        protected EntityBase()
        {
            DomainEvents = new List<IDomainEvent>();
        }

        protected EntityBase(Guid key) : this()
        {
            Key = key;
        }

        public ICollection<IDomainEvent> DomainEvents { get; protected set; }
        public Guid Key { get; protected set; }

        public virtual void SetKey(Guid key)
        {
            Key = key;
        }

        public void Publish(IDomainEvent domainEvent)
        {
            Mediator.Default.Publish(domainEvent);
        }

        public virtual void PublishAllDomainEvents()
        {
            foreach (var domainEvent in DomainEvents)
            {
                Publish(domainEvent);
            }
        }
    }
}