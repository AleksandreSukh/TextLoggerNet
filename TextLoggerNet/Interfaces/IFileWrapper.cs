using System;
using System.IO;

namespace TextLoggerNet.Interfaces
{
    public interface IFileWrapper
    {

        bool Exists(string path);
        FileStream Create(string path);
        void Delete(string destPath);
        FileStream OpenRead(string sourceFilePath);
        void Move(string source, string destination);
        void Copy(string source, string destination, bool overwrite = false);
        string ReadAllText(string path);
        void WriteAllText(string path, string contents);
        FileAttributes GetAttributes(string path);
        void SetAttributes(string path, FileAttributes attributes);
        DateTime GetLastWriteTime(string fileName);
        DateTime GetCreationTime(string file);
        Stream Open(string path, FileMode open, FileAccess read, FileShare readWrite);
    }
}