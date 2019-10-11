using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace WindowsCompatConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var loggingPath = GetLoggingPath();
            Console.WriteLine($"Write logs to: {loggingPath}");
        }

        private static string GetLoggingPath()
        {
            // Verify the code is running on Windows.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Fabrikam\AssetManagement"))
                {
                    if (key?.GetValue("LoggingDirectoryPath") is string configuredPath)
                        return configuredPath;
                }
            }

            // This is either not running on Windows or no logging path was configured,
            // so just use the path for non-roaming user-specific data files.
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(appDataPath, "Fabrikam", "AssetManagement", "Logging");
        }
    }
}