namespace DevTeam.TestEngine.Tests
{
    using System.Linq;
    using Contracts;
    using Shouldly;
    using IoC.Contracts;
    using Sandbox;
    using Xunit;

    public class TestEngineIntegrationTests
    {
        [Fact]
        public void ShouldDiscoverTests()
        {
            // Given
            var session = CreateSession();

            // When
            var cases = session.Discover(Integration.GetSource()).Filter(typeof(SimpleTest)).ToArray();

            // Then
            cases.Length.ShouldBe(2);
        }

        [Fact]
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

        [Fact]
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

        [Fact]
        public void ShouldRunTestsWhenGenericArgsTest()
        {
            // Given
            var session = CreateSession();

            // When
            var cases = session.Discover(Integration.GetSource()).Filter(typeof(GenericArgsTest<,>)).ToArray();
            var results = session.RunAll(cases);

            // Then
            results.Length.ShouldBe(16);
        }

        [Fact]
        public void ShouldRunTestsWhenCaseSource()
        {
            // Given
            var session = CreateSession();

            // When
            var cases = session.Discover(Integration.GetSource()).Filter(typeof(CaseSourceTest)).ToArray();
            var results = session.RunAll(cases);

            // Then
            results.Length.ShouldBe(4);
        }

        [Fact]
        public void ShouldRunTestsWhenCaseSingleItemSource()
        {
            // Given
            var session = CreateSession();

            // When
            var cases = session.Discover(Integration.GetSource()).Filter(typeof(CaseSingleItemSourceTest)).ToArray();
            var results = session.RunAll(cases);

            // Then
            results.Length.ShouldBe(4);
        }

        [Fact]
        public void ShouldRunTestsWhenGenericArgsSource()
        {
            // Given
            var session = CreateSession();

            // When
            var cases = session.Discover(Integration.GetSource()).Filter(typeof(GenericArgsSourceTest<,>)).ToArray();
            var results = session.RunAll(cases);

            // Then
            results.Length.ShouldBe(2);
        }

        [Fact]
        public void ShouldRunTestsWhenGenericArgsSingleItemSource()
        {
            // Given
            var session = CreateSession();

            // When
            var cases = session.Discover(Integration.GetSource()).Filter(typeof(GenericArgsSingleItemSourceTest<>)).ToArray();
            var results = session.RunAll(cases);

            // Then
            results.Length.ShouldBe(2);
        }

        private static ISession CreateSession()
        {
            var container = Integration.CreateContainer();
            return container.Resolve().Instance<ISession>();
        }
    }
}
