namespace DevTeam.TestAdapter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using IoC;
    using IoC.Configurations.Json;
    using IoC.Contracts;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
    using TestEngine.Contracts;
    using TestEngine.Contracts.Reflection;
    using TestCase = Microsoft.VisualStudio.TestPlatform.ObjectModel.TestCase;

    [FileExtension(".dll")]
    [FileExtension(".exe")]
    [DefaultExecutorUri(ExecutorUri)]
    [ExtensionUri(ExecutorUri)]
    public class TestAdapter : ITestDiscoverer, ITestExecutor
    {
        public const string ExecutorUri = "executor://devteam/TestRunner";
        private readonly Container _container;
        private readonly ITestExplorer _testExplorer;

        public TestAdapter()
        {
            _container = new Container("root").Configure()
                .DependsOn<JsonConfiguration>(ReadIoCConfiguration()).ToSelf()
                .Register().Contract<IReflection>().Autowiring<Reflection>().ToSelf();

            _testExplorer = _container.Resolve().Instance<ITestExplorer>();
        }

        public void DiscoverTests(
            IEnumerable<string> sources,
            IDiscoveryContext discoveryContext,
            IMessageLogger logger,
            ITestCaseDiscoverySink discoverySink)
        {
            logger.SendMessage(TestMessageLevel.Informational, "DiscoverTests");
            foreach (var testCase in GetTestCases(sources))
            {
                discoverySink.SendTestCase(testCase);
            }
        }

        public void RunTests(IEnumerable<TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            frameworkHandle.SendMessage(TestMessageLevel.Informational, "RunTests");
            frameworkHandle.SendMessage(TestMessageLevel.Informational, runContext.RunSettings.SettingsXml);
            foreach (var test in tests)
            {
                frameworkHandle.RecordStart(test);
                frameworkHandle.RecordResult(new TestResult() {Outcome = TestOutcome.Passed, DisplayName = test.DisplayName });
                frameworkHandle.RecordEnd(test, TestOutcome.Passed);
                frameworkHandle.SendMessage(TestMessageLevel.Informational, test.DisplayName);
            }
        }

        public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            var runConfiguration = runContext.RunSettings.SettingsXml;
            RunTests(GetTestCases(sources), runContext, frameworkHandle);
        }

        public void Cancel()
        {
        }

        private IEnumerable<TestCase> GetTestCases(IEnumerable<string> sources)
        {
            var executorUri = new Uri(ExecutorUri);
            return
                from testAssembly in _testExplorer.ExploreSources(sources)
                from testClass in testAssembly.Classes
                from testMethod in testClass.Methods
                from testCase in testMethod.Cases
                let testElements = new ITestElement[] { testAssembly, testClass, testMethod, testCase }
                select new TestCase(
                    string.Join(":", testElements.Select(i => i.FullyQualifiedName).Where(i => !string.IsNullOrWhiteSpace(i))),
                    executorUri,
                    testAssembly.Source
                )
                {
                    Id = testCase.Id,
                    DisplayName = string.Join(":", testElements.Select(i => i.DisplayName).Where(i => !string.IsNullOrWhiteSpace(i)))
                };
        }

        private string ReadIoCConfiguration()
        {
            using (var configReader = new StreamReader(GetType().GetTypeInfo().Assembly.GetManifestResourceStream("DevTeam.TestAdapter.DevTeam.TestEngine.json")))
            {
                return configReader.ReadToEnd();
            }
        }
    }
}