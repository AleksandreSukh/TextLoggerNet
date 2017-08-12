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
        readonly IEnvironmentWrapper _environmentWrapper;
        readonly INativeMethodsWrapper _nativeMethodsWrapper;

        public EnvironmentInfo(IFileVersionInfoProvider fileVersionInfoProvider,
            IEnvironmentWrapper environmentWrapper,
            INativeMethodsWrapper nativeMethodsWrapper)
        {
            _fileVersionInfoProvider = fileVersionInfoProvider;
            _environmentWrapper = environmentWrapper;
            _nativeMethodsWrapper = nativeMethodsWrapper;
        }
        readonly WindowsIdentity _currentIdentity = WindowsIdentity.GetCurrent();

        #region EnvironmentInfo_Optimized
        public string ApplicationVersion => FileVersionInfoCached != null ? FileVersionInfoCached.FileVersion : CurrentProssFileVersionInfo.FileVersion;

        public IFileVersionInfoWrapper CurrentProssFileVersionInfo => _fileVersionInfoProvider.FromVersionInfo(CurrentProcess.MainModule.FileVersionInfo);

        IFileVersionInfoWrapper _fileVersionInfoStatic;
        public IFileVersionInfoWrapper FileVersionInfoCached => _fileVersionInfoStatic ?? (_fileVersionInfoStatic = CurrentProssFileVersionInfo);
        Process _currentProcess;
        public Process CurrentProcess => _currentProcess ?? (_currentProcess = Process.GetCurrentProcess());
        public string ProcessName => CurrentProcess.ProcessName;
        int? _processId;
        public int ProcessId
        {
            get
            {
                if (_processId == null) _processId = CurrentProcess.Id;
                return _processId.Value;
            }
        }

        int? _sessionId;
        public int SessionId
        {
            get
            {
                if (_sessionId == null)
                { _sessionId = CurrentProcess.SessionId; }
                return _sessionId.Value;
            }
        }
        string _sessionUsername;
        public string SessionUserName => _sessionUsername ?? (_sessionUsername = _nativeMethodsWrapper.GetUsernameBySessionId(CurrentProcess.SessionId, true) ?? string.Empty);

        string _sessionUserNameForPath;
        public string SessionUserNameFullNameAdaptedForFileName => _sessionUserNameForPath ?? (_sessionUserNameForPath = SessionUserName.ToValidFileName());
        bool? _userIsAdmin;
        public bool UserIsAdmin
        {
            get
            {
                if (_userIsAdmin == null)
                { _userIsAdmin = new WindowsPrincipal(_currentIdentity).IsInRole(WindowsBuiltInRole.Administrator); }
                return _userIsAdmin.Value;
            }
        }


        public bool UserIsSystem => _currentIdentity.IsSystem;
        public bool UserIsLocal => _environmentWrapper.MachineName == _environmentWrapper.UserDomainName;
        public DateTime ApllicationStartupTime => CurrentProcess.StartTime;

        public TimeSpan ApplicationUptime => DateTime.Now - ApllicationStartupTime;

        public long GetCurrentProcessMemoryUsage()
        {
            using (var prc = Process.GetCurrentProcess()) return prc.PrivateMemorySize64;
        }

        public string[] GetInstalledDotNetVersions()
        {
            const string path = @"SOFTWARE\Microsoft\NET Framework Setup\NDP";
            using (var installedVersions = Registry.LocalMachine.OpenSubKey(path))
            {
                var versionNames = installedVersions?.GetSubKeyNames();
                return versionNames;
            }
        }

        #endregion


        public bool IsConnectedToTheInternet()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This Method returns true if computer is connected to local network
        /// </summary>
        public bool IsConnectedToTheLocalNetwork() => NetworkInterface.GetIsNetworkAvailable();
    }

}
