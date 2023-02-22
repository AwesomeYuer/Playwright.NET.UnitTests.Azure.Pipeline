# Command Line
```shell
dotnet new tool-manifest --force

dotnet tool install Microsoft.Playwright.CLI

dotnet build Playwright.NET.UnitTests.Azure.Pipeline.sln

dotnet tool run playwright install

//for linux
dotnet tool run playwright install --force msedge

dotnet test **/*UnitTests*.dll

dotnet test **/*UnitTests*.csproj
```



# Windows
[![Build Status](https://microshaoft.visualstudio.com/AzurePipelines/_apis/build/status/AwesomeYuer.Playwright.NET.UnitTests.Azure.Pipeline-Windows?branchName=master)](https://microshaoft.visualstudio.com/AzurePipelines/_build/latest?definitionId=43&branchName=master)

# Linux
[![Build Status](https://microshaoft.visualstudio.com/AzurePipelines/_apis/build/status/AwesomeYuer.Playwright.NET.UnitTests.Azure.Pipeline-Linux?branchName=master)](https://microshaoft.visualstudio.com/AzurePipelines/_build/latest?definitionId=42&branchName=master)