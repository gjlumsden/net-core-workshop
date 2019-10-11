# Lab Guide - Get started with .NET Core

## Exercise 1 - Install the .NET Core SDK

### Steps

1. Go to https://dotnet.microsoft.com/download

2. Download the latest .NET Core SDK and run the installer.

3. Verify the installation was successful by running the following command in your console:

    ```bash
    dotnet --version
    ```

## Exercise 2 - Create your first application

### Steps

1. Open your preferred console. Create a directory for the workshop:

    ```bash
    cd C:\Dev\
    mkdir dotnet-workshop
    cd .\dotnet-workshop\
    ```

2. Type the following ```dotnet``` commands to create and run a C# application.

    ```bash
    dotnet new console --name MyFirstConsoleApp
    cd .\MyFirstConsoleApp\
    dotnet run
    ```

3. Open the project in Visual Studio or VS Code. Take a look at the project file - open the *.csproj file:

    * The ```OutputType``` tag specifies that we're building an executable, in other words a console application.
    * The ```TargetFramework``` tag specifies what .NET implementation we're targeting. 

4. Let's change the program a bit. Replace the contents of your *Program.cs* file with the following code:

    ```c#
    using System;

    namespace Hello
    {
        class Program
        {
            static void Main(string[] args)
            {
                if (args.Length > 0)
                {
                    Console.WriteLine($"Hello {args[0]}!");
                }
                else
                {
                    Console.WriteLine("Hello!");
                }

                Console.WriteLine("Fibonacci Numbers 1-15:");

                for (int i = 0; i < 15; i++)
                {
                    Console.WriteLine($"{i + 1}: {FibonacciNumber(i)}");
                }
            }

            static int FibonacciNumber(int n)
            {
                int a = 0;
                int b = 1;
                int tmp;

                for (int i = 0; i < n; i++)
                {
                    tmp = a;
                    a = b;
                    b += tmp;
                }

                return a;
            }

        }
    }
    ```

5. Return to your console, and compile the changes:

    ```bash
    dotnet build
    ```

    > **Note:** Starting with .NET Core 2.0 SDK, ```dotnet restore``` runs implicitly when you run ```dotnet build```.

6. Run the program passing a parameter to the app:

    ```bash
    dotnet run -- John
    ```

    > **Note:** Any arguements after the "--" are passed to the application itself. 

7. Add a new file inside the project directory named *FibonacciGenerator.cs* with the following code:

    ```c#
    using System;
    using System.Collections.Generic;

    namespace Hello
    {
        public class FibonacciGenerator
        {
            private Dictionary<int, int> _cache = new Dictionary<int, int>();
            
            private int Fib(int n) => n < 2 ? n : FibValue(n - 1) + FibValue(n - 2);
            
            private int FibValue(int n)
            {
                if (!_cache.ContainsKey(n))
                {
                    _cache.Add(n, Fib(n));
                }
                
                return _cache[n];
            }
            
            public IEnumerable<int> Generate(int n)
            {
                for (int i = 0; i < n; i++)
                {
                    yield return FibValue(i);
                }
            }
        }
    }
    ```

8. In *Program.cs* update the ```Main``` method to instantiate the new class and call its method as in the following example:

    ```c#
    using System;

    namespace Hello
    {
        class Program
        {
            static void Main(string[] args)
            {
                var generator = new FibonacciGenerator();
                foreach (var digit in generator.Generate(15))
                {
                    Console.WriteLine(digit);
                }
            }
        }
    }
    ```

9. Return to your console, compile and run your application:

    ```bash
    dotnet build
    dotnet run
    ```

## Exercise 3 - ```dotnet new```
You have already create a project using ```dotnet new```. A console application to be specific. But, what else can you create?

### Steps

1. Return to your console and navigate to the workshop directory:

    ```bash
    cd C:\Dev\dotnet-workshop\
    ```

2. Run ```dotnet new```. You will be presented with a list of project templates that you can use. This is the command line experience for **File -> New Project** in Visual Studio. For each template you will see:

    * It's short name, e.g. ```mvc```
    * The available languages, e.g. [C#] and F#

4. Each template has further options. For example, run ```dotnet new mvc --help``` and you will see this template has options like ```--auth```, ```--no-https``` and ```--no-restore```.

5. Let's try creating an ASP.NET MVC project. Run the following command:

    ```bash
    dotnet new mvc --name MyFirstWebApp --no-https
    ```

6. Open your project in Visual Studio or VS Code. You will see the project has been scaffolded from the MVC template.

7. Return to your console. Just like the console application, we can run the web application using ```dotnet run```:

    ```bash
    dotnet run --project MyFirstWebApp
    ```

    > **Note:** This command uses the ```--project``` option to provide the path to the project file to run. By default, ```dotnet``` would use the current directory if there is only one project.

8. Open your web browser and navigate to "http://localhost:5000/". 

9. To stop running press **Ctrl+C**.