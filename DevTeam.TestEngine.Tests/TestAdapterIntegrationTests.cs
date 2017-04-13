namespace DevTeam.TestEngine.Tests
{
    using System.IO;
    using Helpers;
    using Shouldly;
    using Xunit;

    public class TestAdapterIntegrationTests
    {
        [Theory]
        [InlineData(@"TestData\TestProject\TestProject.csproj", "net462")]
        [InlineData(@"TestData\TestProject\TestProject.csproj", "netcoreapp1.0")]
        public void ShouldRunTests(string projectName, string targetFramework)
        {
            // Given
            var testCommandLine = new CommandLine(
                @"dotnet",
                "test",
                projectName,
#if DEBUG
                "-c:Debug"
#else
                "-c:Release"
#endif
                ,$"/p:TargetFramework={targetFramework}"
            );

            // testCommandLine.AddEnvitonmentVariable("VSTEST_HOST_DEBUG", "1");
            // testCommandLine.AddEnvitonmentVariable("VSTEST_RUNNER_DEBUG", "1");

            // When
            testCommandLine.TryExecute(out CommandLineResult result).ShouldBe(true);

            // Then
            result.ExitCode.ShouldBe(0);
            result.StdError.Trim().ShouldBe(string.Empty);
        }

        [Theory]
        [InlineData(@"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe", "netcoreapp1.0", false)]
        [InlineData(@"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe", "net462", false)]
        [InlineData(@"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe", "net45", false)]
        [InlineData(@"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe", "netcoreapp1.0", true)]
        [InlineData(@"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe", "net462", true)]
        [InlineData(@"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe", "net45", true)]
        public void ShouldRunTestsUsingIdeTool(string vstest, string targetFramework, bool specifyPath)
        {
            // Given
#if DEBUG
            var configuration = "Debug";
#else
            var configuration = "Release";
#endif
            var testAssemblyFileName = $@"TestData\TestProject\bin\{configuration}\{targetFramework}\TestProject.dll";
            CommandLine testCommandLine;
            if (specifyPath)
            {
                testCommandLine = new CommandLine(vstest, testAssemblyFileName, $"/TestAdapterPath:{Path.GetDirectoryName(testAssemblyFileName)}");
            }
            else
            {
                testCommandLine = new CommandLine(vstest, testAssemblyFileName);
            }

            //testCommandLine.AddEnvitonmentVariable("VSTEST_HOST_DEBUG", "1");
            //testCommandLine.AddEnvitonmentVariable("VSTEST_RUNNER_DEBUG", "1");

            // When
            testCommandLine.TryExecute(out CommandLineResult result).ShouldBe(true);

            // Then
            result.ExitCode.ShouldBe(0);
            result.StdError.Trim().ShouldBe(string.Empty);
        }
    }
}
