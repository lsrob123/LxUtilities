using System;
using LxUtilities.Definitions.ServiceBus.Messges;

namespace LxUtilities.Services.Tests.ServiceBus.MassTransit._ObjectMothers
{
    public interface ISomeBusMessage : IBusMessage
    {
        Guid SomeData { get; set; }
    }

    public interface ISomeBusCommand : IBusCommand, ISomeBusMessage
    {
    }

    public class SomeBusCommand : ISomeBusCommand
    {
        public Guid SomeData { get; set; }
    }

    public interface ISomeBusEvent : IBusEvent, ISomeBusMessage
    {
    }

    public class SomeBusEvent : ISomeBusEvent
    {
        public Guid SomeData { get; set; }
    }

    public interface ISomeBusResponse : IBusResponse, ISomeBusMessage
    {
    }

    public class SomeBusResponse : ISomeBusResponse
    {
        public Guid SomeData { get; set; }
    }

    public interface ISomeBusRequest : IBusRequest, ISomeBusMessage
    {
    }

    public class SomeBusRequest : BusRequestBase<SomeBusResponse>, ISomeBusRequest
    {
        public Guid SomeData { get; set; }
    }
}