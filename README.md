# Azure Pipeline CI With MsTest UnitTests VSTest CI
## Command Line
```
dotnet build Playwright.NET.UnitTests.Azure.Pipeline.sln

dotnet new tool-manifest --force

dotnet tool install Microsoft.Playwright.CLI

dotnet tool run playwright install

dotnet test **/*UnitTests*.dll

dotnet test **/*UnitTests*.csproj
```


