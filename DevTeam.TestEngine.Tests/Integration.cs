namespace DevTeam.TestEngine.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Contracts;
    using IoC;
    using IoC.Configurations.Json;
    using IoC.Contracts;

    internal static class Integration
    {
        public static IContainer CreateContainer()
        {
            return new Container("root").Configure()
                .DependsOn<JsonConfiguration>(ReadIoCConfiguration()).ToSelf()
                .Register().Autowiring<Contracts.Reflection.IReflection, Reflection>().ToSelf();
        }

        public static string GetSource()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DevTeam.TestEngine.Tests.dll");
        }

        public static IEnumerable<ITestCase> Filter([IoC.Contracts.NotNull] this IEnumerable<ITestCase> testCases, [IoC.Contracts.NotNull] params Type[] testTypes)
        {
            if (testCases == null) throw new ArgumentNullException(nameof(testCases));
            if (testTypes == null) throw new ArgumentNullException(nameof(testTypes));
            var typeNames = new HashSet<string>(testTypes.Select(type => type.Name));
            return testCases.Where(i => i.FullTypeName.Contains(".Sandbox.") && (!testTypes.Any() || typeNames.Contains(i.TypeName)));
        }

        public static ITestResult[] RunAll([IoC.Contracts.NotNull] this ITestSession session, [IoC.Contracts.NotNull] IEnumerable<ITestCase> testCases)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (testCases == null) throw new ArgumentNullException(nameof(testCases));
            return testCases.Select(testCase => session.Run(testCase.Id)).ToArray();
        }

        private static string ReadIoCConfiguration()
        {
            return File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DevTeam.TestEngine.ioc"));
        }
    }
}
