# Lab Guide - Distributing .NET Core

## Exercise 1 - Self-contained applications
When you publish a self-contained deployment (SCD), the .NET Core SDK creates a platform-specific executable. Publishing an SCD includes all required .NET Core files to run your app but it doesn't include the native dependencies of .NET Core.

### Steps

1. Open your console and navigate to the workshop directory.

    ```bash
    cd C:\Dev\dotnet-workshop\
    ```

2. Create a new console application.

    ```bash
    dotnet new console --name SelfContainedApp
    cd .\SelfContainedApp\
    ```

3. Open the project in Visual Studio or VS Code.

4. Edit the project file (.csproj) to include the ```RuntimeIdentifiers``` attribute:

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>netcoreapp3.0</TargetFramework>
        <RuntimeIdentifiers>win10-x64;osx.10.11-x64;ubuntu.16.10-x64</RuntimeIdentifiers>
    </PropertyGroup>

    </Project>
    ```

5. Return to your console, and publish your application for Windows:

    ```bash
    dotnet publish -c release -r win10-x64 --self-contained
    ```

6. Now let's run the executable we've just created. From the same directory, run:

    ```bash
    .\bin\release\netcoreapp3.0\win10-x64\SelfContainedApp.exe
    ```

7. Let's now publish the application for Ubuntu:

    ```bash
    dotnet publish -c release -r ubuntu.16.10-x64 --self-contained
    ```

8. Explore the ```bin``` directory. Navigate to ".\bin\release\netcoreapp3.0". You'll find a directory for both Windows and Ubuntu. Each, with their respective executable as an self-contained deployment.

    > **Note:** Because we opted to publish a self contained application each of these applications can be executed, on their respective platform, without needing the .NET runtime to be installed.