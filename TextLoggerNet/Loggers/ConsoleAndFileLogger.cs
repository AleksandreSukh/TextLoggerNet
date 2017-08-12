using System;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.Loggers
{
    public class ConsoleAndFileLogger : ConsoleLogger, ITextLoggerToFile
    {
        readonly ITextLoggerToFile _toFile;
        public ConsoleAndFileLogger(IDebugModeStateMarker debugModeStateMarker,
            IEnvironmentInfo environmentInfo,
            IDateTime dateTime,
            ITextLoggerToFile textLoggerToFile, ITextLoggerTextFormatter textLoggerTextFormatter)
            : base(debugModeStateMarker, environmentInfo, dateTime, textLoggerTextFormatter)
        {
            _toFile = textLoggerToFile;
        }


        public override void WriteLine(Exception exception)
        {
            try
            {
                WriteLine(exception.ToString());
            }
            catch (Exception ex) { WriteLine(ex.ToString()); }//Log text about failure in logging exception
        }

        //public void SetFileName(string fileName)
        //{
        //    _toFile.SetFileName(fileName);
        //}

        public override void WriteLine(string logText)
        {
            try
            {
                var text = TextLoggerTextFormatter.FormatTextToLog(logText);

                try
                {
                    //Formatting text twice _ To file formats similarly without version
                    WriteLineWithoutFormatting(text);
                }
                catch { /* ignored */ }

                try { _toFile.WriteLine(logText); }
                catch { /* ignored */ }
            }
            catch { /* ignored */ }
        }
    }
}