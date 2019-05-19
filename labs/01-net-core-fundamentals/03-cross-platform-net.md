# Lab Guide - Cross-platform .NET

## Exercise 1 - Cross-platform targeting

### Steps

1. Return to your console, navigate to the workshop directory and create a new class library project.

    ```bash
    cd C:\Dev\dotnet-workshop\
    dotnet new classlib --name MultiTargetLib
    cd .\MultiTargetLib\
    ```

2. Open the project in Visual Studio or VS Code. Take a look at the project file (*\*.csproj*).

    > **Note:** The ```TargetFramework``` is "netstandard2.0".

3. Update the project file, to look like this:

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

4. Let's take a look at three key changes here:

    * The ```TargetFramework``` has been replaced by ```TargetFrameworks```, and three TFMs are expressed inside.
    * There is an ```<ItemGroup>``` node for the ```net40``` target pulling in one .NET Framework reference.
    * There is an ```<ItemGroup>``` node for the ```net45``` target pulling in two .NET Framework references.

5. Using conditional compilation we can write code specifically for each target. For example:

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

5. Return to your console and build the project:

    ```bash
    dotnet build
    ```

6. Navigate to the ```bin``` directory. You'll find three directories, one for each framework: "netstandard2.0", "net40" and "net45". Each of these contain their own respective ```.dll``` files.

### Steps

## Exercise 2 - Using Windows Compatibility Pack

### Steps

1. Open your console and navigate to the workshop directory.

    ```bash
    cd C:\Dev\dotnet-workshop\
    ```

2. Create a new console application.

    ```bash
    dotnet new console --name WindowsCompatConsoleApp
    cd .\WindowsCompatConsoleApp\
    ```

3. Add a NuGet package reference to the project for "Microsoft.Windows.Compatibility".

    ```bash
     dotnet add package Microsoft.Windows.Compatibility
    ```

4. Open the project in Visual Studio or VS Code.

5. Update *Program.cs* as follows:

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

6. Run the application. On Windows the console app will use the Registry value (if it exists). On any other platform, it will use the *LocalApplicationData* directory.
    
___
#### Conditions and Terms of Use

This training package is proprietary and confidential and is intended only for training purpose within Aggreko. Content and software is provided to you under a Non-Disclosure Agreement and cannot be distributed. Copying or disclosing outside Aggreko all or any portion of the content and/or software included in such packages is strictly prohibited.
The contents of this package are for informational and training purposes only and are provided "as is" without warranty of any kind, whether express or implied, including but not limited to the implied warranties of merchantability, fitness for a particular purpose, and non-infringement.
Training package content, including URLs and other Internet Web site references, is subject to change without notice. Because Microsoft must respond to changing market conditions, the content should not be interpreted to be a commitment on the part of Microsoft, and Microsoft cannot guarantee the accuracy of any information presented after the date of publication. Unless otherwise noted, the companies, organizations, products, domain names, e-mail addresses, logos, people, places, and events depicted herein are fictitious, and no association with any real company, organization, product, domain name, e-mail address, logo, person, place, or event is intended or should be inferred.

#### Copyright and Trademarks
© 2018 Microsoft Corporation. All rights reserved.
Microsoft may have patents, patent applications, trademarks, copyrights, or other intellectual property rights covering subject matter in this document. Except as expressly provided in written license agreement from Microsoft, the furnishing of this document does not give you any license to these patents, trademarks, copyrights, or other intellectual property.
Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this document may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, outside Aggreko without the express written permission of Microsoft Corporation. 

For more information, see Use of Microsoft Copyrighted Content at
http://www.microsoft.com/en-us/legal/intellectualproperty/permissions/default.aspx
