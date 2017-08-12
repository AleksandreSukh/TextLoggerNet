using System.Threading;

namespace TextLoggerNet.Helpers
{
    public class MonitorHelper
    {
        public static void TryRunInTime(Fiction action, object locker, int timeout)
        {
            if (!Monitor.TryEnter(locker, timeout)) return;
            try { action(); }
            finally { Monitor.Exit(locker); }
        }
    }
}

