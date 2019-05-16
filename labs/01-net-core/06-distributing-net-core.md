# Lab Guide - Distributing .NET Core

## Exercise 1 - Self-contained applications
When you publish a self-contained deployment (SCD), the .NET Core SDK creates a platform-specific executable. Publishing an SCD includes all required .NET Core files to run your app but it doesn't include the native dependencies of .NET Core.

### Steps

1. Open your console and navigate to the workshop directory.

    ```bash
    cd C:\Dev\dotnet-workshop\
    ```

2. Create a new console application.

    ```bash
    dotnet new console --name SelfContainedApp
    cd .\SelfContainedApp\
    ```

3. Open the project in Visual Studio or VS Code.

4. Edit the project file (.csproj) to include the ```RuntimeIdentifiers``` attribute:

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>netcoreapp2.2</TargetFramework>
        <RuntimeIdentifiers>win10-x64;osx.10.11-x64;ubuntu.16.10-x64</RuntimeIdentifiers>
    </PropertyGroup>

    </Project>
    ```

5. Return to your console, and publish your application for Windows:

    ```bash
    dotnet publish -c release -r win10-x64 --self-contained
    ```

6. And now, for Ubuntu:

    ```bash
    dotnet publish -c release -r ubuntu.16.10-x64 --self-contained
    ```

7. Explore the ```bin``` directory. Navigate to ".\bin\release\netcoreapp2.2". You'll find a directory for both Windows and Ubuntu. Each, with their respective executable as an self-contained deployment.

8. Each of these applications can be executed, on their respective platform, without needing the .NET runtime to be installed.

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
