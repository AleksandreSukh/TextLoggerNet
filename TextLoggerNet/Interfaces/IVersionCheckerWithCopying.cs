using TextLoggerNet.Helpers;

namespace TextLoggerNet.Interfaces
{
    public interface IVersionCheckerWithCopying
    {
        IFileVersionInfoWrapper TryCheckVersionOfExeWithCopying();
        bool TryCheckVersionOfExeWithCopying(out IFileVersionInfoWrapper fVExeFullPath);
    }
}