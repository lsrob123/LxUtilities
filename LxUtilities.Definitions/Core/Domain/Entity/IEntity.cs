using System;
using LxUtilities.Definitions.Core.Domain.Messaging;

namespace LxUtilities.Definitions.Core.Domain.Entity
{
    public interface IEntity
    {
        long Id { get; }
        Guid Key { get; }
        void SetKey(Guid key);
        void SetId(long id);
        void PublishAllDomainEvents();
        void PublishDomainEvent(IDomainEvent domainEvent);
    }
}