﻿using System.Threading.Tasks;
using LxUtilities.Services.Bootstrapping;
using NUnit.Framework;

namespace LxUtilities.Services.Tests.Bootstrapping
{
    [TestFixture]
    public class BootstrapingTests
    {
        [Test]
        public async void Give_BoostrapActions_When_BootstrapperRun_Then_AllActionsShouldBeExecuted()
        {
            Bootstrapper.RegisterTasks(TypeWithRegisteredBootstrapAction.DoSomething);

            Assert.IsFalse(BootstrapState.FlagAnnotated);
            Assert.IsFalse(BootstrapState.FlagRegistered);

            await Bootstrapper.StartSync();

            Assert.IsTrue(BootstrapState.FlagAnnotated);
            Assert.IsTrue(BootstrapState.FlagRegistered);
        }
    }
}