using System;
using LxUtilities.Definitions.Logging;

namespace LxUtilities.Services.Tests.Cache.Redis._ObjectMothers
{
    public class DoNothingLogger : ILogger
    {
        public void LogInfo(string message)
        {
        }

        public void LogTrace(string message)
        {
        }

        public void LogException(Exception ex)
        {
        }

        public void LogException(Guid exceptionId, Exception ex)
        {
        }
    }

    public static class LoggerMother
    {
        public static ILogger DoNothingLogger()
        {
            return new DoNothingLogger();
        }
    }
}