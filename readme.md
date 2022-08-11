**Notas**

-   Visual Studio Workspace Settings:

    -   .NET Core Test Explorer -> Test Project Path = Backend/*/*Tests.csproj
    -   .NET Core Test Explorer -> Test Arguments = /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./TestResults/coverage/lcov.info

-   Test: dotnet test /p:CollectCoverage=true

-   Generate report:

    -   dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:Exclude="[xunit*]\*" /p:CoverletOutput="./TestResults/"
    -   dotnet reportgenerator "-reports:TestResults\coverage.cobertura.xml" "-targetdir:TestResults\html" -reporttypes:HTML;

-   Update database

    -   dotnet ef migrations add v2 -p ../Backend.Repository
    -   dotnet ef database update -p ../Backend.Repository
