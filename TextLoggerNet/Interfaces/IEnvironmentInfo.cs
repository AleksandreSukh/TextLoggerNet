using System;
using System.Diagnostics;
using TextLoggerNet.Helpers;

namespace TextLoggerNet.Interfaces
{
    public interface IEnvironmentInfo
    {
        Process CurrentProcess { get; }
        IFileVersionInfoWrapper FileVersionInfoCached { get; }
        IFileVersionInfoWrapper CurrentProssFileVersionInfo { get; }
        int ProcessId { get; }
        string SessionUserName { get; }
        string SessionUserNameFullNameAdaptedForFileName { get; }
    }
}