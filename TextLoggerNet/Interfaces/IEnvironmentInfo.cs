using System;
using System.Diagnostics;
using TextLoggerNet.Helpers;

namespace TextLoggerNet.Interfaces
{
    public interface IEnvironmentInfo
    {
        string ApplicationVersion { get; }
        Process CurrentProcess { get; }
        IFileVersionInfoWrapper FileVersionInfoCached { get; }
        IFileVersionInfoWrapper CurrentProssFileVersionInfo { get; }
        long GetCurrentProcessMemoryUsage();
        string[] GetInstalledDotNetVersions();
        int ProcessId { get; }
        string ProcessName { get; }
        int SessionId { get; }
        string SessionUserName { get; }
        string SessionUserNameFullNameAdaptedForFileName { get; }
        bool UserIsAdmin { get; }
        bool UserIsSystem { get; }
        bool UserIsLocal { get; }
        DateTime ApllicationStartupTime { get; }
        /// <summary>
        /// Returns time spent since application startup
        /// </summary>
        TimeSpan ApplicationUptime { get; }
        bool IsConnectedToTheInternet();
        bool IsConnectedToTheLocalNetwork();
    }
}