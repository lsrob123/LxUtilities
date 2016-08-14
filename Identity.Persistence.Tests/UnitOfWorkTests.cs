using System;
using Identity.Domain;
using Identity.Domain.Entities;
using Identity.Domain.ValueObjects;
using LxUtilities.Services.Bootstrapping;
using LxUtilities.Services.Caching.Redis;
using LxUtilities.Services.Mapping.AutoMapper;
using LxUtilities.Services.Serialization;
using NUnit.Framework;

namespace Identity.Persistence.Tests
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        public UnitOfWorkTests()
        {
            Bootstrapper.StartSync().Wait();
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void Given_User_When_SetUserCalled_Then_UserPersisted(bool bypassCache)
        {
            var user = new User(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), new AccountStatus());

            var serializer = new JsonSerializer();
            var mappingService = new MappingService();

            var unitOfWorkFactory = new IdentityUnitOfWorkFactory("name=Identity", () => new Cache(serializer),
                mappingService);
            unitOfWorkFactory.Execute(uow => { uow.SetUser(user, bypassCache); });

            User readBackUser = null;
            unitOfWorkFactory.Execute(uow => { readBackUser = uow.GetUser(user.Key, bypassCache); });

            Assert.IsNotNull(readBackUser);
            Assert.AreEqual(user.Key, readBackUser.Key);
        }
    }
}