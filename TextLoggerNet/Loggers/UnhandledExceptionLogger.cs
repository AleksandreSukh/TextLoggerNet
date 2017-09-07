using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using TextLoggerNet.Helpers;

namespace TextLoggerNet.Loggers
{
    /// <summary>
    /// This class provides a way to avoid application crash on unhandled exception and log it silently
    /// </summary>
    public class UnhandledExceptionLogger
    {
        /// <summary>
        /// Event handler for AppDomain-s UnhandledException event and <see cref="Application.ThreadException"/>
        /// </summary>
        /// <param name="e">ExceptionEventArgs - this may be of type <see cref="UnhandledExceptionEventArgs"/> or <see cref="ThreadExceptionEventArgs"/></param>
        /// <param name="isThreadException">In order to log exception info from <see cref="ThreadExceptionEventArgs"/> this parameter should be true when subscribed to <see cref="Application.ThreadException"/> </param>
        /// <param name="exitCodeAfter">Exit code which will be returned after excepiont is logged and application exits</param>
        /// <param name="meta">Extra information to append to exception log</param>
        /// <param name="useDirectrory">Optional - custom folder name where exception log file will be created</param>
        public static void UnhandledExceptionHandler(object e, bool isThreadException, int exitCodeAfter, string meta = null, string useDirectrory = "applog")
        {
#if DEBUG
            throw (e as UnhandledExceptionEventArgs)?.ExceptionObject as Exception;
#endif
            try
            {
                string currentDir;
                var currentExeName = "application";
                try
                {
                    var assemblyPath = Assembly.GetEntryAssembly().Location;
                    currentDir = Path.GetDirectoryName(assemblyPath);
                    currentExeName = Path.GetFileNameWithoutExtension(assemblyPath);
                }
                catch (Exception) { currentDir = Environment.CurrentDirectory; }

                var unhandledExceptionsDir =
                    Path.Combine(currentDir, useDirectrory);//!!!This should be same as DirectoryTree.ApplicationlogFolderName
                //--es shegvizlia shemovitanot
                if (!Directory.Exists(unhandledExceptionsDir))
                {
                    Directory.CreateDirectory(unhandledExceptionsDir);
                }

                var fileToWrite = Path.Combine(unhandledExceptionsDir,
                    currentExeName +
                    $"_exc_{WindowsIdentity.GetCurrent()?.Name.ToValidFileName()}" +
                    $"{DateTime.Now.ToString("s").ToValidFileName()}" +
                    ".txt");
                File.Create(fileToWrite).Close();
                var sb = new StringBuilder(string.Empty);

                TryExecute(() => sb.AppendLine($"Now: {DateTime.Now}"),
                    () => sb.AppendLine($"ApplicationVersion: {FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion}"),
                    () => sb.AppendLine($"OSVersion: {Environment.OSVersion}"),
                    () => sb.AppendLine($"RuntimeVersion: {Environment.Version}"),
                    () => sb.AppendLine($"InstalledFrameworks: {string.Join(";", GetInstalledDotNetVersions())}"),
                    () => sb.AppendLine($"UserName: {WindowsIdentity.GetCurrent()?.Name}"),
                    () => sb.AppendLine($"UiUserName: {Win32ExtensionMethods.GetUsernameBySessionId(Process.GetCurrentProcess().SessionId, true)}"),
                    () =>
                    {
                        var process = Process.GetCurrentProcess(); ;
                        sb.AppendLine($"ProcessName: \"{process.ProcessName}\" Id: \"{process.Id}");
                    },
                    () => sb.AppendLine($"MachineName: {Environment.MachineName}"),
                    () => sb.AppendLine(isThreadException ?
                        $"Exception:{(e as ThreadExceptionEventArgs)?.Exception}" :
                        $"Exception:{(e as UnhandledExceptionEventArgs)?.ExceptionObject}"),
                    () => sb.AppendLine($"isterminating:{(e as UnhandledExceptionEventArgs)?.IsTerminating}"),
                    () =>
                    {
                        var loaderExceptions =
                            ((e as UnhandledExceptionEventArgs)?.ExceptionObject as System.Reflection.ReflectionTypeLoadException)?.LoaderExceptions;
                        if (loaderExceptions == null || loaderExceptions.Length <= 0) return;
                        foreach (var exc in loaderExceptions)
                        {
                            sb.AppendLine($"LoaderException:{exc}");
                        }
                    },
                    () => sb.AppendLine($"Meta:{meta ?? ""}")
                );

                var logText = sb.ToString();
                using (var sw = new StreamWriter(fileToWrite))
                    sw.WriteLine(logText);
            }
            finally
            {
                Environment.Exit(exitCodeAfter);
            }
        }
        static string[] GetInstalledDotNetVersions()
        {
            const string path = @"SOFTWARE\Microsoft\NET Framework Setup\NDP";
            using (var installedVersions = Registry.LocalMachine.OpenSubKey(path))
            {
                return installedVersions?.GetSubKeyNames();
            }
        }
        static void TryExecute(params Action[] actions2Execute)
        {
            foreach (var action2Execute in actions2Execute)
            {
                try { action2Execute(); }
                catch {/* ignored */}
            }
        }
    }

}
