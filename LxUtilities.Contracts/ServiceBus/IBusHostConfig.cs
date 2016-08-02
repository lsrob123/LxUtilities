using System;

namespace LxUtilities.Contracts.ServiceBus
{
    public interface IBusHostConfig
    {
        string Uri { get; } 
        string Username { get; }
        string Password { get; }

        TimeSpan DefaultRequestTimeout { get; }
    }
}