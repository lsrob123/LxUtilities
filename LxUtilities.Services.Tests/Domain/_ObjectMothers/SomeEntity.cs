using System;
using LxUtilities.Definitions.Domain.Entity;
using LxUtilities.Definitions.Domain.Messaging;

namespace LxUtilities.Services.Tests.Domain._ObjectMothers
{
    public class SomeEntity : EntityBase
    {
        public Guid SomeValue { get; private set; }

        public SomeEntity(Guid key, Guid someValue) : base(key)
        {
            SomeValue = someValue;
        }

        public void MakeSomeChange(IDomainEventService domainEventService)
        {
            SomeValue = Guid.NewGuid();
            domainEventService.Publish(new SomeDomainEvent(this));
        }
    }
}