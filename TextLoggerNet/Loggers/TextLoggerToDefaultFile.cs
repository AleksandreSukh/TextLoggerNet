using TextLoggerNet.Helpers;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.Loggers
{
    public class TextLoggerToDefaultFile : TextLoggerToFile
    {
        public TextLoggerToDefaultFile(IFileWrapper fileWrapper, IExeLocationInfo exeLocationInfo, IDirectoryWrapper directoryWrapper, IEnvironmentInfo environmentInfo, IEventWaitHandleWrapperProvider eventWaitHandleWrapperProvider, ITextLoggerTextFormatter textLoggerTextFormatter) : base(fileWrapper, exeLocationInfo, directoryWrapper, environmentInfo, eventWaitHandleWrapperProvider, textLoggerTextFormatter, "applog") { }
    }
}