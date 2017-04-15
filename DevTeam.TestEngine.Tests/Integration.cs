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
                .DependsOn<JsonConfiguration>(ReadIoCConfiguration()).ToSelf();
        }

        public static string GetSource()
        {
            return Path.Combine(TestsExtensions.GetBinDirectory(), "DevTeam.TestEngine.Tests.dll");
        }

        public static IEnumerable<ICase> Filter([IoC.Contracts.NotNull] this IEnumerable<ICase> testCases, [IoC.Contracts.NotNull] params Type[] testTypes)
        {
            if (testCases == null) throw new ArgumentNullException(nameof(testCases));
            if (testTypes == null) throw new ArgumentNullException(nameof(testTypes));
            var typeNames = new HashSet<string>(testTypes.Select(type => new string(type.Name.TakeWhile(i => i != '`').ToArray())));
            return testCases.Where(i => i.DisplayName.Contains(".Sandbox.") && typeNames.Any(typeName => i.DisplayName.Contains(typeName)));
        }

        public static IResult[] RunAll([IoC.Contracts.NotNull] this ISession session, [IoC.Contracts.NotNull] IEnumerable<ICase> testCases)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (testCases == null) throw new ArgumentNullException(nameof(testCases));
            return testCases.Select(testCase => session.Run(testCase.Id)).ToArray();
        }

        private static string ReadIoCConfiguration()
        {
            return File.ReadAllText(Path.Combine(TestsExtensions.GetBinDirectory(), "DevTeam.TestEngine.dll.ioc"));
        }
    }
}
