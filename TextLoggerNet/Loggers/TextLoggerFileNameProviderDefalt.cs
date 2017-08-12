using System.IO;
using TextLoggerNet.Helpers;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.Loggers
{
    public class TextLoggerFileNameProviderDefalt : ITextLoggerFileNameProvider
    {
        readonly IEnvironmentInfo _environmentInfo;

        public TextLoggerFileNameProviderDefalt(IEnvironmentInfo environmentInfo)
        {
            _environmentInfo = environmentInfo;
        }

        public string GetLogFilePath()
        {
            var exePath = new ExeLocationInfo().ExeFullPath;
            var logFileName = Path.GetFileNameWithoutExtension(exePath) + "_Log.txt";
            return Path.Combine(Path.Combine(Path.GetDirectoryName(exePath), "applog"), _environmentInfo.SessionUserNameFullNameAdaptedForFileName + "_" + logFileName);
        }
    }
}