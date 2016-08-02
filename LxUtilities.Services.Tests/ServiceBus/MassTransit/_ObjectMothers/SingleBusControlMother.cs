using System.Collections.Generic;
using LxUtilities.Definitions.ServiceBus;
using LxUtilities.Services.ServiceBus.MassTransit;
using MassTransit;

namespace LxUtilities.Services.Tests.ServiceBus.MassTransit._ObjectMothers
{
    public static class SingleBusControlMother
    {
        private const string EndpointForCommandsAndEvents = "EndpointForCommandsAndEvents";
        private static readonly string EndpointSomeResponse = typeof (SomeBusResponse).FullName;

        public static ISingleBusControl<MassTransitBus> Default()
        {
            var singleBusControl = new SingleBusControl(BusHostConfigMother.Default(), new List<string>
            {
                EndpointForCommandsAndEvents,
                EndpointSomeResponse
            }, (endpoint, configurator) =>
            {
                if (endpoint == EndpointForCommandsAndEvents)
                {
                    configurator.Consumer(() => new BusMessageConsumer<SomeBusCommand>(() => new BusCommandHandler()));
                    configurator.Consumer(() => new BusMessageConsumer<SomeBusEvent>(() => new BusEventHandler()));
                }
                else if (endpoint == EndpointSomeResponse)
                {
                    configurator.Consumer(() => new BusMessageConsumer<SomeBusRequest>(() => new BusRequestHandler()));
                }
            });
            return singleBusControl;
        }
    }
}