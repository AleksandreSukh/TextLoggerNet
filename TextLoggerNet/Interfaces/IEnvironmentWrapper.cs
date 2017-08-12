using System;

namespace TextLoggerNet.Interfaces
{
    public interface IEnvironmentWrapper
    {
        void Exit(int e);
        int GetTickCount();
        string CurrentDirectory { get; }
        string StackTrace { get; }
        string MachineName { get; }
        string UserDomainName { get; }
        string GetFolderPath(System.Environment.SpecialFolder folder);
        string SystemDirectory { get; }
        OperatingSystem OSVersion { get; }
        string GetEnvironmentVariable(string variable);
    }
}