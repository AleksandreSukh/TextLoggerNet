using System;
using System.IO;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.Helpers
{
    public class FileWrapper : IFileWrapper
    {
        public bool Exists(string path) => File.Exists(path);
        public FileStream Create(string path) => File.Create(path);
        public void Delete(string destPath) => File.Delete(destPath);
        public FileStream OpenRead(string sourceFilePath) => File.OpenRead(sourceFilePath);
        public void Move(string source, string destination) => File.Move(source, destination);
        public void Copy(string source, string destination, bool overwrite = false) => File.Copy(source, destination, overwrite);
        public string ReadAllText(string path) { return File.ReadAllText(path); }
        public void WriteAllText(string path, string contents) { File.WriteAllText(path, contents); }
        public DateTime GetLastWriteTime(string fileName) { return File.GetLastWriteTime(fileName); }
        public DateTime GetCreationTime(string file) { return File.GetCreationTime(file); }
        public Stream Open(string path, FileMode open, FileAccess read, FileShare readWrite)
            => File.Open(path, open, read, readWrite);
        public void SetAttributes(string path, FileAttributes attributes)
            => File.SetAttributes(path, attributes);
        public FileAttributes GetAttributes(string path)
            => File.GetAttributes(path);
    }
}