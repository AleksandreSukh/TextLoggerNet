using TextLoggerNet.Helpers;
using TextLoggerNet.Interfaces;
using ExeLocationInfo = TextLoggerNet.Helpers.ExeLocationInfo;

namespace TextLoggerNet.Loggers
{
    /// <summary>
    /// Easy implementaion of <see cref="LoggerToFile"/> with dependency on <see cref="ITextLoggerTextFormatter"/>
    /// </summary>
    public class LoggerToFileDefault : LoggerToFile
    {
        public LoggerToFileDefault(ITextLoggerTextFormatter textLoggerTextFormatter)
            : base(new ExeLocationInfo(), new EnvironmentInfo(new FileVersionInfoProvider()), new EventWaitHandleWrapperProvider(), textLoggerTextFormatter, "applog") { }
    }

   

    /// <summary>
    /// Easy implementaion of <see cref="LoggerToFile"/> without dependencies
    /// </summary>
    public class LoggerToFileDefaultEasy : LoggerToFileDefault
    {
        public LoggerToFileDefaultEasy()
            : base(new LoggerTextFormatterVersionPidDateThreadDefault()) { }
    }
}