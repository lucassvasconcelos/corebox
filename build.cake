#tool dotnet:?package=dotnet-reportgenerator-globaltool&version=5.1.11
var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");
var solution = "./CoreBox.sln";
var gitVersion = EnvironmentVariable<string>("GIT_VERSION", "");

Task("Clean").Does(() => {
    CleanDirectories($"./src/**/bin/{configuration}");
    CleanDirectories($"./tests/**/bin/{configuration}");
});

Task("Restore").IsDependentOn("Clean").Does(() => {
    DotNetRestore(solution, new DotNetRestoreSettings { NoCache = true });
});

Task("Build").IsDependentOn("Restore").Does(() => {
    if (gitVersion != "")
        DotNetBuild(solution, new DotNetBuildSettings { 
            Configuration = configuration, 
            MSBuildSettings = (new DotNetMSBuildSettings()).WithProperty("Version", gitVersion)
        });
    else
        DotNetBuild(solution, new DotNetBuildSettings { Configuration = configuration });
});

Task("Test").IsDependentOn("Build").Does(() => {
    DotNetTest(
        null,
        new DotNetTestSettings {
            ArgumentCustomization = args => 
                args.Append("/p:CollectCoverage=true,CoverletOutputFormat=opencover,Threshold=100,ThresholdType=\"line%2cbranch%2cmethod\"")
                    .Append($"--configuration Release")
                    .Append($"--no-build")
                    .Append($"--no-restore")
                    .Append($"--logger \"html;logfilename=testResults.html\"")
        }
    );
}).Finally(() => {
    ReportGenerator(
        report: "./tests/CoreBox.Tests/coverage.opencover.xml",
        $"./tests/CoreBox.Tests/TestResults/Report",
        new ReportGeneratorSettings { ArgumentCustomization = args => args.Append("-reporttypes.Html")}
    );
});

RunTarget(target);