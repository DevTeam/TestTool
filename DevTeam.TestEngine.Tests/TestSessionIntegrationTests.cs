namespace DevTeam.TestEngine.Tests
{
    using System.Linq;
    using Contracts;
    using NUnit.Framework;

    using Shouldly;
    using IoC.Contracts;
    using Sandbox;

    [TestFixture]
    public class TestSessionIntegrationTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void ShouldDiscoverTests()
        {
            // Given
            var testEngine = CreateInstance();

            // When
            var tests = testEngine.Discover(Integration.GetSource()).Filter(typeof(SimpleTest)).ToArray();

            // Then
            tests.Length.ShouldBe(2);
        }

        [Test]
        public void ShouldRunTests()
        {
            // Given
            var testEngine = CreateInstance();

            // When
            var tests = testEngine.Discover(Integration.GetSource()).Filter(typeof(SimpleTest)).ToArray();
            var results = testEngine.RunAll(tests);

            // Then
            results.Length.ShouldBe(2);
        }

        [Test]
        public void ShouldRunTestsWhenParams()
        {
            // Given
            var testEngine = CreateInstance();

            // When
            var tests = testEngine.Discover(Integration.GetSource()).Filter(typeof(ParamsTest)).ToArray();
            var results = testEngine.RunAll(tests);

            // Then
            results.Length.ShouldBe(8);
        }

        private static ITestSession CreateInstance()
        {
            var container = Integration.CreateContainer();
            return container.Resolve().Instance<ITestSession>();
        }
    }
}
