using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.Helpers
{
    public class ThreadWrapper : IThreadWrapper
    {
        public void Sleep(int milliSeconds) { ThreadHelper.Sleep(milliSeconds); }
        public void Sleep(TimeSpan timeSpan) { Sleep(Convert.ToInt32(timeSpan.TotalMilliseconds)); }


        static Thread StartAndReturn(Thread thread)
        {
            thread.Start();
            return thread;
        }

        public Thread Run(ThreadStart start, string name)
        {
            var thread = new Thread(start) { Name = name };
            return StartAndReturn(thread);
        }
        public Thread Run(ThreadStart start, bool isBackground)
        {
            var thread = new Thread(start) { IsBackground = isBackground };
            return StartAndReturn(thread);
        }
        public Thread Run(ThreadStart start)
        {
            var thread = new Thread(start);
            return StartAndReturn(thread);
        }
        //TODO:Not just wrapper
        public bool RunAndJoin(ThreadStart start, int timeOut) => Run(start)?.Join(timeOut) ?? false;
        [Obsolete("Found undisposed waitHandle")]
        public void RunAndJoin(IEnumerable<ThreadStart> starts, int timeout)
        {
            if (starts == null) return;

            var wh = new WaitHandle[starts.Count()];
            var threadList = new List<Thread>();

            var i = 0;
            foreach (var threadStart in starts)
            {
                var handle = new EventWaitHandle(false, EventResetMode.ManualReset);
                threadList.Add(
                    Run(() =>
                    {
                        threadStart.Invoke();
                        handle.Set();
                    }));
                wh[i++] = handle;
            }

            WaitHandle.WaitAll(wh, timeout);
        }

        public Thread CurrentThread => Thread.CurrentThread;


    }
}