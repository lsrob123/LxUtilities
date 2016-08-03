using System;

namespace LxUtilities.Services.Tests.Domain._ObjectMothers
{
    public class SomePortsService : ISomePortsService
    {
        public SomePortsService()
        {
            Data = new SomeEntity(Guid.NewGuid(), Guid.NewGuid());
        }

        public SomeEntity Data { get; }
        public Guid LatestValue { get; protected set; }

        public Guid MakeSomeChangeAndGetNewValue()
        {
            Data.MakeSomeChange();
            return Data.SomeValue;
        }

        public void ProcessChange(SomeDomainEvent domainEvent)
        {
            LatestValue = domainEvent.Entity.SomeValue;
        }
    }
}