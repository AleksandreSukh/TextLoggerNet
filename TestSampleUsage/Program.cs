using System;
using System.IO;
using System.Windows.Forms;
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

            //UnhandledExceptionLogger sample
            //Subscribing at entry point of an application (or if application uses more than one Application domain like windows service then use this code at entry points of every app domain)
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => UnhandledExceptionLogger.UnhandledExceptionHandler(e, false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //Subscribe to handle exceptions which are thrown from other threads
            Application.ThreadException += (sender, e) => UnhandledExceptionLogger.UnhandledExceptionHandler(e, true);

            //TODO:Add samples for all loggers
        }
    }
}
