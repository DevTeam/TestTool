namespace DevTeam.TestAdapter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
    using TestCase = Microsoft.VisualStudio.TestPlatform.ObjectModel.TestCase;
    using System.Reflection;
#if NETCOREAPP1_0
    using System.Runtime.Loader;
#endif

    [FileExtension(".dll")]
    [FileExtension(".exe")]
    [DefaultExecutorUri(ExecutorId)]
    [ExtensionUri(ExecutorId)]
    public class TestAdapter : ITestDiscoverer, ITestExecutor
    {
        public const string ExecutorId = "executor://devteam/TestRunner";
        private static readonly string[] PrivatePaths = {"DevTeam.TestAdapter", ""};
        private static readonly Uri ExecutorUri = new Uri(ExecutorId);
        private static readonly ITestDiscoverer TestDiscoverer;
        private static readonly ITestExecutor TestExecutor;

        static TestAdapter()
        {
            var testPlatformAdapterFileName = GetFileNames("DevTeam.TestPlatformAdapter.dll").First();
            var icoConfigFileName = GetFileNames("DevTeam.TestEngine.dll.ioc").First();
#if !NETCOREAPP1_0
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                var assemblyName = new AssemblyName(args.Name);
                foreach (var fileName in GetFileNames(assemblyName.Name + ".dll"))
                {
                    try
                    {
                        return Assembly.LoadFile(fileName);
                    }
                    catch
                    {
                        // ignored
                    }
                }

                return null;
            };

            var testPlatformAdapterAssembly = Assembly.LoadFile(testPlatformAdapterFileName);
            var testPlatformAdapterType = testPlatformAdapterAssembly.GetTypes().Single(i => i.Name == "TestAdapter");

#else
            AssemblyLoadContext.Default.Resolving += (ctx, assemblyName) =>
            {
                foreach (var fileName in GetFileNames(assemblyName.Name + ".dll"))
                {
                    try
                    {
                        return ctx.LoadFromAssemblyPath(fileName);
                    }
                    catch
                    {
                        // ignored
                    }
                }

                return null;
            };

            var testPlatformAdapterAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(testPlatformAdapterFileName);
            var testPlatformAdapterType = testPlatformAdapterAssembly.ExportedTypes.Single(i => i.Name == "TestAdapter");
#endif
            var adapter = Activator.CreateInstance(testPlatformAdapterType, icoConfigFileName, ExecutorUri);
            TestDiscoverer = (ITestDiscoverer) adapter;
            TestExecutor = (ITestExecutor)adapter;
        }

        public void DiscoverTests(
            IEnumerable<string> sources,
            IDiscoveryContext discoveryContext,
            IMessageLogger logger,
            ITestCaseDiscoverySink discoverySink)
        {
            TestDiscoverer.DiscoverTests(sources, discoveryContext, logger, discoverySink);
        }

        public void RunTests(IEnumerable<TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            TestExecutor.RunTests(tests, runContext, frameworkHandle);
        }

        public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            TestExecutor.RunTests(sources, runContext, frameworkHandle);
        }

        public void Cancel()
        {
            TestExecutor.Cancel();
        }

        private static string GetBinDirectory()
        {
#if NET35
            return AppDomain.CurrentDomain.BaseDirectory;
#else
            return Path.GetDirectoryName(typeof(TestAdapter).GetTypeInfo().Assembly.Location);
#endif
        }

        private static IEnumerable<string> GetFileNames(string fileName)
        {
            var bin = GetBinDirectory();
            foreach (var privatePath in PrivatePaths)
            {
                var privateBinPath = bin;
                if (privatePath != string.Empty)
                {
                    privateBinPath = Path.Combine(bin, privatePath);
                }

                var name = Path.Combine(privateBinPath, fileName);
                if (File.Exists(name))
                {
                    yield return name;
                }
            }
        }
    }
}