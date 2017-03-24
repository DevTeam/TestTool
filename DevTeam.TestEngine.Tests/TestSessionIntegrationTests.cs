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
            var session = CreateSession();

            // When
            var cases = session.Discover(Integration.GetSource()).Filter(typeof(SimpleTest)).ToArray();

            // Then
            cases.Length.ShouldBe(2);
        }

        [Test]
        public void ShouldRunTests()
        {
            // Given
            var session = CreateSession();

            // When
            var cases = session.Discover(Integration.GetSource()).Filter(typeof(SimpleTest)).ToArray();
            var results = session.RunAll(cases);

            // Then
            results.Length.ShouldBe(2);
        }

        [Test]
        public void ShouldRunTestsWhenParams()
        {
            // Given
            var session = CreateSession();

            // When
            var cases = session.Discover(Integration.GetSource()).Filter(typeof(ParamsTest)).ToArray();
            var results = session.RunAll(cases);

            // Then
            results.Length.ShouldBe(8);
        }

        private static ISession CreateSession()
        {
            var container = Integration.CreateContainer();
            return container.Resolve().Instance<ISession>();
        }
    }
}
