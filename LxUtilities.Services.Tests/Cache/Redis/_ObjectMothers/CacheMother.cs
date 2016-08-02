using LxUtilities.Services.Serialization;

namespace LxUtilities.Services.Tests.Cache.Redis._ObjectMothers
{
    public static class CacheMother
    {
        public static Caching.Redis.Cache Default()
        {
            return new Caching.Redis.Cache(LoggerMother.DoNothingLogger(), new JsonSerializer());
        }
    }
}