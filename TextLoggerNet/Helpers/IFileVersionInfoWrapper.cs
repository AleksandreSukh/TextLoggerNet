namespace TextLoggerNet.Helpers
{
    public interface IFileVersionInfoWrapper
    {
        int FileMajorPart { get; set; }
        int FileMinorPart { get; set; }
        int FileBuildPart { get; set; }
        int FilePrivatePart { get; set; }
        string FileVersion { get; }
        string FileVersionWithoutZeros { get; }
    }
}