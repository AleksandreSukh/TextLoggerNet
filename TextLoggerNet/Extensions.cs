using System.IO;

namespace TextLoggerNet
{
    public static class Extensions
    {
        public static string ToValidFileName(this string stringToFileName)
        {
            string result = stringToFileName;
            foreach (var nameChar in Path.GetInvalidFileNameChars())
                result = result.Replace(nameChar, '_');
            return result;
        }
    }
}
