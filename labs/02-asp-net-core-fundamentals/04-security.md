# Lab Guide - Security

## Exercise 1 - User Secrets

1. In Visual Studio create a new ASP.NET Core web application.

2. Right click the project and select **Manage User Secrets**.

3. In the *secrets.json* file add some valid JSON, for example:

    ```json
    { "ThisIsASecret": "Don't tell anyone" }
    ```

4. Save the file.

5. Open *Startup.cs*. Create and initiated a fields called ```configuration``` as follows:

    ```c#
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    ```

6. In the ```Configure``` method, update the default ```UseEndpoints``` middleware as follows:

    ```c#
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapGet("/", async context =>
        {
            var secret = configuration["ThisIsASecret"];
            await context.Response.WriteAsync($"The secret is: {secret}");
        });
    });
    ```

6. Run the application.

    > **Note:** The secret value has been loaded from *secrets.json*.