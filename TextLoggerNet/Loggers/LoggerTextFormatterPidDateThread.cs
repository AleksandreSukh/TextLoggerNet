using System.Globalization;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.Loggers
{
    public class LoggerTextFormatterPidDateThread : ITextLoggerTextFormatter
    {
        readonly IThreadWrapper _threadWrapper;
        readonly IEnvironmentInfo _environmentInfo;
        readonly IDateTime _dateTime;
        public LoggerTextFormatterPidDateThread(IThreadWrapper threadWrapper, IEnvironmentInfo environmentInfo, IDateTime dateTime)
        {
            _threadWrapper = threadWrapper;
            _environmentInfo = environmentInfo;
            _dateTime = dateTime;
        }

        public string FormatTextToLog(string logText)
        {
            var threadname = _threadWrapper.CurrentThread.Name;
            if (!string.IsNullOrEmpty(threadname))
                threadname += "\t";
            var logTextWithoutVersion = $"{_environmentInfo.ProcessId}\t{_dateTime.Now.ToString("dd.MMM.yy hh:mm:ss.fff", CultureInfo.InvariantCulture)}\t{threadname}{logText}";
            return logTextWithoutVersion;
        }

    }
}