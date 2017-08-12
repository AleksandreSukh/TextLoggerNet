using System;
using System.Globalization;
using System.Threading;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.Loggers
{
    public class LoggerTextFormatterPidDateThread : ITextLoggerTextFormatter
    {
        readonly IEnvironmentInfo _environmentInfo;
        
        public LoggerTextFormatterPidDateThread(IEnvironmentInfo environmentInfo)
        {
            _environmentInfo = environmentInfo;
            
        }

        public string FormatTextToLog(string logText)
        {
            var threadname = Thread.CurrentThread.Name;
            if (!string.IsNullOrEmpty(threadname))
                threadname += "\t";
            var logTextWithoutVersion = $"{_environmentInfo.ProcessId}\t{DateTime.Now.ToString("dd.MMM.yy hh:mm:ss.fff", CultureInfo.InvariantCulture)}\t{threadname}{logText}";
            return logTextWithoutVersion;
        }

    }
}