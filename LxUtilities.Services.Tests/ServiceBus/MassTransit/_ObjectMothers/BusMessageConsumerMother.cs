using System;
using LxUtilities.Definitions.ServiceBus;
using MassTransit;
using NUnit.Framework;

namespace LxUtilities.Services.Tests.ServiceBus.MassTransit._ObjectMothers
{
    public class BusCommandHandler : IBusMessageHandler<ISomeBusCommand>
    {
        public void Handle(ISomeBusCommand message, object context = null)
        {
            Assert.IsNotNull(context);
            Assert.IsNotNull(message);

            Console.WriteLine(GetType().FullName + " [" + message.SomeData + "] was processed.");

            CommandState.Consumed = message.SomeData;
            Assert.AreEqual(CommandState.Sent, CommandState.Consumed);
            Console.WriteLine("Sent or published [" + CommandState.Sent + "] = Consumed [" + message.SomeData + "]");
        }
    }

    public class BusEventHandler : IBusMessageHandler<ISomeBusEvent>
    {
        public void Handle(ISomeBusEvent message, object context = null)
        {
            Assert.IsNotNull(context);
            Assert.IsNotNull(message);

            Console.WriteLine(GetType().FullName + " [" + message.SomeData + "] was processed.");

            EventState.Consumed = message.SomeData;
            Assert.AreEqual(EventState.Sent, EventState.Consumed);
            Console.WriteLine("Published [" + EventState.Sent + "] = Consumed [" + message.SomeData + "]");
        }
    }

    public class BusRequestHandler : IBusMessageHandler<ISomeBusRequest>
    {
        public void Handle(ISomeBusRequest message, object context = null)
        {
            var consumeContext = context as ConsumeContext<SomeBusRequest>;
            if (consumeContext == null)
                throw new ArgumentNullException(nameof(context));

            Console.WriteLine(GetType().FullName + " [" + message.SomeData + "] was processed.");
            consumeContext.Respond(new SomeBusResponse {SomeData = message.SomeData});
        }
    }


    public static class CommandState
    {
        public static Guid Sent;
        public static Guid Consumed;
    }

    public static class EventState
    {
        public static Guid Sent;
        public static Guid Consumed;
    }
}