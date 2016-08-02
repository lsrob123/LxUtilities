using System;
using System.Threading.Tasks;
using LxUtilities.Definitions.ServiceBus.Messges;
using MassTransit;
using MassTransit.Util;

namespace LxUtilities.Services.ServiceBus.MassTransit
{
    public static class MassTransitBusExtension
    {
        /// <summary>
        /// Not working
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="hostUri"></param>
        /// <param name="endpointName"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task Send(this MassTransitBus bus, string hostUri, string endpointName, IBusCommand command)
        {
            var sendEndpointUri = new Uri(hostUri).AppendToPath(endpointName);
            var sendEndpoint = await (bus as ISendEndpointProvider).GetSendEndpoint(sendEndpointUri);
            await sendEndpoint.Send(command);
        }
    }
}