using System;

namespace LxUtilities.Services.Tests.Domain._ObjectMothers
{
    public interface ISomePortsService
    {
        SomeEntity Data { get; }
        Guid LatestValue { get; }
        Guid MakeSomeChangeAndGetNewValue();
        void ProcessChange(SomeDomainEvent domainEvent);
    }
}