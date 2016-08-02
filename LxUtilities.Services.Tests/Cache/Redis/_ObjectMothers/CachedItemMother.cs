using System;

namespace LxUtilities.Services.Tests.Cache.Redis._ObjectMothers
{
    public class CachedItem
    {
        public string SomeProperty { get; set; }
    }

    public static class CachedItemMother
    {
        public static CachedItem Random()
        {
            return new CachedItem
            {
                SomeProperty = Guid.NewGuid().ToString()
            };
        }
    }
}