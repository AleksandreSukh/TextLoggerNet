namespace TextLoggerNet.Loggers
{
    public class TextLoggerFileNameProviderEasy : ITextLoggerFileNameProvider
    {
        readonly string _filePah;

        public TextLoggerFileNameProviderEasy(string filePah)
        {
            _filePah = filePah;
        }

        public string GetLogFilePath()
        {
            return _filePah;
        }
    }
}