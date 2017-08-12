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
            : base(new FileWrapper(), new ExeLocationInfo(), new DirectoryWrapper(), new EnvironmentInfo(new FileVersionInfoProvider(), new EnvironmentWrapper(), new NativeMethodsWrapper()), new EventWaitHandleWrapperProvider(), textLoggerTextFormatter, "applog") { }
    }

    public class NativeMethodsWrapper : INativeMethodsWrapper
    {
        public string GetUsernameBySessionId(int sessionId, bool prependDomain)
        {
            return Win32ExtensionMethods.GetUsernameBySessionId(sessionId, prependDomain);
        }
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