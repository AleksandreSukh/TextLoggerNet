using System;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.Loggers
{
    public class ExceptionLoggerToTextLogger : IExceptionLogger
    {
        readonly ITextLogger _textLogger;

        public ExceptionLoggerToTextLogger(ITextLogger textLogger)
        {
            _textLogger = textLogger;
        }

        public void LogException(Exception exception)
        {
            _textLogger.WriteLine(exception);
        }
    }
}