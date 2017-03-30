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
    using TestCase = Microsoft.VisualStudio.TestPlatform.ObjectModel.TestCase;

    [FileExtension(".dll")]
    [FileExtension(".exe")]
    [DefaultExecutorUri(ExecutorId)]
    [ExtensionUri(ExecutorId)]
    public class TestAdapter : ITestDiscoverer, ITestExecutor
    {
        public const string ExecutorId = "executor://devteam/TestRunner";
        private static readonly Uri ExecutorUri = new Uri(ExecutorId);
        private readonly ISession _session;
        private bool _canceled;

        public TestAdapter()
        {
            var container = new Container("root").Configure()
                .DependsOn<JsonConfiguration>(ReadIoCConfiguration()).ToSelf();

            _session = container.Resolve().Instance<ISession>();
        }

        public void DiscoverTests(
            IEnumerable<string> sources,
            IDiscoveryContext discoveryContext,
            IMessageLogger logger,
            ITestCaseDiscoverySink discoverySink)
        {
            foreach (var testCase in Discover(sources))
            {
                discoverySink.SendTestCase(testCase);
            }
        }

        public void RunTests(IEnumerable<TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            frameworkHandle.SendMessage(TestMessageLevel.Informational, "RunTests");
            frameworkHandle.SendMessage(TestMessageLevel.Informational, runContext.RunSettings.SettingsXml);

            foreach (var testCase in tests)
            {
                frameworkHandle.RecordStart(testCase);
                var result = _session.Run(testCase.Id);
                frameworkHandle.RecordResult(new TestResult { Outcome = TestOutcome.Passed, DisplayName = testCase.DisplayName });
                frameworkHandle.RecordEnd(testCase, TestOutcome.Passed);
                if (_canceled)
                {
                    _canceled = false;
                    break;
                }
            }
        }

        public void RunTests([IoC.Contracts.NotNull] IEnumerable<string> sources, [IoC.Contracts.NotNull] IRunContext runContext, [IoC.Contracts.NotNull] IFrameworkHandle frameworkHandle)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            if (runContext == null) throw new ArgumentNullException(nameof(runContext));
            if (frameworkHandle == null) throw new ArgumentNullException(nameof(frameworkHandle));
            RunTests(Discover(sources), runContext, frameworkHandle);
        }

        public void Cancel()
        {
            _canceled = true;
        }

        private IEnumerable<TestCase> Discover([IoC.Contracts.NotNull] IEnumerable<string> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            return
                from source in sources
                from testCase in _session.Discover(source)
                select new TestCase(testCase.ToString(), ExecutorUri, testCase.Source)
                {
                    DisplayName = $"{testCase.TypeName}{GetTypesString(testCase.TypeGenericArgs)}{GetParametersString(testCase.TypeParameters)}.{testCase.MethodName}{GetParametersString(testCase.MethodParaeters)}",
                    CodeFilePath = testCase.CodeFilePath
                };
        }

        private string GetParametersString(string[] parameters)
        {
            if (parameters.Length == 0)
            {
                return string.Empty;
            }

            return $"({string.Join(", ", parameters)})";
        }

        private static string GetTypesString(string[] types)
        {
            if (types == null) throw new ArgumentNullException(nameof(types));
            if (types.Length == 0)
            {
                return string.Empty;
            }

            return $"<{string.Join(", ", types)}>";
        }

        private string ReadIoCConfiguration()
        {
            return File.ReadAllText(Path.Combine(Path.GetDirectoryName(GetType().GetTypeInfo().Assembly.Location), "DevTeam.TestEngine.ioc"));
        }
    }
}