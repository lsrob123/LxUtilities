using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LxUtilities.Definitions.ServiceBus;
using LxUtilities.Definitions.ServiceBus.Messges;
using MassTransit;
using MassTransit.Util;

namespace LxUtilities.Services.ServiceBus.MassTransit
{
    public class ServiceBusControl : IServiceBusControl<MassTransitBus>
    {
        protected const string InstanceNullMessage = "SingleBusControl.Instance is null";

        protected readonly ReaderWriterLockSlim BusControlLock =
            new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        protected readonly ICollection<string> EndpointNames;
        protected readonly Action<string, IRabbitMqReceiveEndpointConfigurator> RegisterConsumerAction;

        public ServiceBusControl(IBusHostConfig config, ICollection<string> endpointNames,
            Action<string, IRabbitMqReceiveEndpointConfigurator> registerConsumerAction)
        {
            Config = config;
            RegisterConsumerAction = registerConsumerAction;
            EndpointNames = endpointNames;
            Create();
        }

        public IBusHostConfig Config { get; }
        public MassTransitBus BusInstance { get; protected set; }

        public void Start()
        {
            BusControlLock.EnterWriteLock();
            try
            {
                var busControl = BusInstance as IBusControl;
                if (busControl == null)
                    return;

                busControl.Start();

                busControl.Publish(new BusReadyEvent()).Wait();
            }
            finally
            {
                BusControlLock.ExitWriteLock();
            }
        }

        public void Stop()
        {
            BusControlLock.EnterWriteLock();
            try
            {
                var busControl = BusInstance as IBusControl;
                busControl?.Stop();
            }
            finally
            {
                BusControlLock.ExitWriteLock();
            }
        }

        public Uri GetTypedResponseEndpoint(Type responseType)
        {
            return new Uri(Config.Uri).AppendToPath(responseType.FullName);
        }

        public async Task SendAsync<TBusMessage>(TBusMessage command, Action<TBusMessage, object> callback = null)
            where TBusMessage : class, IBusMessage
        {
            await PublishAsync(command, callback);
        }

        public async Task PublishAsync<TBusMessage>(TBusMessage message, Action<TBusMessage, object> callback = null)
            where TBusMessage : class, IBusMessage
        {
            if (BusInstance == null)
                throw new NullReferenceException(InstanceNullMessage);

            await BusInstance.Publish(message, context =>
            {
                if (callback == null)
                    return;

                callback.Invoke(context.Message, context);
            });
        }

        protected void Create()
        {
            BusControlLock.EnterUpgradeableReadLock();
            try
            {
                if (BusInstance != null)
                    return;

                BusControlLock.EnterWriteLock();
                try
                {
                    BusInstance = Bus.Factory.CreateUsingRabbitMq(rabbitMqBusFactoryConfigurator =>
                    {
                        var host = rabbitMqBusFactoryConfigurator.Host(new Uri(Config.Uri), rabbitMqHostConfigurator =>
                        {
                            rabbitMqHostConfigurator.Username(Config.Username);
                            rabbitMqHostConfigurator.Password(Config.Password);
                        });

                        foreach (var endpoint in EndpointNames)
                        {
                            rabbitMqBusFactoryConfigurator.ReceiveEndpoint(host, endpoint,
                                receiveEndpointConfigurator =>
                                {
                                    RegisterConsumerAction?.Invoke(endpoint, receiveEndpointConfigurator);
                                });
                        }
                    }) as MassTransitBus;

                    var busControl = (IBusControl) BusInstance;
                    if (busControl == null)
                        throw new NullReferenceException("Failed to create BusControl");
                }
                finally
                {
                    BusControlLock.ExitWriteLock();
                }
            }
            finally
            {
                BusControlLock.ExitUpgradeableReadLock();
            }
        }
    }
}