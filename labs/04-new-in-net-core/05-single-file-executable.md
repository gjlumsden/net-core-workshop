# Lab Guide - Single File Executable and Linker

In this lab, we will look at single file executables and reducing the output size using the Linker (also known as a Trimmed Output).

## Exercise 1 - Create a Single File Executable

### Steps

1. Open your console and navigate to the workshop directory:

    ```bash
    cd C:\Dev\dotnet-workshop\
    ```

1. Create a new Console application

    ```bash
    dotnet new console --name SingleFileExecutable
    cd .\SingleFileExecutable\
    ```

1. Open the project in Visual Studio and open the SingleFileExecutable.csproj file. Modify the project file as follows and save your changes:

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">

      <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>netcoreapp3.1</TargetFramework>
        <RuntimeIdentifier>win-x64</RuntimeIdentifier>
        <PublishSingleFile>true</PublishSingleFile>
      </PropertyGroup>

    </Project>
    ```

1. Return to the command line and publish the application:

    ```bash
    dotnet publish -c release
    ```

1. Navigate to the publish directory and examine the output:
  
    ```bash
    cd bin\release\netcoreapp3.1\publish
    ls
    ```

1. Run the executable:

    ```bash
    .\SingleFileExecutable.exe
    ```

## Exercise 2 - Produce a Trimmed Executable

1. Take a note of the size of the existing `SingleFileExecutable.exe` file

1. Return to visual studio and update the project file as follows:

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">

      <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>netcoreapp3.0</TargetFramework>
        <RuntimeIdentifier>win-x64</RuntimeIdentifier>
        <PublishSingleFile>true</PublishSingleFile>
        <PublishTrimmed>true</PublishTrimmed>
      </PropertyGroup>

    </Project>

    ```

1. In your console, return to the root of the project directory and publish the project again:

    ```bash
    cd ..\..\..\..
    dotnet publish -c release
    ```

1. Return to the publish directory and compare the new executable file size with the size you noted earlier. Notice the new one is significantly smaller.

>If you have time, experiment with combinations of these options to see the effect on the publish directory or file size.
