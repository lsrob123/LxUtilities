using System;

namespace LxUtilities.Definitions.ServiceBus
{
    public interface IBusHostConfig
    {
        string Uri { get; } 
        string Username { get; }
        string Password { get; }

        TimeSpan DefaultRequestTimeout { get; }
    }
}