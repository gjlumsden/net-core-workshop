# Lab Guide - Global Tools

## Exercise 1 - Install a Global Tool

### Steps

1. Open your console. Install ```dotnetsay``` in the default location.

    ```bash
    dotnet tool install -g dotnetsay
    ```

    > **Note:** THe default location in Windows is "~\\.dotnet\tools"

2. Check that the tool has installed by running ```dotnet tool list -g```.

3. You can invoke the tool using the following command: ```dotnetsay```

4. If required, you can updated ```dotnetsay```:

    ```bash
    dotnet tool update -g dotnetsay
    ```

4. Finally, you can uninstall ```dotnetsay```:

    ```bash
    dotnet tool uninstall  -g dotnetsay
    ```

    > You can find .NET Core Global Tools on NuGet. However, NuGet doesn't yet allow you to search specifically for .NET Core Global Tools.
    >
    > You can find a list of tool in the [natemcmaster/dotnet-tools](https://github.com/natemcmaster/dotnet-tools) GitHub repository.

## Exercise 2 - Create your own Global Tool

### Steps

1. Return to your console, navigate to the workshop directory and create a new Console App.

    ```bash
    cd C:\Dev\dotnet-workshop\
    dotnet new console --name MyCustomTool.SayHello
    cd .\MyCustomTool.SayHello\
    ```

2. Open the project in Visual Studio or VS Code. Open the project file (*\*.csproj*).

3. Add the following into the main ```PropertyGroup``` section:

    ```xml
    <PackAsTool>true</PackAsTool>
    <ToolCommandName>dotnet-say-hello</ToolCommandName>
    ```

4. Return to your console. Publish and package the project.

    ```bash
    dotnet publish
    dotnet pack
    ```

5. Install the Global Tool:

    ```bash
    dotnet tool install -g MyCustomTool.SayHello --add-source .\bin\debug
    ```

    > **Note:** Typically you would install Global Tools from NuGet. In this instance, you're using ```--add-source``` to install from a local source.

6. Check that the application has installed by running ```dotnet tool list -g```.

7. Now test your application by running ```dotnet say-hello```. It should write "Hello World!" to your console.

8. Uninstall your application by running ```dotnet tool uninstall -g MyCustomTool.SayHello```