using System;
using System.Runtime.InteropServices;

namespace TextLoggerNet.Helpers
{
    public static class Win32ExtensionMethods
    {
        public static string GetUsernameBySessionId(int sessionId, bool prependDomain)
        {
            IntPtr buffer;
            uint strLen;
            string username = "SYSTEM";
            if (Win32.WTSQuerySessionInformation(IntPtr.Zero, sessionId,  WTS_INFO_CLASS.WTSUserName, out buffer, out strLen) && strLen > 1)
            {
                username = Marshal.PtrToStringAnsi(buffer);
                Win32.WTSFreeMemory(buffer);
                if (prependDomain)
                {
                    if (Win32.WTSQuerySessionInformation(IntPtr.Zero, sessionId, WTS_INFO_CLASS.WTSDomainName, out buffer, out strLen) && strLen > 1)
                    {
                        username = Marshal.PtrToStringAnsi(buffer) + "\\" + username;
                        Win32.WTSFreeMemory(buffer);
                    }
                }
            }
            return username;
        }
    }
}
