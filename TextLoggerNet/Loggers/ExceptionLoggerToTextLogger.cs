using System;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.Loggers
{
    public class ExceptionLoggerToTextLogger : IExceptionLogger
    {
        readonly ILogger _logger;

        public ExceptionLoggerToTextLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void LogException(Exception exception)
        {
            _logger.WriteLine(exception);
        }
    }
}