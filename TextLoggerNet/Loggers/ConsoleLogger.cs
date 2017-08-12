using System;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.Loggers
{
    public class FakeLogger : ITextLogger
    {
        public void WriteLine(string logText)
        {
        }

        public void WriteLine(Exception exception)
        {
        }
    }
    public class ConsoleLoggerEasy : ITextLogger
    {
        public void WriteLine(string logText)
        {
            Console.WriteLine(logText);
        }

        public void WriteLine(Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
    public class ConsoleLogger : ITextLogger
    {
        //Dependencies
        protected readonly ITextLoggerTextFormatter TextLoggerTextFormatter;
        // ReSharper disable once InconsistentNaming

        public ConsoleLogger(ITextLoggerTextFormatter textLoggerTextFormatter)
        {
            TextLoggerTextFormatter = textLoggerTextFormatter;
        }

        //RedirectorMethods
        public virtual void WriteLine(Exception exception)
        {
            WriteLine(exception.ToString());
        }
        public virtual void WriteLine(string logText)
        { WriteLineWithoutFormatting(TextLoggerTextFormatter.FormatTextToLog(logText)); }

        protected void WriteLineWithoutFormatting(string text)
        { Console.WriteLine(text + Environment.NewLine); }

        //Actual Logic
    }
}
