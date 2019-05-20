# Lab Guide - Security

## Exercise 1 - Identity Setup in Project Template
In this lab you will:
* Create a ASP.NET Core web application and add identity through the scaffolded templates.

### Add identity to an existing application via scaffolding

1. In Visual Studio, create a web application using the MVC template with authentication set to **No Authentication**.

2. Run your application to confirm there’s no identity currently configured.

3. Stop running your application.

4. Right click the project and select **Add > New Scaffolded Item…**.

5. Select **Identity** from the list on the left-hand side. Click **Add**.

7. Select the existing "/Views/Shared/_Layout.cshtml" file as the layout page.

8. Next to Data context class, select Add. Name your context "[WebApplicationName].Models.UserContext".

9.	Click **Add**.

10.	Open the "Views/Shared/_Layout.csHtml" file and add the following inside the nav bar, after the closing ```</ul>``` element.

    ```html
    <partial name="_LoginPartial" />
    ```

11.	Open the Package Manager Console (**Tools > NuGet Package Manager > Package Manager Console**) and run the following two the following:

    ```ps
    Add-Migration CreateIdentitySchema
    Update-Database
    ```
12.	In Startup.cs add the following line after ```app.UseStaticFiles();```.

    ```c#
    app.UseAuthentication();
    ```

13.	Run your application again. Once running clikc **Register** to show it now has identity enabled.

### Override the Identity UI

1. Right click the project and select **Add > New Scaffolded Item…**.

2. Select **Identity** from the list on the left-hand side.

3. Check the check box next to "Account\Login".

4. Select the ```UserContext``` class from the **Data Context** class box.

5. Click **Add**.

6. Open the "Areas/Identity/Pages/Account/Login.cshtml" page that was just added to your application.

    > **Note:** This is a Razor page that has its own code behind file. There is no controller in use here.

8. Change the ```<h4>``` element at line 13 to read:

    ```html
    <h4>Welcome to our website, please log in:</h4>
    ```

9. Run the application again and note that you’ve now overridden the styling for the login page.

## Exercise 2 - ASP.NET Core User Secrets

1. In create a new ASP.NET Core web application.

2. Right click the project and select **Manage User Secrets**.

3. In the *secrets.json* file add some valid JSON, for example:

    ```json
    { "ThisIsASecret": "Don’t tell anyone" }
    ```

4. Save the file.

5. Open *Startup.cs*. In the ```ConfigureServices``` method add the line:

    ```c#
    var secret = Configuration["ThisIsASecret"];
    ```

6. Place a breakpoint on the line and run the application.

7. Step-over the line. Note that the value has been loaded from *secrets.json*.

## Exercise 3 - Securing a Web API with Azure AD

1. Take a look at the sample for [Calling a web API in an ASP.NET Core web application using Azure AD](https://azure.microsoft.com/en-gb/resources/samples/active-directory-dotnet-webapp-webapi-openidconnect-aspnetcore/).

2. Click **Browse on GitHub** and explore the project. 

3. If you have an Azure AD tenant that you can use, try running the sample *(guide is included in the link above)*.

    > **Note:** This sample is for **Azure AD v1.0**. If you are looking for an Azure AD v2.0 sample (to sign-in users with Work and School accounts and Microsoft Personal accounts, please look at [active-directory-aspnetcore-webapp-openidconnect-v2](https://github.com/Azure-Samples/active-directory-aspnetcore-webapp-openidconnect-v2).

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
