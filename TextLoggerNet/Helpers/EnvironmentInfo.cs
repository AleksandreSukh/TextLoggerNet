using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using TextLoggerNet.Interfaces;
using IExeLocationInfo = TextLoggerNet.Interfaces.IExeLocationInfo;

namespace TextLoggerNet.Helpers
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
    public interface IFileVersionInfoProviderEx : IFileVersionInfoProvider
    {
        IFileVersionInfoWrapper GetVersionInfo(string path);
    }
    public class FileVersionInfoProviderEx : FileVersionInfoProvider, IFileVersionInfoProviderEx
    {
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
    public interface IFileVersionInfoWrapper
    {
        int FileMajorPart { get; set; }
        int FileMinorPart { get; set; }
        int FileBuildPart { get; set; }
        int FilePrivatePart { get; set; }
        string FileVersion { get; }
        string FileVersionWithoutZeros { get; }
    }
    public interface IFileVersionInfoProvider
    {
        IFileVersionInfoWrapper FromVersionInfo(FileVersionInfo info);
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
    public class ExeLocationInfo : IExeLocationInfo
    {
        public string ExeDirectory => Path.GetDirectoryName(ExeFullPath);
        static string _exeFullPath;
        //NOTE! Checked and it is safe to Lazy-load location it will keep value when process was launched
        public string ExeFullPath
            => _exeFullPath ?? (_exeFullPath = Assembly.GetEntryAssembly().Location);
        public string Exename => Path.GetFileName(ExeFullPath);

    }
    public interface IEnvironmentInfo
    {
        IFileVersionInfoWrapper FileVersionInfoCached { get; }
        int ProcessId { get; }
        string SessionUserNameFullNameAdaptedForFileName { get; }
    }
    public class EnvironmentInfo : IEnvironmentInfo
    {
        readonly IFileVersionInfoProvider _fileVersionInfoProvider;

        public EnvironmentInfo(IFileVersionInfoProvider fileVersionInfoProvider)
        {
            _fileVersionInfoProvider = fileVersionInfoProvider;
        }

        #region EnvironmentInfo_Optimized

        public IFileVersionInfoWrapper CurrentProssFileVersionInfo => _fileVersionInfoProvider.FromVersionInfo(CurrentProcess.MainModule.FileVersionInfo);

        IFileVersionInfoWrapper _fileVersionInfoStatic;
        public IFileVersionInfoWrapper FileVersionInfoCached => _fileVersionInfoStatic ?? (_fileVersionInfoStatic = CurrentProssFileVersionInfo);
        Process _currentProcess;
        public Process CurrentProcess => _currentProcess ?? (_currentProcess = Process.GetCurrentProcess());
        int? _processId;
        public int ProcessId
        {
            get
            {
                if (_processId == null) _processId = CurrentProcess.Id;
                return _processId.Value;
            }
        }

        string _sessionUsername;
        public string SessionUserName => _sessionUsername ?? (_sessionUsername = Win32ExtensionMethods.GetUsernameBySessionId(CurrentProcess.SessionId, true) ?? string.Empty);

        string _sessionUserNameForPath;
        public string SessionUserNameFullNameAdaptedForFileName => _sessionUserNameForPath ?? (_sessionUserNameForPath = SessionUserName.ToValidFileName());
        public DateTime ApllicationStartupTime => CurrentProcess.StartTime;

        #endregion

    }
}
