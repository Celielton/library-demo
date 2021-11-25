#tool nuget:?package=MSBuild.SonarQube.Runner.Tool&version=4.6.0
#addin nuget:?package=Cake.Sonar&version=1.1.22
#addin nuget:?package=Cake.Git&version=0.21.0

var target = Argument("target", "Sonar");
var configuration = Argument("configuration", "Release");
var sonarLogin = "admin";
var sonarPassword = "admin";
var branch = GitBranchCurrent(".").FriendlyName;
var projectName = "library-api";
var solutionName = "library-api.sln";

// Run dotnet restore to restore all package references.
Task("Clean")
    .Does(() =>
{
    CleanDirectory($"./src/Presentation/{projectName}.API/bin/{configuration}");
    CleanDirectory($"./{projectName}.Test/bin/{configuration}");
    var files = GetFiles($"{projectName}.Test/coverage.opencover.xml");
    DeleteFiles(files);         
});

Task("Restore")

    .IsDependentOn("SonarBegin")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        DotNetCoreRestore(solutionName);
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        DotNetCoreBuild(solutionName,
           new DotNetCoreBuildSettings()
                {
                    Configuration = configuration
                });
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var projects = GetFiles("./**/*.csproj");
        foreach(var project in projects)
        {
            DotNetCoreTest(
                project.FullPath,
                new DotNetCoreTestSettings()
                {
                    Configuration = configuration,
                    NoBuild = true,
                    ArgumentCustomization = args => 
                                    args.Append("/p:CollectCoverage=true")
                                        .Append("/p:CoverletOutputFormat=opencover")
                });
        }
    });

Task("SonarBegin")
    .Does(() => 
    {
        SonarBegin(new SonarBeginSettings {
            Key = projectName + ".API",
            //Branch = branch,
            //Organization = "[SONARCLOUD-ORGANIZATION]",
            Url = "http://localhost:9000",
            Exclusions = "**/Samples/**/*.cs,**/*.Tests/*.cs",
            OpenCoverReportsPath = "**/*.opencover.xml",
            Login = sonarLogin,
            Password = sonarPassword
        });
    });

Task("Sonar")
  .IsDependentOn("Test")  
  .Does(() => {
     SonarEnd(new SonarEndSettings{
        Login = sonarLogin,
        Password = sonarPassword
     });
  });

RunTarget(target);
//https://andrewlock.net/running-tests-with-dotnet-xunit-using-cake/
//https://diegogiacomelli.com.br/publishing-a-dotnet-core-project-to-sonarcloud-with-cake/