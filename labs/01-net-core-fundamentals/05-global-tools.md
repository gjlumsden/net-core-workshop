# Lab Guide - Global Tools

## Exercise 1 - Install a Global Tool

### Steps

1. Open your console. Install ```dotnetsay``` in the default location.

    ```bash
    dotnet tool install -g dotnetsay
    ```

    > **Note:** THe default location in Windows is "~\\.dotnet\tools"

2. Check that the tool has installed by running ```dotnet tool list -g```.

3. You can invoke the tool using the following command: ```dotnetsay```

4. If required, you can updated ```dotnetsay```:

    ```bash
    dotnet tool update -g dotnetsay
    ```

4. Finally, you can uninstall ```dotnetsay```:

    ```bash
    dotnet tool uninstall  -g dotnetsay
    ```

## Exercise 2 - Create your own Global Tool

### Steps

1. Return to your console, navigate to the workshop directory and create a new Console App.

    ```bash
    cd C:\Dev\dotnet-workshop\
    dotnet new console --name SayHello
    cd .\SayHello\
    ```

2. Open the project in Visual Studio or VS Code. Open the project file (*\*.csproj*).

3. Add the following into the main ```PropertyGroup``` section:

    ```xml
    <PackAsTool>true</PackAsTool>
    <ToolCommandName>dotnet-say-hello</ToolCommandName>
    ```

4. Return to your console. Publish and package the project.

    ```bash
    dotnet publish
    dotnet pack
    ```

5. Install the Global Tool:

    ```bash
    dotnet tool install -g Sayhello --add-source .\bin\debug
    ```

    > **Note:** Typically you would install Global Tools from NuGet. In this instance, you're using ```--add-source``` to install from a local source.

6. Check that the application has installed by running ```dotnet tool list -g```.

7. Now test your application by running ```dotnet say-hello```. It should write "Hello World!" to your console.

8. Uninstall your application by running ```dotnet tool uninstall -g SayHello```

___
#### Conditions and Terms of Use

This training package is proprietary and confidential and is intended only for training purpose within Aggeko. Content and software is provided to you under a Non-Disclosure Agreement and cannot be distributed. Copying or disclosing outside Aggeko all or any portion of the content and/or software included in such packages is strictly prohibited.
The contents of this package are for informational and training purposes only and are provided "as is" without warranty of any kind, whether express or implied, including but not limited to the implied warranties of merchantability, fitness for a particular purpose, and non-infringement.
Training package content, including URLs and other Internet Web site references, is subject to change without notice. Because Microsoft must respond to changing market conditions, the content should not be interpreted to be a commitment on the part of Microsoft, and Microsoft cannot guarantee the accuracy of any information presented after the date of publication. Unless otherwise noted, the companies, organizations, products, domain names, e-mail addresses, logos, people, places, and events depicted herein are fictitious, and no association with any real company, organization, product, domain name, e-mail address, logo, person, place, or event is intended or should be inferred.

#### Copyright and Trademarks
© 2018 Microsoft Corporation. All rights reserved.
Microsoft may have patents, patent applications, trademarks, copyrights, or other intellectual property rights covering subject matter in this document. Except as expressly provided in written license agreement from Microsoft, the furnishing of this document does not give you any license to these patents, trademarks, copyrights, or other intellectual property.
Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this document may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, outside Aggeko without the express written permission of Microsoft Corporation. 

For more information, see Use of Microsoft Copyrighted Content at
http://www.microsoft.com/en-us/legal/intellectualproperty/permissions/default.aspx
