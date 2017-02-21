namespace DevTeam.TestAdapter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
    using TestEngine;
    using TestEngine.Contracts;
    using TestCase = Microsoft.VisualStudio.TestPlatform.ObjectModel.TestCase;

    [FileExtension(".dll")]
    [FileExtension(".exe")]
    [DefaultExecutorUri(ExecutorUri)]
    [ExtensionUri(ExecutorUri)]
    public class TestAdapter : ITestDiscoverer, ITestExecutor
    {
        public const string ExecutorUri = "executor://devteam/TestRunner";

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

        private static IEnumerable<TestCase> GetTestCases(IEnumerable<string> sources)
        {
            var testExplorer = new TestExplorer(new Reflection());
            var executorUri = new Uri(ExecutorUri);
            return 
                from source in sources
                let testAssembly = testExplorer.Explore(source)
                from testClass in testAssembly.Classes
                from testMethod in testClass.Methods
                from testCase in testMethod.Cases
                let testElements = new ITestElement[] { testAssembly, testClass, testMethod, testCase }
                select new TestCase(
                    string.Join(":", testElements.Select(i => i.FullyQualifiedName).Where(i => !string.IsNullOrWhiteSpace(i))),
                    executorUri,
                    source
                )
                {
                    Id = testCase.Id,
                    DisplayName = string.Join(":", testElements.Select(i => i.DisplayName).Where(i => !string.IsNullOrWhiteSpace(i)))
                };
        }
    }
}