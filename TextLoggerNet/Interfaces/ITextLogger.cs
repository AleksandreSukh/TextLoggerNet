using System;

namespace TextLoggerNet.Interfaces
{
    public interface ITextLogger
    {
        void WriteLine(string logText);
        void WriteLine(Exception exception);
    }
}
