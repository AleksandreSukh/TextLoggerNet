using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace TextLoggerNet.Helpers
{
    public static class Win32
    {
        [DllImport("Wtsapi32.dll")]
        public static extern bool WTSQuerySessionInformation(
            IntPtr pServer,
            int iSessionID,
            WTS_INFO_CLASS oInfoClass,
            out IntPtr pBuffer,
            out uint iBytesReturned);

        [DllImport("wtsapi32.dll")]
        public static extern void WTSFreeMemory(
            IntPtr pMemory);
    }
    public enum WTS_INFO_CLASS
    {
        WTSInitialProgram,
        WTSApplicationName,
        WTSWorkingDirectory,
        WTSOEMId,
        WTSSessionId,
        WTSUserName,
        WTSWinStationName,
        WTSDomainName,
        WTSConnectState,
        WTSClientBuildNumber,
        WTSClientName,
        WTSClientDirectory,
        WTSClientProductId,
        WTSClientHardwareId,
        WTSClientAddress,
        WTSClientDisplay,
        WTSClientProtocolType,
        WTSIdleTime,
        WTSLogonTime,
        WTSIncomingBytes,
        WTSOutgoingBytes,
        WTSIncomingFrames,
        WTSOutgoingFrames,
        WTSClientInfo,
        WTSSessionInfo,
        WTSConfigInfo,
        WTSValidationInfo,
        WTSSessionAddressV4,
        WTSIsRemoteSession
    }


}
