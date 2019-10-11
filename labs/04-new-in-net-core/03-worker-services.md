# Lab Guide - Worker Services

## Exercise 3 - Create a Worker Service

### Steps

1. Open your console and navigate to the workshop directory:

    ```bash
    cd C:\Dev\dotnet-workshop\
    ```

2. Create a new project using the ```worker``` template:

    ```bash
    dotnet new worker --name MyWorkerService
    cd .\MyWorkerService\
    ```

3. Open the project in Visual Studio or Visual Studio Code. Open "Worker.cs" and explore what the application does.

3. Return to your console. Let's run the application:

    ```bash
    dotnet run
    ```

    > **Note:** The application logs date and time every 1 second.

4. To exit the application, press **Ctrl+C**.

5. In order to run as a Windows Service we need our worker to listen for start and stop signals from ```ServiceBase``` - the .NET type that exposes the Windows Service systems to .NET applications. To do this we want to add the ```Microsoft.Extensions.Hosting.WindowsServices``` NuGet package

    ```bash
    dotnet add package Microsoft.Extensions.Hosting.WindowsServices
    ```

6. Add the ```UseWindowsService``` call to the ```HostBuilder``` in our "Program.cs":

    ```c#
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices(services =>
                {
                    services.AddHostedService<Worker>();
                });
    }
    ```

    > This method does a couple of things. First, it checks whether or not the application is actually running as a Windows Service, if it isn’t then it noops which makes this method safe to be called when running locally or when running as a Windows Service. You don’t need to add guard clauses to it and can just run the app normally when not installed as a Windows Service.
    >
    > Secondly, it configures your host to use a ```ServiceBaseLifetime```. ```ServiceBaseLifetime``` works with ```ServiceBase``` to help control the lifetime of your app when run as a Windows Service. This overrides the default ```ConsoleLifetime``` that handles signals like CTL+C.

7. Return to your console. Let's publish the application:

    ```bash
    dotnet publish -o .\workerpub
    ```

8. Then we can use the ```sc``` utility in an **admin** command prompt to create the Windows Service:

    ```bash
    sc create workertest binPath=C:\Dev\dotnet-workshop\MyWorkerService\workerpub\MyWorkerService.exe
    ```

9. Open **Services**. You will see "workertest" in the list of Services. Select the service, and click **Start**. You have now successfully created a .NET Core Windows Service.