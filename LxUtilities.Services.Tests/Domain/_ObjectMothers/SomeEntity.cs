using System;
using LxUtilities.Definitions.Core.Domain.Entity;

namespace LxUtilities.Services.Tests.Domain._ObjectMothers
{
    public class SomeEntity : EntityBase
    {
        public SomeEntity(Guid key, Guid someValue) : base(key)
        {
            SomeValue = someValue;
        }

        public Guid SomeValue { get; private set; }

        public void MakeSomeChange()
        {
            SomeValue = Guid.NewGuid();
            PublishDomainEvent(new SomeDomainEvent(this));
        }
   }
}