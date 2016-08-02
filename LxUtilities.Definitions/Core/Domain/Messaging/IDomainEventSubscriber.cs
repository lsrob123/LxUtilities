using System;

namespace LxUtilities.Definitions.Core.Domain.Messaging
{
    public interface IDomainEventSubscriber
    {
        IDomainEventSubscriber Subscribe<TEvent>(IDomainEventHandler handler) where TEvent : IDomainEvent;
        IDomainEventSubscriber Subscribe(Type eventType, IDomainEventHandler handler);
    }
}