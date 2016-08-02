using System;
using System.Collections.Generic;
using System.Linq;
using LxUtilities.Services.Tests.Cache.Redis._ObjectMothers;
using NUnit.Framework;

namespace LxUtilities.Services.Tests.Cache.Redis
{
    [TestFixture]
    public class RedisCacheTests
    {
        [Test]
        public void Given_HashKeyAndHashField_When_ItsSetInCacheHash_Then_SameValueCanBeRetrievedFromCacheHash()
        {
            var hashKey = Guid.NewGuid().ToString();
            var fieldName = Guid.NewGuid().ToString();
            var fieldValue = Guid.NewGuid().ToString();

            using (var cache = CacheMother.Default())
            {
                cache.HashSet(hashKey,
                    new Dictionary<string, string>
                    {
                        {fieldName, fieldValue}
                    });
            }

            using (var cache2 = CacheMother.Default())
            {
                var cachedFields = cache2.HashGet(hashKey, fieldName);
                Assert.IsNotNull(cachedFields);
                Assert.IsTrue(cachedFields.Any());
                Assert.AreEqual(fieldValue, cachedFields.First());
            }
        }

        [Test]
        public void Given_SingleCachedItem_When_ItsSetInCache_Then_SameValueCanBeRetrievedFromCache()
        {
            var cacheKey = Guid.NewGuid().ToString();
            var cachedItem = CachedItemMother.Random();
            using (var cache = CacheMother.Default())
            {
                cache.SetCachedItem(cacheKey, cachedItem, TimeSpan.FromSeconds(10));
            }

            using (var cache2 = CacheMother.Default())
            {
                var cachedImage = cache2.GetCachedItem<CachedItem>(cacheKey);
                Assert.IsNotNull(cachedImage);
                Assert.AreEqual(cachedItem.SomeProperty, cachedImage.SomeProperty);
            }
        }
    }
}