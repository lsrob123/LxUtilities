using System;
using LxUtilities.Definitions.ServiceBus;

namespace LxUtilities.Services.Tests.ServiceBus.MassTransit._ObjectMothers
{
    public class BusHostConfig : IBusHostConfig
    {
        public BusHostConfig(string uri, string username, string password, TimeSpan defaultRequestTimeout)
        {
            Uri = uri;
            Username = username;
            Password = password;
            DefaultRequestTimeout = defaultRequestTimeout;
        }

        public string Uri { get; }
        public string Username { get; }
        public string Password { get; }
        public TimeSpan DefaultRequestTimeout { get; }
    }

    public static class BusHostConfigMother
    {
        public static IBusHostConfig Default()
        {
            return new BusHostConfig("rabbitmq://localhost", "guest", "guest", TimeSpan.FromSeconds(2));
        }
    }
}