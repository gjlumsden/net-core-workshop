# Lab Guide - Middleware

## Exercise 1 - Middleware
In this exercise, you will:
* Create a custom middleware
* Use extension methods for built-in Static Files middleware

### Create Custom Middleware

1. Create a new ASP.NET Core application called *Middleware*, choose the **empty template**.

2. Run the application and it should show “Hello World!” text in the browser.

3. In the configure method, add a second ```app.Run``` statement and write out the statement:
"Hello Again World!"

4. Run the application. Note that only “Hello World!” is displayed.

    > **Note:** The request delegate written in the first middleware, uses ```app.Run()``` and will terminate the pipeline, regardless of other calls to ```app.Run()``` that you may include. Therefore, only the first delegate (“Hello, World!”) will be run and displayed.
    >
    > You must chain multiple request delegates together. Using ```app.Use()```, with a next parameter that represents the next delegate in the pipeline. Note that just because you are calling ```next``` does not mean you cannot perform actions both before and after the next delegate.

5. Change the first ```app.Run()``` statement to be ```app.Use()``` and add a second parameter in the delegate function called ```next```.

6. At the bottom of the delegate function type the following:

    ```c#
    await next.Invoke();
    ```

7. Run the application now. It should show both text lines in the browser.

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
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.Map("/map1", HandleMapTest1);

        app.Map("/map2", HandleMapTest2);

        app.Run(async context =>
        {
            await context.Response.WriteAsync("Hello from non-Map delegate.");
        });
    }
    ```

3. Run the application. Make a request to the following URLs and observe the responses:

    * "/"
    * "/map1"
    * "/map2"
    * "/map3"


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

5. Remove the Welcome Page middleware statement.

6. Add the following line at the start of the ```Configure()``` method:

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

2. Create another ```Configure()``` method in the ```Startup``` class, and name it ```ConfigureDevelopment```. Place this at the end of the file.

    ```c#
    public void ConfigureDevelopment(IApplicationBuilder app)
	{

	}
    ```

3. Add the following statement to ```ConfigureDevelopment()``` method:

    ```c#
    app.UseStaticFiles();
    ```

4. We'll want a Welcome page in our Dev environment. Add the following statement to ```ConfigureDevelopment()``` method:

    ```c#
    app.UseWelcomePage();
    ```

5. Remove the welcome-page and static files middleware from the regular Configure() method.

6. Modify first middleware’s text to “Hello World, from Production!”.

7. Go to **project properties** (right-click the project and select **properties**) and then go to the **Debug** tab. Set the *ASPNETCORE_ENVIRONMENT* variable value to "Development".

8. Run the application, and note the change in application behaviour as you change environment.



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
