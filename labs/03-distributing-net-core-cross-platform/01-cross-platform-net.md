# Lab Guide - Cross-platform .NET

## Exercise 1 - Cross-platform targeting

### Steps

1. Return to your console, navigate to the workshop directory and create a new class library project.

    ```bash
    cd C:\Dev\dotnet-workshop\
    dotnet new classlib --name MultiTargetLib
    cd .\MultiTargetLib\
    ```

1. Open the project in Visual Studio or VS Code. Take a look at the project file (*\*.csproj*).

    > **Note:** The ```TargetFramework``` is "netstandard2.0".

1. We want to update the project file to target "netstandard2.0", ".NET Framework 4.0" and ".NET Framework 4.5". Update the *\*csproj* to look like this:

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">
        <PropertyGroup>
            <TargetFrameworks>netstandard2.0;net40;net45</TargetFrameworks>
        </PropertyGroup>

        <!-- Need to conditionally bring in references for the .NET Framework 4.0 target -->
        <ItemGroup Condition="'$(TargetFramework)' == 'net40'">
            <Reference Include="System.Net" />
        </ItemGroup>

        <!-- Need to conditionally bring in references for the .NET Framework 4.5 target -->
        <ItemGroup Condition="'$(TargetFramework)' == 'net45'">
            <Reference Include="System.Net.Http" />
            <Reference Include="System.Threading.Tasks" />
        </ItemGroup>
    </Project>
    ```

1. Let's take a look at three key changes here:

    * The ```TargetFramework``` has been replaced by ```TargetFrameworks```, and three TFMs are expressed inside.
    * There is an ```<ItemGroup>``` node for the ```net40``` target pulling in one .NET Framework reference.
    * There is an ```<ItemGroup>``` node for the ```net45``` target pulling in two .NET Framework references.

1. Using conditional compilation we can write code specifically for each target. As an example, update "Class1.cs" to look like this:

    ```c#
    using System;
    using System.Text.RegularExpressions;
    #if NET40
    // This only compiles for the .NET Framework 4 targets
    using System.Net;
    #else
    // This compiles for all other targets
    using System.Net.Http;
    using System.Threading.Tasks;
    #endif

    namespace MultiTargetLib
    {
        public class Library
        {
    #if NET40
            private readonly WebClient _client = new WebClient();
            private readonly object _locker = new object();
    #else
            private readonly HttpClient _client = new HttpClient();
    #endif

    #if NET40
            // .NET Framework 4.0 does not have async/await
            public string GetDotNetCount()
            {
                string url = "https://www.dotnetfoundation.org/";

                var uri = new Uri(url);

                string result = "";

                // Lock here to provide thread-safety.
                lock(_locker)
                {
                    result = _client.DownloadString(uri);
                }

                int dotNetCount = Regex.Matches(result, ".NET").Count;

                return $"Dotnet Foundation mentions .NET {dotNetCount} times!";
            }
    #else
            // .NET 4.5+ can use async/await!
            public async Task<string> GetDotNetCountAsync()
            {
                string url = "https://www.dotnetfoundation.org/";

                // HttpClient is thread-safe, so no need to explicitly lock here
                var result = await _client.GetStringAsync(url);

                int dotNetCount = Regex.Matches(result, ".NET").Count;

                return $"dotnetfoundation.org mentions .NET {dotNetCount} times in its HTML!";
            }
    #endif
        }
    }
    ```

1. Return to your console and build the project:

    ```bash
    dotnet build
    ```

1. Navigate to the "bin" directory. You'll find three directories, one for each framework: "netstandard2.0", "net40" and "net45". Each of these contain their own respective ```.dll``` files.

## Exercise 2 - Using Windows Compatibility Pack

### Steps

1. Open your console and navigate to the workshop directory.

    ```bash
    cd C:\Dev\dotnet-workshop\
    ```

1. Create a new console application.

    ```bash
    dotnet new console --name WindowsCompatConsoleApp
    cd .\WindowsCompatConsoleApp\
    ```

1. Add a NuGet package reference to the project for "Microsoft.Windows.Compatibility".

    ```bash
     dotnet add package Microsoft.Windows.Compatibility
    ```

1. Open the project in Visual Studio or VS Code.

1. Update *Program.cs* as follows:

    ```c#
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
    ```

1. Run the application. On Windows the application will use the Registry value (if it exists). On any other platform, it will use the *LocalApplicationData* directory.

> If you have Windows Subsystem for Linux installed, try running the application in Linux to see the different result.
