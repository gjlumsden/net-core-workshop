# Lab Guide - Dependency injection 

## Exercise 1 - ASP.NET Core Built-in Dependency Injection
In this lab you will:
* Configure service mapping with different lifetimes using the ASP.NET Core framework built-in features
* Inject service dependencies in a Web API controller

### Steps

1. In Visual Studio, create a new ASP.NET Core Web API project. 

2. Create a code file with name *OperationInterfaces.cs*

3. Add a base ```IOperation``` interface with an ```OperationId``` property.

    ```c#
    public interface IOperation
    {
        Guid OperationId { get; }
    }
    ```

4. Add two derived interfaces. We will later register them with different lifetimes. 

    ```c#
    public interface IOperationScoped : IOperation
    {
    }

    public interface IOperationSingleton : IOperation
    {
    }
    ```

5. Open the ```ValueController``` class. Add a field declarations for the two services:

    ```c#
    private readonly IOperationScoped _scopedOperation;
    private readonly IOperationSingleton _singletonOperation;
    ```

6. Add a constructor that takes these interfaces as parameters:

    ```c#
    public ValuesController(IOperationScoped scopedOperation, IOperationSingleton singletonOperation)
    {
        _scopedOperation = scopedOperation;
        _singletonOperation = singletonOperation;
    }
    ```

7. Implement the ```Get()``` action to return the two services so that we can more easily test their ```OperationId``` values.

    ```c#
    [HttpGet]
    public ActionResult<Dictionary<string, IOperation>> Get()
    {
        return new Dictionary<string, IOperation>()
        {
            { "Scoped", _scopedOperation },
            { "Singleton", _singletonOperation }
        };
    }
    ```

8. Create a new class with name ```Operation``` as shown here:

    ```c#
    public class Operation : IOperationScoped, IOperationSingleton
    {
        public Operation()
        {
            OperationId = Guid.NewGuid();
        }

        public Guid OperationId { get; private set; }
    }
    ```

    > **Note:** This class implements both interfaces: ```IOperationScoped``` and ```IOperationSingleton```.

9. Open *Startup.cs*. Add the service registrations:

    ```c#
    services.AddScoped<IOperationScoped, Operation>();
    services.AddSingleton<IOperationSingleton, Operation>();
    ```

10. Run the app. And make a request to */api/values*

11. Refresh the browser to create multiple requests and observe the behaviour of the services with different lifetimes.

12. **Bonus task** (if you have time): Play with different registration methods and overloads in ```Setup.ConfigureServices``` and observe the resulting behaviour.

## Exercise 2 - Autofac with ASP.NET Core
In this lab you will:
* Convert the project created in Exercise 1 to use Autofac instead of the built-in DI framework

### Steps

1. Open the Visual Studio project from the previous exercise.

2. Add the Autofac NuGet package and its extension for ASP.NET Core. The packages are:

    * Autofac
    * Autofac.Extensions.DependencyInjection

3. Add Autofac support to the WebHost build pipeline in *Program.cs*.

    1.	Add ```using Autofac.Extensions.DependencyInjection;```
    2.	Add ```AddAutofac``` in ```BuildWebHost``` as following:

        ```c#
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                    .ConfigureServices(services => services.AddAutofac())
                    .UseStartup<Startup>();
        ```
        
4. Add Autofac support to *Startup.cs*.

    1. Add the ```ConfigureContainer``` method as following (you will create the DefaultModule class later in this exercise):

        ```c#
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DefaultModule());
        }
        ```

    2. Resolve any missing ```using``` directives.

    3. Remove the old service mapping registrations from *Startup.cs*.

        ```c#
        services.AddScoped<IOperationScoped, Operation>();
        services.AddSingleton<IOperationSingleton, Operation>();
        ```
    4. Create the Autofac registration module. Create a class named ```DefaultModule``` and define it as following:

        ```c#
        public class DefaultModule : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<Operation>().As<IOperationScoped>()
                    .InstancePerLifetimeScope();
                builder.RegisterType<Operation>().As<IOperationSingleton>().SingleInstance();
            }
        }
        ```

    5. Resolve any further missing ```using``` directives.
    
    6. Run the app. It should continue to work - the same as previously.


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
