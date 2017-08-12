using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TextLoggerNet;
using TextLoggerNet.Loggers;

namespace TestSampleUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            //Default behavior of this logger is to create subdirectory (relative to exe location) and log to file with name forrmatted with userName text formatted with application version - ProcessId - ThreadName (if any) and current date

            var tl = new TextLoggerToFileDefaultEasy();
            tl.WriteLine("DefaultLog");

            //This method gives ability to change log file name 
            tl.SetFileName("CustomFile.txt");

            //You can also use the same instance with same formatting etc. to log to some another file
            tl.WriteToFile("Text to log to custom file", Path.Combine(Directory.GetCurrentDirectory(), "logInTheSameDir.txt"));
            
            //It accepts Exceptions too
            tl.WriteLine(new ArgumentException("In order to log exceptions directly to text file"));
        }
    }
}
