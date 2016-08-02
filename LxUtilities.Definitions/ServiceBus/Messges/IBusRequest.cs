using System;
using LxUtilities.Definitions.Core.DTOs;

namespace LxUtilities.Definitions.ServiceBus.Messges
{
    public interface IBusRequest : IBusMessage, IRequest
    {
        Type ResponseType { get; }
    }
}