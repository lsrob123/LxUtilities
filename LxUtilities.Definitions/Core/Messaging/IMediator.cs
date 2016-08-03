using System;

namespace LxUtilities.Definitions.Core.Messaging
{
    public interface IMediator
    {
        IMediator Publish(object message);

        IMediator Subscribe(Type eventType, IMessageHandler handler);
    }
}