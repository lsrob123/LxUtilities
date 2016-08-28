using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
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

        [NotMapped]
        public ICollection<IDomainEvent> DomainEvents { get; protected set; }

        [Key]
        [IgnoreDataMember]
        public long Id { get; protected set; }

        public Guid Key { get; protected set; }

        public virtual void SetKey(Guid key)
        {
            Key = key;
        }

        public virtual void SetId(long id)
        {
            Id = id;
        }

        public virtual void PublishAllDomainEvents()
        {
            foreach (var domainEvent in DomainEvents)
            {
                PublishDomainEvent(domainEvent);
            }
        }

        public void PublishDomainEvent(IDomainEvent domainEvent)
        {
            Mediator.Default.Publish(domainEvent);
        }
    }
}