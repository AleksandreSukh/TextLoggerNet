using System;

namespace TextLoggerNet.Interfaces
{
    public interface ILogger
    {
        void WriteLine(string logText);
        void WriteLine(Exception exception);
    }
}
