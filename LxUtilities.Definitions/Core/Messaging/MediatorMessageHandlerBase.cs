using System;

namespace LxUtilities.Definitions.Core.Messaging
{
    public abstract class MediatorMessageHandlerBase<TMessage> : IMessageHandler
        where TMessage : class
    {
        protected MediatorMessageHandlerBase(IMediator mediator = null)
        {
            if (mediator == null)
                mediator = MediatorLocator.Default;

            mediator.Subscribe(typeof (TMessage), this);
        }

        public virtual void Handle(object message)
        {
            var domainEvent = message as TMessage;
            if (domainEvent == null)
            {
                throw new ArgumentOutOfRangeException(nameof(message),
                    $"{message.GetType().FullName} doesn't implement {typeof (TMessage).FullName}");
            }

            HandleAction(domainEvent);
        }

        protected abstract void HandleAction(TMessage message);
    }
}