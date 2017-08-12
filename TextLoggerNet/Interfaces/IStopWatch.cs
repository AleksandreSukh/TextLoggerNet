using System;

namespace TextLoggerNet.Interfaces
{
    public interface IStopWatch
    {
        void Reset();
        void Start();
        void Stop();
        TimeSpan Elapsed { get; }
    }
}