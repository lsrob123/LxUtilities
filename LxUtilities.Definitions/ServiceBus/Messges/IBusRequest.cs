using System;

namespace LxUtilities.Definitions.ServiceBus.Messges
{
    public interface IBusRequest : IBusMessage
    {
        Type ResponseType { get; }
    }
}