using System;
using LxUtilities.Definitions.Core.Messaging;

namespace LxUtilities.Definitions.Core.Domain.Messaging
{
    public abstract class DomainEventHandlerBase<TEvent> : MediatorMessageHandlerBase<TEvent>
        where TEvent : class, IDomainEvent
    {
        protected DomainEventHandlerBase(IMediator mediator = null) : base(mediator)
        {
        }

        public override void Handle(object message)
        {
            var domainEvent = message as TEvent;
            if (domainEvent == null)
            {
                throw new ArgumentOutOfRangeException(nameof(message),
                    $"{message.GetType().FullName} doesn't implement {nameof(IDomainEvent)}");
            }

            base.Handle(domainEvent);
        }
    }
}