#addin nuget:?package=Cake.Coverlet&version=2.5.4
var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");
var solution = "./CoreBox.sln";
var gitVersion = EnvironmentVariable<string>("GIT_VERSION", "");

Task("Clean").Does(() => {
    CleanDirectories($"./src/**/bin/{configuration}");
    CleanDirectories($"./tests/**/bin/{configuration}");
});

Task("Restore").IsDependentOn("Clean").Does(() => {
    DotNetCoreRestore(solution, new DotNetCoreRestoreSettings { NoCache = true });
});

Task("Build").IsDependentOn("Restore").Does(() => {
    if (gitVersion != "")
        DotNetCoreBuild(solution, new DotNetCoreBuildSettings { 
            Configuration = configuration, 
            MSBuildSettings = new DotNetCoreMSBuildSettings().WithProperty("Version", gitVersion)
        });
    else
        DotNetCoreBuild(solution, new DotNetCoreBuildSettings { Configuration = configuration });
});

Task("Test").IsDependentOn("Build").Does(() => {
    var coverletSettings = new CoverletSettings
    {
        CollectCoverage = true,
        CoverletOutputFormat = CoverletOutputFormat.cobertura,
        CoverletOutputDirectory = Directory("./tests/.coverage"),
        CoverletOutputName = "cov",
        ThresholdType = ThresholdType.Branch,
        Threshold = 100
    };

    var testSettings = new DotNetCoreTestSettings { Configuration = configuration, NoBuild = true };
    DotNetCoreTest(solution, testSettings, coverletSettings);
});

RunTarget(target);