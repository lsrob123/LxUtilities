using System;
using System.Threading.Tasks;
using LxUtilities.Definitions.ServiceBus;
using LxUtilities.Services.Tests.ServiceBus.MassTransit._ObjectMothers;
using MassTransit;
using NUnit.Framework;

namespace LxUtilities.Services.Tests.ServiceBus.MassTransit
{
    [TestFixture]
    public class BusCommandTests
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
        [TestCase(BusInteraction.Send)]
        [TestCase(BusInteraction.Publish)]
        public async void Given_BusCommand_When_SendOrPublishIsCalled_Then_CommandConsumerConsumesTheCommand(
            BusInteraction busInteraction)
        {
            CommandState.Sent = Guid.NewGuid();
            CommandState.Consumed = Guid.Empty;
            var busCommand = new SomeBusCommand {SomeData = CommandState.Sent};

            switch (busInteraction)
            {
                case BusInteraction.Send:
                    await _singleBusControl.SendAsync(busCommand, (message, context) =>
                    {
                        Assert.IsNotNull(context);
                        Assert.IsNotNull(message);
                        Console.WriteLine("Send callback was called.");
                    });

                    break;

                case BusInteraction.Publish:
                    await _singleBusControl.PublishAsync(busCommand, (message, context) =>
                    {
                        Assert.IsNotNull(context);
                        Assert.IsNotNull(message);
                        Console.WriteLine("Publish callback was called.");
                    });

                    break;

                default:
                    Assert.Fail(busInteraction + " is not covered by the test.");
                    break;
            }

            var interval = TimeSpan.FromMilliseconds(10);
            for (var i = 0; i < 30; i++)
            {
                if (CommandState.Consumed != Guid.Empty)
                {
                    Assert.AreEqual(CommandState.Sent, CommandState.Consumed);
                    Console.WriteLine("CommandState\'s value match in main thread succeeded.");

                    Console.WriteLine($"Time spent on full CommandState update cycle is about {i*10}ms");

                    Assert.Greater(200, i*10);

                    Assert.Pass("Command was consumed");
                    break;
                }

                await Task.Delay(interval);
            }

            Assert.Fail("Fail to consume command.");
        }
    }
}