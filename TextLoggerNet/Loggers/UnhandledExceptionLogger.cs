using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using TextLoggerNet.Helpers;

namespace TextLoggerNet.Loggers
{
    public class UnhandledExceptionLogger
    {
        public static void UnhandledExceptionHandler(object e, bool isThreadException, string meta = null, string useDirectrory = "applog")
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
                Environment.Exit(0);
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
