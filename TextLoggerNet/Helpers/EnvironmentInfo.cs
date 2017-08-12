using System;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Principal;
using Microsoft.Win32;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.Helpers
{
    public class ExeLocationInfo : IExeLocationInfo
    {
        public string ExeDirectory => Path.GetDirectoryName(ExeFullPath);
        static string _exeFullPath;
        //NOTE! Checked and it is safe to Lazy-load location it will keep value when process was launched
        public string ExeFullPath
            => _exeFullPath ?? (_exeFullPath = Assembly.GetEntryAssembly().Location);
        public string Exename => Path.GetFileName(ExeFullPath);

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
