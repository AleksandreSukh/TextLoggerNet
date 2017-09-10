using System;
using System.Globalization;
using System.Threading;
using TextLoggerNet.Helpers;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.Loggers
{
    internal class LoggerTextFormatterVersionPidDateThreadDefault : LoggerTextFormatterVersionPidDateThread
    {
        public LoggerTextFormatterVersionPidDateThreadDefault() 
            : base(new EnvironmentInfo(
                      new FileVersionInfoProvider()))
        {
        }
    }
    internal class LoggerTextFormatterVersionPidDateThread : ITextLoggerTextFormatter
    {
        readonly IEnvironmentInfo _environmentInfo;
        
        public LoggerTextFormatterVersionPidDateThread(IEnvironmentInfo environmentInfo)
        {
            _environmentInfo = environmentInfo;
            
        }

        public string FormatTextToLog(string logText)
        {
            var threadname = Thread.CurrentThread.Name;
            if (!string.IsNullOrEmpty(threadname))
                threadname += "\t";
            var logTextWithoutVersion = $"{_environmentInfo.ProcessId}\t{DateTime.Now.ToString("dd.MMM.yy hh:mm:ss.fff", CultureInfo.InvariantCulture)}\t{threadname}{logText}";
            if (_environmentInfo.FileVersionInfoCached == null)
                return "NoVersion" + logTextWithoutVersion;
            var fileMajorPart = _environmentInfo.FileVersionInfoCached.FileMajorPart;
            var fileMinorPart = _environmentInfo.FileVersionInfoCached.FileMinorPart;
            var fileBuildPart = _environmentInfo.FileVersionInfoCached.FileBuildPart;
            var filePrivatePart = _environmentInfo.FileVersionInfoCached.FilePrivatePart;
            return
                $"{fileMajorPart}.{fileMinorPart}.{fileBuildPart}.{filePrivatePart}\t" + logTextWithoutVersion;
        }


    }
}