# Lab Guide - Desktop Applications

## Exercise 1 - Create a .NET Core Windows Forms application

### Steps

1. Open your console and navigate to the workshop directory:

    ```bash
    cd C:\Dev\dotnet-workshop\
    ```

2. Create a Visual Studio solution, with a Windows Forms desktop application:

    ```bash
    mkdir MyDesktopApp
    cd .\MyDesktopApp\
    dotnet new sln

    dotnet new winforms --name MyDesktopApp.App 
    dotnet sln add .\MyDesktopApp.App\
    ```

3. Open "MyDesktopApp.sln" in Visual Studio and inspect the project file.

    > **Note:** Notice the output type is ```WinExe``` and there is a ```UseWindowsForms``` setting set to ```true```.

4. From Visual Studio, or using ```dotnet run```, run the application. A Windows Desktop app should boot up!

    > In the next module, we'll look at porting an existing .NET Framework desktop application to .NET Core.

## Exercise 2 - Generate a package for your desktop app (optional)

### Steps

1. Close all instances of Visual Studio.

2. Open **Visual Studio Installer**. Modify you Visual Studo 2019 install.

3. Check to see if "??" is selected. If it isn't, select it and click **Apply**. Wait a moment or two while Visual Studio updates.

4. Now that "???" is installed, open the Visual Studio solution ("MyDesktopApp.sln") that you created in the previous exercise.

5. Add a **Windows Application Packaging Project** project to your solution. Name it "MyDesktopApp.Package".

6. Set the **Target Version** of this project to any version that you want, but make sure to set the **Minimum Version** to "Windows 10 Anniversary Update".

7. In Solution Explorer, right-click the **Applications** folder under the packaging project and choose **Add Reference**. 

8. Choose your desktop application project ("MyDesktopApp.App"), and then click the OK button.

9. Right-click the "MyDesktopApp.App", under **Applications** and then choose **Set as Entry Point**.

10. In Solution Explorer, right-click the packaging project node and select **Edit Project File**.

11. Locate the ```<Import Project="$(WapProjPath)\Microsoft.DesktopBridge.targets" />``` element in the file.

12. Replace this element with the following XML:

    ```xml
    <ItemGroup>
        <SDKReference Include="Microsoft.VCLibs,Version=14.0">
            <TargetedSDKConfiguration Condition="'$(Configuration)'!='Debug'">Retail</TargetedSDKConfiguration>
            <TargetedSDKConfiguration Condition="'$(Configuration)'=='Debug'">Debug</TargetedSDKConfiguration>
            <TargetedSDKArchitecture>$(PlatformShortName)</TargetedSDKArchitecture>
            <Implicit>true</Implicit>
        </SDKReference>
    </ItemGroup>
    <Import Project="$(WapProjPath)\Microsoft.DesktopBridge.targets" />
    <Target Name="_StompSourceProjectForWapProject" BeforeTargets="_ConvertItems">
        <ItemGroup>
            <_TemporaryFilteredWapProjOutput Include="@(_FilteredNonWapProjProjectOutput)" />
            <_FilteredNonWapProjProjectOutput Remove="@(_TemporaryFilteredWapProjOutput)" />
            <_FilteredNonWapProjProjectOutput Include="@(_TemporaryFilteredWapProjOutput)">
            <SourceProject>
            </SourceProject>
            </_FilteredNonWapProjProjectOutput>
        </ItemGroup>
    </Target>
    ```

13. Save the project file and close it.

14. Build the packaging project to ensure that no errors appear.

15. Right-click "MyDesktopApp.Package" and select **Publish** -> **Create app packages...**.

16. Select **Sideoading**, uncheck "Enable automatic updates" and click **Next**.

17. Select "No, skip package signing." and click **Next**.

18. Under solution configuration mappings select "x86" and "x64" and click **Create**.

19. Wait a moment while Visual Studio publishes your application package. Once complete, open the **Output location**.

20. From File Explorer, find the  "MyDesktopApp.Package_1.0.1.0_x64.msixbundle". This is the newly creating MSIX Application Bundle.

    > **Note:** The Application Bundle is not signed so Windows won't let you install it.