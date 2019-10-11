# Lab Guide - Dependency injection 

## Exercise 1 - ASP.NET Core Built-in Dependency Injection
In this lab you will:
* Configure service mapping with different lifetimes using the ASP.NET Core framework built-in features
* Inject service dependencies in a Web API controller

### Steps

1. In Visual Studio, create a new ASP.NET Core Web Application project called "DependencyInjection". Opt to use the API project type.

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

5. Right-click "Controllers" and select **Add** -> **Controller**. Choose **API Controller - Empty** and click **Add**. Name the controller "ValuesController".

6. Add a field declarations for the two services in the ```ValuesController``` class:

    ```c#
    private readonly IOperationScoped _scopedOperation;
    private readonly IOperationSingleton _singletonOperation;
    ```

7. Add a constructor that takes these interfaces as parameters:

    ```c#
    public ValuesController(IOperationScoped scopedOperation, IOperationSingleton singletonOperation)
    {
        _scopedOperation = scopedOperation;
        _singletonOperation = singletonOperation;
    }
    ```

8. Create a ```Get()``` action to return the two services so that we can more easily test their ```OperationId``` values.

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

9. Create a new class with name ```Operation``` as shown here:

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

10. Open *Startup.cs*. In the ```ConfigureServices``` method, add the following service registrations:

    ```c#
    services.AddScoped<IOperationScoped, Operation>();
    services.AddSingleton<IOperationSingleton, Operation>();
    ```

11. Resolve any references and run the app. And make a request to */api/values*

12. Refresh the browser to create multiple requests and observe the behaviour of the services with different lifetimes.