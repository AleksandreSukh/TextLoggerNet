using System;
using System.Collections.Generic;
using System.Diagnostics;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.Loggers
{
    public static class TimeConsumingMethodLogger //: ITimeConsumingMethodLogger
    {
        static ILogger _;
        [Conditional("TIMEMON")]
        public static void Initialize(ILogger logger)
        {
            _ = logger;
        }

        static readonly object _logTimeLocker = new object();
        static readonly Dictionary<string, DateTime> _dictionary = new Dictionary<string, DateTime>();

        static Dictionary<string, DateTime> LogTime { get { lock (_logTimeLocker) return _dictionary; } }


        static void Log(string log) { _?.WriteLine(log); }


        [Conditional("TIMEMON")]
        public static void Logt(string log, int logIfMillisecondsGreaterThan = 500, string comment = null)
        {
            if (_ == null) return;
            try
            {
                var curThreadId = log.TrimEnd('2');
                if (LogTime.ContainsKey(curThreadId))
                {
                    var now = DateTime.Now;

                    if (log.EndsWith("2"))
                    {
                        var td = now - LogTime[curThreadId];
                        var milliseconds = td.TotalMilliseconds;
                        var seconds = (int)td.TotalSeconds;
                        var ext = "";
                        for (int i = 0; i < seconds; i++)
                        {
                            ext += "\t\t-----|";
                        }
                        if (milliseconds >= logIfMillisecondsGreaterThan)
                        {
                            var stringToLog = "|-|-| " + td + " " + log;
                            if (!string.IsNullOrEmpty(comment))
                                stringToLog += Environment.NewLine + comment;
                            if (!string.IsNullOrEmpty(ext))
                                stringToLog += ext;
                            Log(stringToLog);
                        }
                    }
                    LogTime[curThreadId] = now;
                }
                else
                {
                    //LogTime.Clear();
                    LogTime.Add(curThreadId, DateTime.Now);
                }

            }
            catch (Exception ex)
            {
                _.WriteLine(ex);
            }
        }
    }
}