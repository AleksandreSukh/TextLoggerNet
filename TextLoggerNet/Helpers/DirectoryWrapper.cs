using System.IO;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.Helpers
{
    public class DirectoryWrapper : IDirectoryWrapper
    {
        public string GetCurrentDirectory() { return Directory.GetCurrentDirectory(); }
        public void SetCurrentDirectory(string path) { Directory.SetCurrentDirectory(path); }
        //public void CreateDirectory(string path) { Directory.CreateDirectory(path); }
        public DirectoryInfo CreateDirectory(string path) { return Directory.CreateDirectory(path); }
        public bool Exists(string path) { return Directory.Exists(path); }
        public System.Security.AccessControl.DirectorySecurity GetAccessControl(string path) { return Directory.GetAccessControl(path); }
        public void SetAccessControl(string path, System.Security.AccessControl.DirectorySecurity sec) { Directory.SetAccessControl(path, sec); }
        public void Delete(string path2Directory, bool recursive) { Directory.Delete(path2Directory, recursive); }
        public string[] GetFiles(string path) { return Directory.GetFiles(path); }
        public string[] GetFiles(string path, string searchPattern, SearchOption searchOption) { return Directory.GetFiles(path, searchPattern, searchOption); }
        public string[] GetFiles(string path, string searchPattern) { return Directory.GetFiles(path, searchPattern); }
        public DirectoryInfo GetParent(string childDir) { return Directory.GetParent(childDir); }
        public string[] GetDirectories(string path) { return Directory.GetDirectories(path); }
        //public string[] GetDirectoriesOlderThanByName(string path, DateTime dateOlderThan ) { return Directory.GetDirectories(path, ); }
    }
}
