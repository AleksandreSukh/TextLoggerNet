using TextLoggerNet.Helpers;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.Loggers
{
    /// <summary>
    /// Easy implementaion of <see cref="TextLoggerToFile"/> with dependency on <see cref="ITextLoggerTextFormatter"/>
    /// </summary>
    public class TextLoggerToFileDefault : TextLoggerToFile
    {
        public TextLoggerToFileDefault(ITextLoggerTextFormatter textLoggerTextFormatter)
            : base(new ExeLocationInfo(), new EnvironmentInfo(new FileVersionInfoProvider()), new EventWaitHandleWrapperProvider(), textLoggerTextFormatter, "applog") { }
    }

   

    /// <summary>
    /// Easy implementaion of <see cref="TextLoggerToFile"/> without dependencies
    /// </summary>
    public class TextLoggerToFileDefaultEasy : TextLoggerToFileDefault
    {
        public TextLoggerToFileDefaultEasy()
            : base(new LoggerTextFormatterVersionPidDateThreadDefault()) { }
    }
}