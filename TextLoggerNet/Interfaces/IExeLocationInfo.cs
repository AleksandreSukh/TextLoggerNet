namespace TextLoggerNet.Interfaces
{
    public interface IExeLocationInfo
    {
        string ExeDirectory { get; }
        string ExeFullPath { get; }
        string Exename { get; }
    }
}
