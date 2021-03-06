﻿using System;
using LxUtilities.Definitions.Bootstrapping;
using LxUtilities.Definitions.Core.Domain.Entity;
using LxUtilities.Definitions.Persistence;
using LxUtilities.Services.Bootstrapping;
using LxUtilities.Services.Mapping.AutoMapper;
using NUnit.Framework;

namespace LxUtilities.Services.Tests.Mapping
{
    public class SomeValueObject
    {
        public Guid SomeData { get; set; }
    }

    public class SomeEntity : EntityBase
    {
        public SomeEntity()
        {
        }

        public SomeEntity(Guid key, Guid someData) : base(key)
        {
            SomeData = someData;
        }

        public Guid SomeData { get; protected set; }
    }


    [TestFixture]
    public class MappingTests
    {
        [BootstrapAction]
        public static void CreateMaps()
        {
            MappingService.AddMaps();
        }

        static MappingTests()
        {
            Bootstrapper.Start();
        }

        [Test]
        public void Given_EntityObject_When_CallMapMethod_Then_ShouldBeMappedToExpectedValueObject()
        {
            var mappingService = new MappingService();

            var someEntity = new SomeEntity(Guid.NewGuid(), Guid.NewGuid());

            var valueObject = mappingService.Map<SomeValueObject>(someEntity);

            Assert.AreEqual(someEntity.SomeData, valueObject.SomeData);
        }

    }
}