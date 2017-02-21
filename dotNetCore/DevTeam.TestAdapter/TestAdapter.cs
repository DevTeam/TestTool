namespace DevTeam.TestAdapter
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;


    [FileExtension(".dll")]
    [DefaultExecutorUri("executor://devteam/TestRunner")]
    [ExtensionUri("executor://devteam/TestRunner")]
    public class TestAdapter : ITestDiscoverer, ITestExecutor
    {
        public TestAdapter()
        {
            System.Diagnostics.Debugger.Break();
        }

        public void DiscoverTests(
            IEnumerable<string> sources,
            IDiscoveryContext discoveryContext,
            IMessageLogger logger,
            ITestCaseDiscoverySink discoverySink)
        {
            System.Diagnostics.Debugger.Break();
            discoverySink.SendTestCase(new TestCase() {DisplayName = "aaa"});
        }

        public void RunTests(IEnumerable<TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            System.Diagnostics.Debugger.Break();
            frameworkHandle.SendMessage(TestMessageLevel.Informational, "aaa");
        }

        public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            // Debugger.Break();
        }

        public void Cancel()
        {
            // Debugger.Break();
        }
    }
}