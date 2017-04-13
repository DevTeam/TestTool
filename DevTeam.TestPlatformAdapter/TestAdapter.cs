﻿namespace DevTeam.TestPlatformAdapter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using IoC;
    using IoC.Configurations.Json;
    using IoC.Contracts;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
    using TestEngine.Contracts;
    using TestCase = Microsoft.VisualStudio.TestPlatform.ObjectModel.TestCase;

    public class TestAdapter : ITestDiscoverer, ITestExecutor
    {
        private readonly Uri _executorUri;
        private readonly ISession _session;
        private bool _canceled;

        public TestAdapter([IoC.Contracts.NotNull] string iocConfigFile, [IoC.Contracts.NotNull] Uri executorUri)
        {
            if (iocConfigFile == null) throw new ArgumentNullException(nameof(iocConfigFile));
            if (executorUri == null) throw new ArgumentNullException(nameof(executorUri));
            _executorUri = executorUri;
            var container = new Container("root").Configure().DependsOn<JsonConfiguration>(File.ReadAllText(iocConfigFile)).ToSelf();
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

                var stateMessages = new List<string>
                {
                    $"{testResult.Duration.TotalMilliseconds:0} ms"
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
                        case MessageType.State:
                            stateMessages.Add(message.Text);
                            break;

                        case MessageType.StdOutput:
                            testResult.Messages.Add(new TestResultMessage(TestResultMessage.StandardOutCategory, message.Text));
                            // frameworkHandle.SendMessage(TestMessageLevel.Informational, message.Text);
                            break;

                        case MessageType.StdError:
                            testResult.Messages.Add(new TestResultMessage(TestResultMessage.StandardErrorCategory, message.Text));
                            // frameworkHandle.SendMessage(TestMessageLevel.Error, message.Text);
                            break;

                        case MessageType.Exception:
                            errorMessage.AppendLine(message.Text);
                            // frameworkHandle.SendMessage(TestMessageLevel.Error, message.Text);
                            break;

                        case MessageType.Trace:
                            testResult.Messages.Add(new TestResultMessage(TestResultMessage.DebugTraceCategory, message.Text));
                            // frameworkHandle.SendMessage(TestMessageLevel.Informational, message.Text);
                            break;

                        default:
                            testResult.Messages.Add(new TestResultMessage(TestResultMessage.AdditionalInfoCategory, message.Text));
                            // frameworkHandle.SendMessage(TestMessageLevel.Informational, message.Text);
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
                var stateMessage = string.Join(", ", stateMessages.ToArray());
                var informationalMessage = $"{testCase.DisplayName} - {testResult.Outcome} ({stateMessage})";
                frameworkHandle.SendMessage(TestMessageLevel.Informational, informationalMessage);
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
                let testName = testCase.ToString()
                select new TestCase(testCase.ToString(), _executorUri, testCase.Source)
                {
                    Id = testCase.Id,
                    DisplayName = testName,
                    CodeFilePath = testCase.CodeFilePath,
                    FullyQualifiedName = testName,
                    LineNumber = testCase.LineNumber ?? 0,
                };
        }
    }
}