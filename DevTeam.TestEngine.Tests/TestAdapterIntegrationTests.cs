namespace DevTeam.TestEngine.Tests
{
    using Helpers;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class TestAdapterIntegrationTests
    {
        [Test]
        [TestCase(@"TestData\TestProject\TestProject.csproj")]
        public void ShouldDiscoverTests(string projectName)
        {
            // Given
            var testCommandLine = new CommandLine(
                @"dotnet",
                "test",
                projectName
            );

            // testCommandLine.AddEnvitonmentVariable("VSTEST_HOST_DEBUG", "1");

            // When
            testCommandLine.TryExecute(out CommandLineResult result).ShouldBe(true);

            // Then
            result.ExitCode.ShouldBe(0);
            result.StdError.Trim().ShouldBe(string.Empty);
        }
    }
}
