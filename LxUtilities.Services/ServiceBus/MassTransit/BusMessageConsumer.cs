using System;
using System.Threading.Tasks;
using LxUtilities.Definitions.ServiceBus;
using LxUtilities.Definitions.ServiceBus.Messges;
using MassTransit;

namespace LxUtilities.Services.ServiceBus.MassTransit
{
    public class BusMessageConsumer<TBusMessage> : IConsumer<TBusMessage>, IBusMessageConsumer
        where TBusMessage : class, IBusMessage
    {
        protected readonly Func<IBusMessageHandler<TBusMessage>> BusMessageHandlerFactory;

        public BusMessageConsumer(Func<IBusMessageHandler<TBusMessage>> busMessageHandlerFactory)
        {
            BusMessageHandlerFactory = busMessageHandlerFactory;
        }

        public async Task Consume(ConsumeContext<TBusMessage> context)
        {
            await Task.Run(() => BusMessageHandlerFactory().Handle(context.Message, context));
        }
    }
}