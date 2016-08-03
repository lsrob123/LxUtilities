using System;
using System.Diagnostics;
using System.Threading.Tasks;
using LxUtilities.Definitions.ServiceBus;
using LxUtilities.Services.Tests.ServiceBus.MassTransit._ObjectMothers;
using MassTransit;
using NUnit.Framework;

namespace LxUtilities.Services.Tests.ServiceBus.MassTransit
{
    [TestFixture]
    public class BusRequestResponseTests
    {
        [SetUp]
        public void SetUp()
        {
            _serviceBusControl = SingleBusControlMother.Default();

            _serviceBusControl.Start();
        }

        [TearDown]
        public void TearDown()
        {
            _serviceBusControl.Stop();
        }

        private IServiceBusControl<MassTransitBus> _serviceBusControl;

        private static async Task RunTest(IRequestClient<SomeBusRequest, SomeBusResponse> client, int testIndex)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var data = Guid.NewGuid();

            var response = await client.Request(new SomeBusRequest {SomeData = data});

            stopWatch.Stop();

            Console.WriteLine($"Time spent on Test {testIndex} = {stopWatch.ElapsedMilliseconds}ms");

            Assert.IsNotNull(response);
            Assert.AreEqual(data, response.SomeData);
            Console.WriteLine("Request [" + data + "] = Response [" + response.SomeData + "]");
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async void Given_BusRequest_When_RequestIsCalled_Then_ExpectedBusResponseIsReturned(int testIndex)
        {
            var responseEndpointUri = _serviceBusControl.GetTypedResponseEndpoint(typeof (SomeBusResponse));

            Console.WriteLine($"Response endpoint is {responseEndpointUri.AbsoluteUri}");

            var client = _serviceBusControl.BusInstance.CreateRequestClient<SomeBusRequest, SomeBusResponse>(
                responseEndpointUri, _serviceBusControl.Config.DefaultRequestTimeout);

            await RunTest(client, testIndex);
        }
    }
}