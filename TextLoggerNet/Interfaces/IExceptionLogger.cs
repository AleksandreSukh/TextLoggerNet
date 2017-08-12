using System;

namespace TextLoggerNet.Interfaces
{
    public interface IExceptionLogger
    {
        void LogException(Exception exception);
    }
}
