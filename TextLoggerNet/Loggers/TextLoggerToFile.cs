using System;
using System.Diagnostics;
using System.IO;
using TextLoggerNet.Helpers;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.Loggers
{
    public class TextLoggerToFile : ITextLoggerToFile
    {
        readonly ITextLoggerTextFormatter _textFormatter;
        
        readonly IExeLocationInfo _exeLocationInfo;
        readonly IEnvironmentInfo _environmentInfo;
        
        readonly IEventWaitHandleWrapperProvider _eventWaitHandleWrapperProvider;
        readonly string _applicationLogDirectoryName;
        public TextLoggerToFile(IExeLocationInfo exeLocationInfo,
            IEnvironmentInfo environmentInfo,
            IEventWaitHandleWrapperProvider eventWaitHandleWrapperProvider,
            ITextLoggerTextFormatter textLoggerTextFormatter, string applicationLogDirectoryName)
        {
            _exeLocationInfo = exeLocationInfo;
            _environmentInfo = environmentInfo;
            _eventWaitHandleWrapperProvider = eventWaitHandleWrapperProvider;
            _textFormatter = textLoggerTextFormatter;
            if (string.IsNullOrEmpty(applicationLogDirectoryName))
                throw new ArgumentNullException($"Argument by name:{nameof(applicationLogDirectoryName)} should not be null or empty");
            _applicationLogDirectoryName = applicationLogDirectoryName;
            _logFileName = Path.GetFileNameWithoutExtension(_exeLocationInfo.ExeFullPath) + "_Log.txt";
        }

        string _logFileName = "untitled_Log.txt";
        //TODO:Add capability to get fileName with a func to auto generate for example daily or hourly logs
        //To set custom file name for the log othervise executing assembly name + log.txt is used
        public void SetFileName(string fileName)
        {
            _logFileName = fileName;
        }

        string LogFileDir => Path.Combine(_exeLocationInfo.ExeDirectory, _applicationLogDirectoryName);//);

        string LogFilePath => Path.Combine(LogFileDir, _environmentInfo.SessionUserNameFullNameAdaptedForFileName + "_" + _logFileName);

        public void WriteToFile(string text, string logFilePath)
        {
            var handleAcquired = false;
            var waitHandleName = "SHARED_BY_ALL_PROCESSES_" + _logFileName;
            var waitHandle = _eventWaitHandleWrapperProvider.New(true, System.Threading.EventResetMode.AutoReset, waitHandleName);

            try
            {
                var formatted = _textFormatter.FormatTextToLog(text);

                //var formattedWithEndline = formatted + Endline;

                handleAcquired = waitHandle.WaitOne(10000, false);
                var directoryName = Path.GetDirectoryName(logFilePath);
                if (!System.IO.Directory.Exists(directoryName))
                    System.IO.Directory.CreateDirectory(directoryName);
                if (!System.IO.File.Exists(logFilePath))
                    System.IO.File.Create(logFilePath).Close();
                //if (Debugger.IsAttached)
                //    Console.WriteLine(formatted);
                using (var writer = new StreamWriter(logFilePath, true))
                    writer.WriteLine(formatted);
                //writer.WriteLine(formattedWithEndline);
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                    Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (handleAcquired)
                    waitHandle.Set();
            }
            DeleteLogIfTooBigSize(logFilePath);
        }

        void DeleteLogIfTooBigSize(string logFilePath)
        {
            try
            {
                if (!System.IO.File.Exists(logFilePath))
                    return;

                if (new FileInfo(logFilePath).Length <= 20000000) return;
                try { System.IO.File.Delete(logFilePath); }
                catch (Exception e)
                { System.IO.File.Move(logFilePath, logFilePath + ".DelErr"); }
            }
            catch (Exception)
            {
#if DEBUG
                throw;
#endif
            }
        }

        public void WriteLine(string logText)
        {
            WriteToFile(logText, LogFilePath);
        }

        public void WriteLine(Exception exception)
        {
            WriteToFile($"Exception: {exception}", LogFilePath);
        }
    }
}
