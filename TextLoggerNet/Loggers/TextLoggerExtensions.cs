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
        static ITextLogger _textLogger;
        public static void Init(ITextLogger textLogger) { _textLogger = textLogger; }

        public static void w2t(this string logText)
        {
#if DEBUGALL
            if (_textLogger == null)
                throw new InvalidOperationException("Textlogger must be initialized first");
#endif
            _textLogger?.WriteLine(logText);
        }

        public static void w2t(this Exception ex)
        {
#if DEBUGALL
            if (_textLogger == null)
                throw new InvalidOperationException("Textlogger must be initialized first");
#endif
            _textLogger?.WriteLine(ex);
        }
    }
}