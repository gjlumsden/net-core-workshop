# Lab Guide - Middleware

## Exercise 1 - Middleware
In this exercise, you will:
* Create a custom middleware
* Use extension methods for built-in Static Files middleware

### Create Custom Middleware

1. Create a new ASP.NET Core application called *Middleware*, choose the **empty template**.

3. Open "Startup.cs" and update the ```Configure``` method as below:

    ```c#
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.Run(async context =>
        {
            await context.Response.WriteAsync("Hello World!");
        });
    }
    ```

3. Run the application and it should show “Hello World!” in the browser.

4.  In the configure method, add a second ```app.Run``` statement and write out the statement:
"Hello World, once again!"

5. Run the application. Note that only “Hello World!” is displayed.

    > **Note:** The request delegate written in the first middleware, uses ```app.Run()``` and will terminate the pipeline, regardless of other calls to ```app.Run()``` that you may include. Therefore, only the first delegate (“Hello World!”) will be run and displayed.
    >
    > You must chain multiple request delegates together. Using ```app.Use()```, with a next parameter that represents the next delegate in the pipeline. Note that just because you are calling ```next``` does not mean you cannot perform actions both before and after the next delegate.

5. Change the first ```app.Run()``` statement to be ```app.Use()``` and add a second parameter in the delegate function called ```next```.

6. At the bottom of the delegate function type the following:

    ```c#
    await next.Invoke();
    ```

7. ```Configure``` should now look like this:

    ```c#
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Hello, World!");
            await next.Invoke();
        });

        app.Run(async context =>
        {
            await context.Response.WriteAsync("Hello, World. Once again!");
        });
    }
    ```

7. Run the application again. It should show both text lines in the browser.

### Use ```map```

1. Add the follow static methods to your ```Startup``` class:

    ```c#
    private static void HandleMapTest1(IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            await context.Response.WriteAsync("Map Test 1");
        });
    }

    private static void HandleMapTest2(IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            await context.Response.WriteAsync("Map Test 2");
        });
    }
    ```

2. Update the ```Configure``` method as follows:

    ```c#
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.Map("/map1", HandleMapTest1);

        app.Map("/map2", HandleMapTest2);

        app.Run(async context =>
        {
            await context.Response.WriteAsync("Hello from non-Map delegate.");
        });
    }
    ```

3. Run the application. Make a request to the following paths and observe the responses. If you prefer, place a couple of breakpoints to help you understand the pipeline:

    * "/"
    * "/map1"
    * "/map2"
    * "/map3"

### Use ```MapWhen```

1. Add the follow static method to your ```Startup``` class:

    ```c#
    private static void HandleBranch(IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            var branchVer = context.Request.Query["branch"];
            await context.Response.WriteAsync($"Branch used = {branchVer}");
        });
    }
    ```

2. Update the ```Configure``` method as follows:

    ```c#
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.MapWhen(context => context.Request.Query.ContainsKey("branch"),
                            HandleBranch);

        app.Map("/map1", HandleMapTest1);

        app.Map("/map2", HandleMapTest2);

        app.Run(async context =>
        {
            await context.Response.WriteAsync("Hello from non-Map delegate.");
        });
    }
    ```

3. Run the application. Make a request to the following paths and observe the responses. If you prefer, place a couple of breakpoints to help you understand the pipeline:

    * "/"
    * "/map1"
    * "/map1?branch=New"

### Middleware Class

Middleware is generally encapsulated in a class and exposed with an extension method.

1. Consider the following middleware, which sets the culture for the current request from a query string:

    ```c#
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var cultureQuery = context.Request.Query["culture"];
                if (!string.IsNullOrWhiteSpace(cultureQuery))
                {
                    var culture = new CultureInfo(cultureQuery);

                    CultureInfo.CurrentCulture = culture;
                    CultureInfo.CurrentUICulture = culture;
                }

                // Call the next delegate/middleware in the pipeline
                await next();
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(
                    $"Hello {CultureInfo.CurrentCulture.DisplayName}");
            });

        }
    }
    ```

1. Create a new *class* called "RequestCultureMiddleware". This class must include:

    * A public constructor with a parameter of type ```RequestDelegate```.
    * A public method named "Invoke" or "InvokeAsync". This method must:
        * Return a ```Task```.
        * Accept a first parameter of type ```HttpContext```.

2. The above middleware would be implemented as such:

    ```c#
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestCultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var cultureQuery = context.Request.Query["culture"];
            if (!string.IsNullOrWhiteSpace(cultureQuery))
            {
                var culture = new CultureInfo(cultureQuery);

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;

            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
    ```

3. To configure this middleware, we'll create a middleware extension method. Create a new static class called "RequestCultureMiddlewareExtensions":

    ```c#
    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestCultureMiddleware>();
        }
    }
    ```

4. In "Startup.cs" update the ```Configure``` method with the following:

    ```c#
    app.UseRequestCulture();
    ```

5. The original middleware example should now look like this:

    ```c#
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRequestCulture();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(
                    $"Hello {CultureInfo.CurrentCulture.DisplayName}");
            });
        }
    }
    ```

### Use Static Files Middleware

1. Add the following line at the start of the ```Configure()``` method:

    ```c#
    app.UseWelcomePage();
    ```

    > **Important:** The order in which you arrange setup middleware in your application’s ```Configure``` method is very important. Be sure you have a good understanding of how your application’s request pipeline will behave in various scenarios.

3. Running the application now should show a Welcome page.

4. In Visual Studio, right-click the ASP.NET project, and select **Add > New Folder**. Give the folder the following name: "wwwroot".

4. Add some static files from to *wwwroot* folder. You can do this by right-clicking *wwwroot* and then selecting **Add > Existing Item**.

    > For example, include some image, *\*.css* and *\*.js* files.

5. Add the following line at the start of the ```Configure()``` method:

    ```c#
    app.UseStaticFiles();
    ```

7. Run the application and navigate to the path of one the images. For example "/images/logo.png".

    > ```app.UseStaticFiles()``` has enabled access of all static files in wwwroot folder.

8. Replace the existing ```UseStaticFiles()``` middleware component with the following:

    ```c#
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider($"{env.WebRootPath}"),
        RequestPath = new PathString("/static")
    });
    ```
9. Resolve any namespace ```using``` statements as required.

10. Run the application. Your static content will now be served using "http://localhost:[port]/static/...".

## Exercise 2 - Working Environments
In this exercise, you will:
* Configure different pipelines for development and production working environments.

### Steps

1. You can continue using the project you created in the previous exercise. 

2. Create another ```Configure()``` method in the ```Startup``` class, and name it ```ConfigureDevelopment```.

    ```c#
    public void ConfigureDevelopment(IApplicationBuilder app)
	{

	}
    ```

3. Add the following middleware to ```ConfigureDevelopment()``` method:

    ```c#
    app.UseStaticFiles();
    app.UseWelcomePage();
    ```

4. Remove the welcome-page and static files middleware from the ```Configure()``` method.

5. Modify ```Configure()```'s final middleware to return “Hello World, from Production!”.

6. Run the application and observe the results.

7. Go to **project properties** (right-click the project and select **properties**) and select the **Debug** tab. Set the *ASPNETCORE_ENVIRONMENT* variable value to "Production".

7. Run the application again, and note the change in application behaviour as you change environment.

---
TODO: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write?view=aspnetcore-3.0