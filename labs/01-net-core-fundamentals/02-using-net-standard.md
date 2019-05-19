# Lab Guide - Using .NET Standard

## Prerequisites
* **Operating System:** Windows
* **Software:** Visual Studio 2017 or 2019.

## Exercise 1 - Reference .NET Standard from .NET Core

### Steps

1. Return to your console, create a Visual Studio Solution with a couple of new projects.

    ```bash
    cd C:\Dev\dotnet-workshop\
    mkdir DotNetStandard
    cd .\DotNetStandard\
    dotnet new sln

    dotnet new classlib --name DiscountCalculator.Core
    dotnet sln add .\DiscountCalculator.Core\
    
    dotnet new api --name DiscountCalculator.API
    dotnet sln add .\DiscountCalculator.API\
    ```

2. Add a project reference too:

    ```bash
    dotnet add DiscountCalculator.API reference .\DiscountCalculator.Core\
    ```

3. Open the solution (.sln) file in Visual Studio. For each project, open it's project file. Note that they are targeting "netstandard2.0" and "netcoreapp2.2" respectively.

4. Let's add some code. In *DiscountCalculator.Core* replace *Class1.cs** with "DiscountEngine.cs"

    ```c#
    using System;

    namespace DiscountCalculator.Core
    {
        public class DiscountEngine
        {
            public double GetDiscountAmount(DateTime dateJoined)
            {
                var diff = DateTime.Today - dateJoined;

                switch ((int)Math.Floor(diff.TotalDays / 365))
                {
                    case 0:
                        return 0;
                    case 1:
                        return 0.1;
                    case 2:
                    case 3:
                    case 4:
                        return 0.15;
                    case 5:
                        return 0.2;
                    default:
                        return 0.3;
                }
            }
        }
    }
    ```

5. In *DiscountCalculator.API* replace *ValuesController.cs** with "DiscountsController.cs"

    ```c#
    using System;
    using DiscountCalculator.Core;
    using Microsoft.AspNetCore.Mvc;

    namespace DiscountCalculator.API.Controllers
    {
        [Route("api/discounts")]
        [ApiController]
        public class DiscountsController : ControllerBase
        {
            [HttpGet]
            public ActionResult<double> Get(DateTime dateJoined)
            {
                var engine = new DiscountEngine();
                var discount = engine.GetDiscountAmount(dateJoined);
                return discount;
            }
        }
    }
    ```

    > **Note:** In the next module, we'll explore how to improve this with *depenedency injection*.

6. Run your web application from Visual Studio. Make a web request to "/api/discounts?dateJoined=2017-05-01" and the service will return the permitted discount.

## Exercise 2 - Reference .NET Standard from .NET Framework

### Steps

1. In Visual Studio, right click the solution and select **Add -> New Project**. Look for the project type "Console Application (.NET Framework)". Call it "DiscountCalculator.CLI"

    > **Note:** For the purpose of this exercise ensure you select .NET Framework and C#.

2. Right click the new project and select **Add -> Reference**. Select the project "DiscountCalculator.Core".

3. Update *Program.cs* with:

    ```c#
    using DiscountCalculator.Core;
    using System;

    namespace DiscountCalculator.CLI
    {
        class Program
        {
            static void Main(string[] args)
            {
                var discountEngine = new DiscountEngine();

                while (true)
                {
                    Console.WriteLine("Enter date joined:");
                    var input = Console.ReadLine();

                    DateTime dateJoined;
                    if (!DateTime.TryParse(input, out dateJoined))
                    {
                        Console.WriteLine($"Invalid input: '{input}'.");
                    }

                    var discount = discountEngine.GetDiscountAmount(dateJoined);
                    Console.WriteLine($"Available discount: {discount * 100}%");
                }


            }
        }
    }
    ```

4. Run your console application from Visual Studio.

5. You now have a .NET Standard library, that is being referenced by a ASP.NET Core Web API project and a .NET Framework Console app. "DiscountCalculator.Core" can be used anywhere that implements "netstandard2.0":

    * .NET Core 2.0
    * .NET Framework 4.6.1
    * Mono 5.4
    * Xamarin.iOS 10.14
    * Xamarin.Mac 3.8
    * Xamarin.Android 8.0
    * Universal Windows Platform 10.0.16299
    * Unity 2018.1
    * And more!

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
