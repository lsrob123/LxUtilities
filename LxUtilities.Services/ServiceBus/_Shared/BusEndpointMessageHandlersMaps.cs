using System;
using System.Collections.Generic;
using LxUtilities.Definitions.ServiceBus;

namespace LxUtilities.Services.ServiceBus._Shared
{
    public class BusEndpointMessageHandlersMaps
    {
        public BusEndpointMessageHandlersMaps(IDictionary<string, ICollection<Func<IBusMessageHandler>>> maps = null)
        {
            Maps = maps ?? new Dictionary<string, ICollection<Func<IBusMessageHandler>>>();
        }

        public IDictionary<string, ICollection<Func<IBusMessageHandler>>> Maps { get; }
    }
}