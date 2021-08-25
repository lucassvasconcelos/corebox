#addin nuget:?package=Cake.Coverlet&version=2.5.4
var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");
var solution = "./CoreBox.sln";

Task("Clean").Does(() => {
    CleanDirectories($"./src/**/bin/{configuration}");
    CleanDirectories($"./tests/**/bin/{configuration}");
});

Task("Restore").IsDependentOn("Clean").Does(() => {
    DotNetCoreRestore(solution, new DotNetCoreRestoreSettings { NoCache = true });
});

Task("Build").IsDependentOn("Restore").Does(() => {
    DotNetCoreBuild(solution, new DotNetCoreBuildSettings { Configuration = configuration });
});

Task("Test").IsDependentOn("Build").Does(() => {
    var coverletSettings = new CoverletSettings
    {
        CollectCoverage = true,
        CoverletOutputFormat = CoverletOutputFormat.cobertura,
        CoverletOutputDirectory = Directory(@"./tests/.coverage"),
        CoverletOutputName = $"coverage-result",
        ThresholdType = ThresholdType.Line,
        Threshold = 100
    };

    var testSettings = new DotNetCoreTestSettings
    {
        Configuration = configuration,
        NoBuild = true,
    };

    DotNetCoreTest(solution, testSettings, coverletSettings);
});

RunTarget(target);