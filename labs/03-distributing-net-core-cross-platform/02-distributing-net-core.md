# Lab Guide - Distributing .NET Core

## Exercise 1 - Self-contained applications

When you publish a self-contained deployment (SCD), the .NET Core SDK creates a platform-specific executable. Publishing an SCD includes all required .NET Core files to run your app but it doesn't include the native dependencies of .NET Core.

### Steps

1. Open your console and navigate to the workshop directory.

    ```bash
    cd C:\Dev\dotnet-workshop\
    ```

1. Create a new console application.

    ```bash
    dotnet new console --name SelfContainedApp
    cd .\SelfContainedApp\
    ```

1. Open the project in Visual Studio or VS Code.

1. Edit the project file (.csproj) to include the ```RuntimeIdentifiers``` attribute:

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>netcoreapp3.1</TargetFramework>
        <RuntimeIdentifiers>win10-x64;osx.10.11-x64;linux-x64</RuntimeIdentifiers>
    </PropertyGroup>

    </Project>
    ```

1. Return to your console, and publish your application for Windows:

    ```bash
    dotnet publish -c release -r win10-x64 --self-contained
    ```

1. Now let's run the executable we've just created. From the same directory, run:

    ```bash
    .\bin\release\netcoreapp3.1\win10-x64\SelfContainedApp.exe
    ```

1. Let's now publish the application for Linux:

    ```bash
    dotnet publish -c release -r linux-x64 --self-contained
    ```

    >**Note:** The Runtime Identifier `linux-x64` covers most desktop distributions like CentOS, Debian, Fedora, Ubuntu and derivatives

1. Explore the ```bin``` directory. Navigate to ".\bin\release\netcoreapp3.1". You'll find a directory for both Windows and Linux. Each, with their respective executable as an self-contained deployment.

    > **Note:** Because we opted to publish a self contained application each of these applications can be executed, on their respective platform, without needing the .NET runtime to be installed.
