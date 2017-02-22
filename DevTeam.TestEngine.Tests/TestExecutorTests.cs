namespace DevTeam.TestEngine.Tests
{
    using System.IO;
    using System.Linq;
    using Contracts;
    using Contracts.Reflection;
    using IoC;
    using IoC.Configurations.Json;
    using IoC.Contracts;
    using Moq;

    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class TestExecutorTests
    {
        private Container _container;

        [SetUp]
        public void SetUp()
        {
            _container = new Container();
            _container.Configure().DependsOn<JsonConfiguration>(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "DevTeam.TestEngine.json"))).ToSelf();
            _container.Register().Contract<IReflection>().Autowiring<Reflection>().ToSelf();
        }

        [TearDown]
        public void TearDown()
        {
            _container.Dispose();
        }

        [Test]
        public void Should()
        {
            // Given
            var testDiscoverer = CreateTestDiscoverer();
            var testExecutor = CreateTestExecutor();

            // When
            var testAssemblies = testDiscoverer.ExploreSources(Enumerable.Repeat(@"C:\Projects\DevTeam\TestTool\dotNetCore\SimpleTests\bin\Debug\SimpleTests.dll", 1)).ToList();
            var data = testExecutor.Run(testAssemblies).ToList();

            // Then
        }

        private ITestDiscoverer CreateTestDiscoverer()
        {
            return _container.Resolve().Instance<ITestDiscoverer>();
        }

        private ITestExecutor CreateTestExecutor()
        {
            return _container.Resolve().Instance<ITestExecutor>();
        }
    }
}
