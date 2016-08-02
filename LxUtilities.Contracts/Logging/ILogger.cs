using System;

namespace LxUtilities.Contracts.Logging
{
    /// <summary>
    ///     For general purposed loggers
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        ///     Log with normal message
        /// </summary>
        /// <param name="message"></param>
        void LogInfo(string message);

        /// <summary>
        ///     Log with trace message
        /// </summary>
        /// <param name="message"></param>
        void LogTrace(string message);

        /// <summary>
        ///     Log exception
        /// </summary>
        /// <param name="ex"></param>
        void LogException(Exception ex);

        /// <summary>
        ///     Log exception with internally used error ID
        /// </summary>
        /// <param name="exceptionId"></param>
        /// <param name="ex"></param>
        void LogException(Guid exceptionId, Exception ex);
    }
}