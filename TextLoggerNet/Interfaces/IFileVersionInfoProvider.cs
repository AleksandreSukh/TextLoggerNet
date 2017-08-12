using System.Diagnostics;
using TextLoggerNet.Helpers;

namespace TextLoggerNet.Interfaces
{
    public interface IFileVersionInfoProvider
    {
        IFileVersionInfoWrapper FromVersionInfo(FileVersionInfo info);
        IFileVersionInfoWrapper GetVersionInfo(string path);
    }
}