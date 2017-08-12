

// ReSharper disable InconsistentNaming

namespace TextLoggerNet.Interfaces
{
    public interface INativeMethodsWrapper
    {
        //uint GetLastInputTime();
        //IntPtr SetWinEventHook(uint eventSystemForeground, uint u, IntPtr zero, WinEventDelegate procDelegate, uint i, uint i1, uint wineventOutofcontext);
        //void GetMessage(ref Message message, IntPtr zero, uint i, uint i1);
        //IntPtr SetClipboardViewer(IntPtr handle);
        //void ChangeClipboardChain(IntPtr handle, IntPtr clipboardViewerNext);
        //IntPtr GetForegroundWindow();
        //void ShowConsoleWindow();
        //void HideConsoleWindow();
        //void SendMessage(IntPtr clipboardViewerNext, int msg, IntPtr wParam, IntPtr lParam);
        //string GetWindowClassName(IntPtr hwnd);
        //System.Diagnostics.Process GetWindowHandlerProcess(IntPtr hwnd);
        //string[] GetConnectedIpAddressesByProcess(int id);
        //string GetWindowText(IntPtr hwnd);
        //string GetControlText(IntPtr hwnd);
        //string GetForegroundWindowClass();
        //string GetRdpClientIp();
        //bool Is64BitOperatingSystem();
        //void Deny2KillProcess(IntPtr handle);
        //void Allow2KillProcess(IntPtr handle);
        //IntPtr GetDesktopWindow();
        //IntPtr GetWindowDC(IntPtr handle);
        //void GetWindowRect(IntPtr handle, ref RECT windowRect);
        //IntPtr CreateCompatibleDC(IntPtr hdcSrc);
        //IntPtr CreateCompatibleBitmap(IntPtr hdcSrc, int width, int height);
        //IntPtr SelectObject(IntPtr hdcDest, IntPtr hBitmap);
        //void BitBlt(IntPtr hdcDest, int i, int i1, int width, int height, IntPtr hdcSrc, int i2, int i3, int srccopy);
        //void DeleteDC(IntPtr hdcDest);
        //void ReleaseDC(IntPtr handle, IntPtr hdcSrc);
        //void DeleteObject(IntPtr hBitmap);
        //IntPtr GetFocusedWindowHandle();
        //bool IsUniversalAppWindow(IntPtr hwnd);
        //IntPtr GetCoreWindowFromUniversalAppWindow(IntPtr hwnd);
        //bool IsWindow(IntPtr windowHandle);
        //bool DesktopIsAvailableThereforeSessionUnlocked();
        //IntPtr GetProcessHandleFromWindow(IntPtr hwnd);
        //int GetProcessIdFromWindow(IntPtr hwnd);
        string GetUsernameBySessionId(int sessionId, bool prependDomain);
        //int GetWindowThreadProcessId(IntPtr hWnd, IntPtr procid);
        //bool GetGUIThreadInfo(uint hTreadId, ref GUITHREADINFO lpgui);
        //IntPtr FindWindow(string lpClassName, string lpWindowName);
        //IntPtr WTSOpenServer([MarshalAs(UnmanagedType.LPStr)] string pServerName);
        //void WTSCloseServer(IntPtr hServer);

        //int WTSEnumerateSessions(
        //    IntPtr pServer,
        //    [MarshalAs(UnmanagedType.U4)] int iReserved,
        //    [MarshalAs(UnmanagedType.U4)] int iVersion,
        //    ref IntPtr pSessionInfo,
        //    [MarshalAs(UnmanagedType.U4)] ref int iCount);

        //void WTSFreeMemory(IntPtr pMemory);
        //uint ProcessIdToSessionId(uint processId);
    }
}