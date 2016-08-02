using LxUtilities.Services.Domain.Messaging;
using LxUtilities.Services.Tests.Domain._ObjectMothers;
using NUnit.Framework;

namespace LxUtilities.Services.Tests.Domain.Messaging
{
    [TestFixture]
    public class DomainEventServiceTests
    {
        [Test]
        public void Given_SomeEntity_When_SomeChangeIsPublished_Then_TheChangeIsHandledProperly()
        {
            var domainEventService = new DomainEventService();
            var portService = new SomePortsService(domainEventService);
            new SomeDomainEventHandler(portService).SubscribeWith(domainEventService);

            var oldValue = portService.Data.SomeValue;
            var newValue = portService.MakeSomeChangeAndGetNewValue();
            var afterEventHandledValue = portService.LatestValue;

            Assert.AreNotEqual(oldValue, afterEventHandledValue);
            Assert.AreNotEqual(oldValue, newValue);
            Assert.AreEqual(newValue, afterEventHandledValue);
        }
    }
}