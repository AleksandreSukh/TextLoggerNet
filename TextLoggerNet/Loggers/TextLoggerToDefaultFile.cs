using TextLoggerNet.Helpers;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.Loggers
{
    public class TextLoggerToDefaultFile : TextLoggerToFile
    {
        public TextLoggerToDefaultFile(IExeLocationInfo exeLocationInfo, IEnvironmentInfo environmentInfo, IEventWaitHandleWrapperProvider eventWaitHandleWrapperProvider, ITextLoggerTextFormatter textLoggerTextFormatter) : base(exeLocationInfo, environmentInfo, eventWaitHandleWrapperProvider, textLoggerTextFormatter, "applog") { }
    }
}