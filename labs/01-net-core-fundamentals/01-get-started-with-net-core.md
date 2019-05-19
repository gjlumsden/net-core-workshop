# Lab Guide - Get started with .NET Core

## Exercise 1 - Install the .NET Core SDK

### Steps

1. Go to https://dotnet.microsoft.com/download

2. Download the latest .NET Core SDK (currently version 2.2) and run the installer.

    > **Please note:** There is a seperate download link to use if you're running Visual Studio 2017.

3. Verify the installation was successful by running the following command in your console:

    ```bash
    dotnet --version
    ```

## Exercise 2 - Create your first application

### Steps

1. Open your preferred console. Create a director for the workshop:

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

6. Run the program passing a parameter to the app:

    ```bash
    dotnet run -- John
    ```

    > **Note:** Arguements after the "--" are passed to the application itself. 

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

4. Each template has further options. For example, run ```dotnet new mvc --help``` to see options like ```--auth```, ```--no-https``` and ```--no-restore```.

5. Let's try creating an ASP.NET MVC project. Run the following command:

    ```bash
    dotnet new mvc --name MyFirstWebApp --no-https
    ```

6. Open your project in Visual Studio or VS Code. You will see the project has been scaffolded from the MVC template.

7. Just like our console application, we can run our web application using ```dotnet run```:

    ```bash
    dotnet run --project MyFirstWebApp
    ```

8. Open your web browser and navigate to "http://localhost:5000/". 

9. To stop running press **Ctrl+C**.

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
