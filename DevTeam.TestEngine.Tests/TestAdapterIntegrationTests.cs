namespace DevTeam.TestEngine.Tests
{
    using Helpers;
    using Shouldly;
    using Xunit;

    public class TestAdapterIntegrationTests
    {
        [Theory]
        [InlineData(@"TestData\TestProject\TestProject.csproj")]
        public void ShouldDiscoverTests(string projectName)
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
            );

            

            // testCommandLine.AddEnvitonmentVariable("VSTEST_HOST_DEBUG", "1");
            // testCommandLine.AddEnvitonmentVariable("VSTEST_RUNNER_DEBUG", "1");

            // When
            testCommandLine.TryExecute(out CommandLineResult result).ShouldBe(true);

            // Then
            result.ExitCode.ShouldBe(0);
            result.StdError.Trim().ShouldBe(string.Empty);
        }
    }
}
