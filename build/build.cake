#tool "nuget:?package=xunit.runner.console"

var configuration = Argument("Configuration", "Release");
var solutionPath = Argument("SolutionPath", @"../BillValidator.sln");

Task("Clean")
    .Does(() =>
{
    DotNetCoreClean(solutionPath);
});

Task("Restore-Nuget")
    .Does(()=> {
        DotNetCoreRestore(solutionPath);
});

Task("Build")
    .Does(()=>
{

    var settings = new DotNetCoreBuildSettings
    {
        Configuration = configuration
    };

    DotNetCoreBuild(solutionPath, settings);
});

Task("Run-Unit-Tests")
    .Does(()=>
{
    //var testAssemblies = GetFiles("./tests/**/**/"+configuration + "/*.Unit.Test.dll");
    //XUnit2(testAssemblies);
    var projects = GetFiles("../tests/**/*.csproj");
        foreach(var project in projects)
        {
            DotNetCoreTest(
                project.FullPath,
                new DotNetCoreTestSettings()
                {
                    Configuration = configuration,
                    NoBuild = true
                });
        }
});

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore-Nuget")
    .IsDependentOn("Build")
    .IsDependentOn("Run-Unit-Tests")
    ;

RunTarget("Default");