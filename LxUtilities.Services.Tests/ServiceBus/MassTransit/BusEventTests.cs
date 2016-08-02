using System;
using System.Threading.Tasks;
using LxUtilities.Definitions.ServiceBus;
using LxUtilities.Services.Tests.ServiceBus.MassTransit._ObjectMothers;
using MassTransit;
using NUnit.Framework;

namespace LxUtilities.Services.Tests.ServiceBus.MassTransit
{
    [TestFixture]
    public class BusEventTests
    {
        [SetUp]
        public void SetUp()
        {
            _singleBusControl = SingleBusControlMother.Default();

            _singleBusControl.Start();
        }

        [TearDown]
        public void TearDown()
        {
            _singleBusControl.Stop();
        }

        private ISingleBusControl<MassTransitBus> _singleBusControl;

        [Test]
        public async void Given_BusEvent_When_PublishIsCalled_Then_EventConsumerConsumesTheEvent()
        {
            EventState.Sent = Guid.NewGuid();
            EventState.Consumed = Guid.Empty;
            var busEvent = new SomeBusEvent {SomeData = EventState.Sent};

            await _singleBusControl.PublishAsync(busEvent, (message, context) =>
            {
                Assert.IsNotNull(context);
                Assert.IsNotNull(message);
                Console.WriteLine("Publish callback was called.");
            });

            var interval = TimeSpan.FromMilliseconds(10);
            for (var i = 0; i < 30; i++)
            {
                if (EventState.Consumed != Guid.Empty)
                {
                    Assert.AreEqual(EventState.Sent, EventState.Consumed);
                    Console.WriteLine("EventState\'s value match in main thread succeeded.");

                    Console.WriteLine($"Time spent on full EventState update cycle is about {i*10}ms");

                    Assert.Greater(200, i*10);

                    Assert.Pass("Event was consumed");
                    break;
                }

                await Task.Delay(interval);
            }

            Assert.Fail("Fail to consume busEvent.");
        }
    }
}