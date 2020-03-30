# Lab Guide - Dependency injection

## Exercise 1 - Dependency Injection with .NET Core

In this lab you will:

* Configure service mapping with different lifetimes using .NET Core built-in features
* Inject service dependencies in a Web API controller

### Steps

1. In Visual Studio, create a new ASP.NET Core Web Application project called "DependencyInjection". Opt to use the API project type.

1. Create a code file with name *OperationInterfaces.cs*

1. Add a base ```IOperation``` interface with an ```OperationId``` property.

    ```CSharp
    public interface IOperation
    {
        Guid OperationId { get; }
    }
    ```

1. Add two derived interfaces. We will later register them with different lifetimes.

    ```CSharp
    public interface IOperationScoped : IOperation
    {
    }

    public interface IOperationSingleton : IOperation
    {
    }
    ```

1. Right-click "Controllers" and select **Add** -> **Controller**. Choose **API Controller - Empty** and click **Add**. Name the controller "ValuesController".

1. Add a field declarations for the two services in the ```ValuesController``` class:

    ```CSharp
    private readonly IOperationScoped _scopedOperation;
    private readonly IOperationSingleton _singletonOperation;
    ```

1. Add a constructor that takes these interfaces as parameters:

    ```CSharp
    public ValuesController(IOperationScoped scopedOperation, IOperationSingleton singletonOperation)
    {
        _scopedOperation = scopedOperation;
        _singletonOperation = singletonOperation;
    }
    ```

1. Create a ```Get()``` action to return the two services so that we can more easily test their ```OperationId``` values.

    ```CSharp
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

1. Create a new class with name ```Operation``` as shown here:

    ```CSharp
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

1. Open *Startup.cs*. In the ```ConfigureServices``` method, add the following service registrations:

    ```c#
    services.AddScoped<IOperationScoped, Operation>();
    services.AddSingleton<IOperationSingleton, Operation>();
    ```

1. Resolve any references and run the app. And make a request to */api/values*

1. Refresh the browser to create multiple requests and observe the behaviour of the services with different lifetimes.
