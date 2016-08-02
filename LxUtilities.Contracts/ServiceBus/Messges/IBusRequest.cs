using System;

namespace LxUtilities.Contracts.ServiceBus.Messges
{
    public interface IBusRequest : IBusMessage
    {
        Type ResponseType { get; }
    }
}