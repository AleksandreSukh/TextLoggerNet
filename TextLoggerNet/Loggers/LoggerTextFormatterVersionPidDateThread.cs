using System;
using System.Diagnostics;
using System.Globalization;
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
    public class DateTimeWrapper : IDateTime
    {
        public System.DateTime Now => System.DateTime.Now;
    }
    public class EnvironmentWrapper : IEnvironmentWrapper
    {
        public int GetTickCount() => Environment.TickCount;

        public string CurrentDirectory
        {
            get { return Environment.CurrentDirectory; }
            set { Environment.CurrentDirectory = value; }
        }

        public string StackTrace => Environment.StackTrace;
        public string MachineName => Environment.MachineName;
        public string UserDomainName => Environment.UserDomainName;

        public void Exit(int e) => Environment.Exit(e);

        public string GetFolderPath(Environment.SpecialFolder folder) => Environment.GetFolderPath(folder);

        public string SystemDirectory => Environment.SystemDirectory;

        public string GetEnvironmentVariable(string variable) => Environment.GetEnvironmentVariable(variable);

        public OperatingSystem OSVersion => Environment.OSVersion;
    }

    public class LoggerTextFormatterVersionPidDateThreadDefault : LoggerTextFormatterVersionPidDateThread
    {
        public LoggerTextFormatterVersionPidDateThreadDefault() 
            : base(new ThreadWrapper(), 
                  new EnvironmentInfo(
                      new FileVersionInfoProvider(), 
                      new EnvironmentWrapper(), 
                      new NativeMethodsWrapper()), 
                  new DateTimeWrapper())
        {
        }
    }
    public class LoggerTextFormatterVersionPidDateThread : ITextLoggerTextFormatter
    {
        readonly IThreadWrapper _threadWrapper;
        readonly IEnvironmentInfo _environmentInfo;
        readonly IDateTime _dateTime;
        public LoggerTextFormatterVersionPidDateThread(IThreadWrapper threadWrapper, IEnvironmentInfo environmentInfo, IDateTime dateTime)
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