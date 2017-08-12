using System;
using System.Diagnostics;
using TextLoggerNet.Helpers;

namespace TextLoggerNet.Interfaces
{
    public interface IEnvironmentInfo
    {
        IFileVersionInfoWrapper FileVersionInfoCached { get; }
        int ProcessId { get; }
        string SessionUserNameFullNameAdaptedForFileName { get; }
    }
}