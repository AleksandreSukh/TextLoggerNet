using System;
using System.Collections.Generic;
using System.Threading;

namespace TextLoggerNet.Interfaces
{
    public interface IThreadWrapper
    {
        void Sleep(int milliSeconds);
        void Sleep(TimeSpan timeSpan);
        Thread Run(ThreadStart start);
        Thread Run(ThreadStart start, string name);
        Thread Run(ThreadStart start, bool isBackground);
        bool RunAndJoin(ThreadStart start, int timeout);
        void RunAndJoin(IEnumerable<ThreadStart> starts, int timeout);
        Thread CurrentThread { get; }
    }
}