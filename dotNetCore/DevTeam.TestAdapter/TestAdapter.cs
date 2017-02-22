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
    using ITestDiscoverer = TestEngine.Contracts.ITestDiscoverer;
    using ITestExecutor = TestEngine.Contracts.ITestExecutor;
    using TestCase = Microsoft.VisualStudio.TestPlatform.ObjectModel.TestCase;

    [FileExtension(".dll")]
    [FileExtension(".exe")]
    [DefaultExecutorUri(ExecutorUri)]
    [ExtensionUri(ExecutorUri)]
    public class TestAdapter : Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter.ITestDiscoverer, Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter.ITestExecutor
    {
        public const string ExecutorUri = "executor://devteam/TestRunner";
        private readonly List<ITestDiscoverer> _testDiscoverer;
        private readonly List<ITestExecutor> _testExecutor;
        private readonly ITestElementFactory _testElementFactory;

        public TestAdapter()
        {
            var container = new Container("root").Configure()
                .DependsOn<JsonConfiguration>(ReadIoCConfiguration()).ToSelf()
                .Register().Contract<IReflection>().Autowiring<Reflection>().ToSelf();

            _testDiscoverer = container.Resolve().Instance<IEnumerable<ITestDiscoverer>>().ToList();
            _testElementFactory = container.Resolve().Instance<ITestElementFactory>();
            _testExecutor = container.Resolve().Instance<IEnumerable<ITestExecutor>>().ToList();
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
            var testDict = tests.ToDictionary(i => i.Id, i => i);
            var assemblies = _testElementFactory.RestoreTestAssemblies(testDict.Values.ToDictionary(i => i.Id, i => i.FullyQualifiedName)).ToList();
            foreach (var testExecutor in _testExecutor)
            {
                foreach (var testCaseInfo in testExecutor.Run(assemblies))
                {
                    TestCase test;
                    if (!testDict.TryGetValue(testCaseInfo.Case.Id, out test))
                    {
                        continue;
                    }

                    switch (testCaseInfo.State)
                    {
                        case TestCaseState.Starting:
                            frameworkHandle.RecordStart(test);
                            break;

                        case TestCaseState.Success:
                            frameworkHandle.RecordResult(new TestResult { Outcome = TestOutcome.Passed, DisplayName = test.DisplayName });
                            frameworkHandle.RecordEnd(test, TestOutcome.Passed);
                            frameworkHandle.SendMessage(TestMessageLevel.Informational, test.DisplayName);
                            break;
                    }
                }
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
                from testDiscoverer in _testDiscoverer
                from testAssembly in testDiscoverer.ExploreSources(sources)
                from testClass in testAssembly.Classes
                from testMethod in testClass.Methods
                from testCase in testMethod.Cases
                select new TestCase(
                        testCase.FullyQualifiedCaseName,
                        executorUri,
                        testAssembly.Source
                    )
                    {
                        Id = testCase.Id,
                        DisplayName = testCase.DisplayName
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