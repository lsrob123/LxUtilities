using System;
using LxUtilities.Definitions.DTOs;

namespace LxUtilities.Definitions.ServiceBus.Messges
{
    public interface IBusRequest : IBusMessage, IRequest
    {
        Type ResponseType { get; }
    }
}