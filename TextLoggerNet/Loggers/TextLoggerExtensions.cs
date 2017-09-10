using System;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.Loggers
{
    /// <summary>
    /// This class provides capability of using ItextLogger instance in a static context
    /// (When passing logger as a dependency is not an option) or a good idea
    /// </summary>
    public static class TextLoggerExtensions
    {
        static ILogger _logger;
        public static void Init(ILogger logger) { _logger = logger; }

        public static void w2t(this string logText)
        {
#if DEBUGALL
            if (_logger == null)
                throw new InvalidOperationException("Textlogger must be initialized first");
#endif
            _logger?.WriteLine(logText);
        }

        public static void w2t(this Exception ex)
        {
#if DEBUGALL
            if (_logger == null)
                throw new InvalidOperationException("Textlogger must be initialized first");
#endif
            _logger?.WriteLine(ex);
        }
    }
}