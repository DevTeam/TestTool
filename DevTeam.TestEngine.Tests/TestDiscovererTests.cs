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
    using IReflection = Contracts.Reflection.IReflection;

    [TestFixture]
    public class TestDiscovererTests
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
            var testDiscoverer = CreateInstance();

            // When
            var testAssemblies = testDiscoverer.ExploreSources(Enumerable.Repeat(@"C:\Projects\DevTeam\TestTool\dotNetCore\SimpleTests\bin\Debug\SimpleTests.dll", 1)).ToList();

            // Then
        }

        private ITestDiscoverer CreateInstance()
        {
            return _container.Resolve().Tag("executor://devteam/DefaultTestExecutor").Instance<ITestDiscoverer>();
        }
    }
}
