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
        public void ShouldRunTestsWhenArgs()
        {
            // Given
            var session = CreateSession();

            // When
            var cases = session.Discover(Integration.GetSource()).Filter(typeof(SimpleArgsTest)).ToArray();
            var results = session.RunAll(cases);

            // Then
            results.Length.ShouldBe(8);
            results.All(result => result.State == State.Passed).ShouldBe(true);
        }

        [Fact]
        public void ShouldRunTestsWhenClassGenericArgs()
        {
            // Given
            var session = CreateSession();

            // When
            var cases = session.Discover(Integration.GetSource()).Filter(typeof(ClassGenericArgsTest<,>)).ToArray();
            var results = session.RunAll(cases);

            // Then
            results.Length.ShouldBe(16);
            results.All(result => result.State == State.Passed).ShouldBe(true);
        }

        [Fact]
        public void ShouldRunTestsWhenArgsSource()
        {
            // Given
            var session = CreateSession();

            // When
            var cases = session.Discover(Integration.GetSource()).Filter(typeof(SimpleArgsSourceTest)).ToArray();
            var results = session.RunAll(cases);

            // Then
            results.Length.ShouldBe(4);
            results.All(result => result.State == State.Passed).ShouldBe(true);
        }

        [Fact]
        public void ShouldRunTestsWhenArgsSingleItemSource()
        {
            // Given
            var session = CreateSession();

            // When
            var cases = session.Discover(Integration.GetSource()).Filter(typeof(ArgsSingleItemSourceTest)).ToArray();
            var results = session.RunAll(cases);

            // Then
            results.Length.ShouldBe(4);
            results.All(result => result.State == State.Passed).ShouldBe(true);
        }

        [Fact]
        public void ShouldRunTestsWhenGenericArgsSource()
        {
            // Given
            var session = CreateSession();

            // When
            var cases = session.Discover(Integration.GetSource()).Filter(typeof(ClassGenericArgsSourceTest<,>)).ToArray();
            var results = session.RunAll(cases);

            // Then
            results.Length.ShouldBe(2);
            results.All(result => result.State == State.Passed).ShouldBe(true);
        }

        [Fact]
        public void ShouldRunTestsWhenClassTypesSingleItemSource()
        {
            // Given
            var session = CreateSession();

            // When
            var cases = session.Discover(Integration.GetSource()).Filter(typeof(ClassTypesSingleItemSourceTest<>)).ToArray();
            var results = session.RunAll(cases);

            // Then
            results.Length.ShouldBe(2);
            results.All(result => result.State == State.Passed).ShouldBe(true);
        }

        [Fact]
        public void ShouldIgnoreTests()
        {
            // Given
            var session = CreateSession();

            // When
            var cases = session.Discover(Integration.GetSource()).Filter(typeof(IgnoreTest)).ToArray();
            var results = session.RunAll(cases);

            // Then
            cases.Length.ShouldBe(2);
            results.All(result => result.State == State.Skiped).ShouldBe(true);
            results[1].Messages.Count(i => i.Type == MessageType.State && i.Text == "some reason").ShouldBe(1);
        }

        [Fact]
        public void ShouldIgnoreAllTests()
        {
            // Given
            var session = CreateSession();

            // When
            var cases = session.Discover(Integration.GetSource()).Filter(typeof(IgnoreAllTest)).ToArray();
            var results = session.RunAll(cases);

            // Then
            cases.Length.ShouldBe(2);
            results.All(result => result.State == State.Skiped).ShouldBe(true);
        }

        [Fact]
        public void ShouldRunTestsWhenMethodGenericArgs()
        {
            // Given
            var session = CreateSession();

            // When
            var cases = session.Discover(Integration.GetSource()).Filter(typeof(MethodGenericArgsTest)).ToArray();
            var results = session.RunAll(cases);

            // Then
            results.Length.ShouldBe(2);
            results.All(result => result.State == State.Passed).ShouldBe(true);
        }

        [Fact]
        public void ShouldRunTestsWhenMethodGenericArgsSource()
        {
            // Given
            var session = CreateSession();

            // When
            var cases = session.Discover(Integration.GetSource()).Filter(typeof(MethodGenericArgsSourceTest)).ToArray();
            var results = session.RunAll(cases);

            // Then
            results.Length.ShouldBe(3);
            results.All(result => result.State == State.Passed).ShouldBe(true);
        }

        private static ISession CreateSession()
        {
            var container = Integration.CreateContainer();
            return container.Resolve().Instance<ISession>();
        }
    }
}
