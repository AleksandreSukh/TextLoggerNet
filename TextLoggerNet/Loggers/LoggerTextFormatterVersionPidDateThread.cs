using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using TextLoggerNet.Helpers;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.Loggers
{
    [Serializable]
    public class FileVersionInfoWrapper : IFileVersionInfoWrapper
    {
        FileVersionInfoWrapper()//For Serialization
        { }


        FileVersionInfoWrapper(int fileMajorPart, int fileMinorPart, int fileBuildPart, int filePrivatePart)//I Made this private to request for versionStringInConstructor (To write sortable string in sessionLog)
        {
            FileMajorPart = fileMajorPart;
            FileMinorPart = fileMinorPart;
            FileBuildPart = fileBuildPart;
            FilePrivatePart = filePrivatePart;
        }
        public FileVersionInfoWrapper(int fileMajorPart, int fileMinorPart, int fileBuildPart, int filePrivatePart, string versionString)
            : this(fileMajorPart, fileMinorPart, fileBuildPart, filePrivatePart)
        {
            _stringValue = versionString;
        }
        public int FileMajorPart { get; set; }
        public int FileMinorPart { get; set; }
        public int FileBuildPart { get; set; }
        public int FilePrivatePart { get; set; }
        //public string FileVersion => $"{FileMajorPart}.{FileMinorPart}.{FileBuildPart}.{FilePrivatePart}";
        public string FileVersion => _stringValue ?? FileVersionWithoutZeros;
        public string FileVersionWithoutZeros => $"{FileMajorPart}.{FileMinorPart}.{FileBuildPart}.{FilePrivatePart}";
        readonly string _stringValue;
    }

    public class FileVersionInfoProvider : IFileVersionInfoProvider
    {
        public IFileVersionInfoWrapper GetVersionInfo(string path)
        {
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(path);
            var foundVersion = new Version(
                fileVersionInfo.FileMajorPart,
                fileVersionInfo.FileMinorPart,
                fileVersionInfo.FileBuildPart,
                fileVersionInfo.FilePrivatePart
            );

            return FromVersion(foundVersion);
        }
        public IFileVersionInfoWrapper FromVersionInfo(FileVersionInfo info)
        {
            return new FileVersionInfoWrapper(
                info.FileMajorPart,
                info.FileMinorPart,
                info.FileBuildPart,
                info.FilePrivatePart,
                info.FileVersion);
        }
        IFileVersionInfoWrapper FromVersion(Version info)
        {
            return new FileVersionInfoWrapper(
                info.Major,
                info.Minor,
                info.Build,
                info.Revision,
                $"{info.Major}.{info.Minor}.{info.Build}.{info.Revision}");
        }
    }
   

    public class LoggerTextFormatterVersionPidDateThreadDefault : LoggerTextFormatterVersionPidDateThread
    {
        public LoggerTextFormatterVersionPidDateThreadDefault() 
            : base(new EnvironmentInfo(
                      new FileVersionInfoProvider()))
        {
        }
    }
    public class LoggerTextFormatterVersionPidDateThread : ITextLoggerTextFormatter
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