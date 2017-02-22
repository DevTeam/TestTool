﻿namespace DevTeam.TestEngine.Tests
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
    public class TestExplorerTests
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
            var explorer = CreateInstance();

            // When
            var testAssemblies = explorer.ExploreSources(Enumerable.Repeat(@"C:\Projects\DevTeam\TestTool\dotNetCore\SimpleTests\bin\Debug\SimpleTests.dll", 1)).ToList();

            // Then
        }

        private ITestExplorer CreateInstance()
        {
            return _container.Resolve().Instance<ITestExplorer>();
        }
    }
}
