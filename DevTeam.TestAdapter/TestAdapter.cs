namespace DevTeam.TestAdapter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using IoC;
    using IoC.Configurations.Json;
    using IoC.Contracts;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
    using TestEngine.Contracts;
    using TestCase = Microsoft.VisualStudio.TestPlatform.ObjectModel.TestCase;
    using System.Reflection;
    using System.Text;

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

        static TestAdapter()
        {
            var bin = GetBinDirectory();
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                var assemblyName = new AssemblyName(args.Name);
                var privateBin = Path.Combine(bin, "DevTeam.TestAdapter");
                var assemblyPath = Path.Combine(privateBin, assemblyName.Name + ".dll");
                if (File.Exists(assemblyPath))
                {
                    return Assembly.LoadFile(assemblyPath);
                }

                return null;
            };
        }

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
            foreach (var testCase in tests)
            {
                frameworkHandle.RecordStart(testCase);
                var startTime = DateTimeOffset.Now;
                var result = _session.Run(testCase.Id);
                var endTime = DateTimeOffset.Now;
                var testResult = new TestResult(testCase)
                {
                    DisplayName = testCase.DisplayName,
                    StartTime = startTime,
                    Duration = endTime - startTime,
                    EndTime = endTime,
                    ComputerName = Environment.MachineName
                };

                var stackTrace = new StringBuilder();
                var errorMessage = new StringBuilder();
                foreach (var message in result.Messages)
                {
                    if (message.StackTrace != null)
                    {
                        stackTrace.AppendLine(message.StackTrace);
                    }

                    switch (message.Type)
                    {
                        case MessageType.StdOutput:
                            testResult.Messages.Add(new TestResultMessage(TestResultMessage.StandardOutCategory, message.Message));
                            break;

                        case MessageType.StdError:
                            testResult.Messages.Add(new TestResultMessage(TestResultMessage.StandardErrorCategory, message.Message));
                            break;

                        case MessageType.Exception:
                            errorMessage.AppendLine(message.Message);
                            break;

                        case MessageType.Trace:
                            testResult.Messages.Add(new TestResultMessage(TestResultMessage.DebugTraceCategory, message.Message));
                            break;

                        default:
                            testResult.Messages.Add(new TestResultMessage(TestResultMessage.AdditionalInfoCategory, message.Message));
                            break;
                    }
                }

                testResult.ErrorStackTrace = stackTrace.ToString();
                testResult.ErrorMessage = errorMessage.ToString();

                switch (result.State)
                {
                    case State.Passed:
                        testResult.Outcome = TestOutcome.Passed;
                        break;

                    case State.Failed:
                        testResult.Outcome = TestOutcome.Failed;
                        break;

                    case State.Skiped:
                        testResult.Outcome = TestOutcome.Skipped;
                        break;

                    case State.NotFound:
                        testResult.Outcome = TestOutcome.NotFound;
                        break;

                    default:
                        testResult.Outcome = TestOutcome.None;
                        break;
                }

                frameworkHandle.RecordResult(testResult);
                frameworkHandle.RecordEnd(testCase, testResult.Outcome);
                frameworkHandle.SendMessage(TestMessageLevel.Informational, $"{testCase} - {testResult.Outcome}");
                if (_canceled)
                {
                    frameworkHandle.SendMessage(TestMessageLevel.Informational, "Canceled");
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
                let testName = testCase.ToString()
                select new TestCase(testCase.ToString(), ExecutorUri, testCase.Source)
                {
                    Id = testCase.Id,
                    DisplayName = testName,
                    CodeFilePath = testCase.CodeFilePath,
                    FullyQualifiedName = testName,
                    LineNumber = testCase.LineNumber ?? 0,
                };
        }

        private static string ReadIoCConfiguration()
        {
            return File.ReadAllText(Path.Combine(Path.Combine(GetBinDirectory(), "DevTeam.TestAdapter"), "DevTeam.TestEngine.dll.ioc"));
        }

        private static string GetBinDirectory()
        {
#if NET35
            return AppDomain.CurrentDomain.BaseDirectory;
#else
            return Path.GetDirectoryName(typeof(TestAdapter).GetTypeInfo().Assembly.Location);
#endif
        }
    }
}