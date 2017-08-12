using System.IO;

namespace TextLoggerNet.Interfaces
{
    public interface IDirectoryWrapper
    {
        DirectoryInfo CreateDirectory(string path);
        //void CreateDirectory(string path);
        void Delete(string path2Directory, bool recursive);
        bool Exists(string path);


        System.Security.AccessControl.DirectorySecurity GetAccessControl(string path);
        string GetCurrentDirectory();
        string[] GetFiles(string path);
        string[] GetFiles(string path, string searchPattern);
        string[] GetFiles(string path, string searchPattern, SearchOption searchOption);
        void SetAccessControl(string path, System.Security.AccessControl.DirectorySecurity sec);
        void SetCurrentDirectory(string p);

        DirectoryInfo GetParent(string childDir);
        string[] GetDirectories(string path);
    }
}
