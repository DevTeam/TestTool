namespace DevTeam.TestAdapter
{
    using System;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

    [ExtensionUri(ExtensionId)]
    [FriendlyName("DevTeam")]
    public class TestLogger : ITestLogger
    {
        public const string ExtensionId = "logger://DevTeam";

        public void Initialize(TestLoggerEvents events, string testRunDirectory)
        {
            events.TestRunMessage += OnTestRunMessage;
            events.TestResult += OnTestResult;
            events.TestRunComplete += OnTestRunComplete;
        }

        private void OnTestRunMessage(object sender, TestRunMessageEventArgs ev)
        {
            Console.WriteLine("##" + ev.Message);
        }

        private void OnTestResult(object sender, TestResultEventArgs ev)
        {
        }

        private void OnTestRunComplete(object sender, TestRunCompleteEventArgs ev)
        {
        }
    }
}
