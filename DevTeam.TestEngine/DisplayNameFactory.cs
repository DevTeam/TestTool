namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;

    internal class DisplayNameFactory : IDisplayNameFactory
    {
        public string CreateDisplayName([NotNull] ITestInfo testInfo)
        {
            var cs = testInfo.Case;
            return $"{cs.Source}: {cs.FullTypeName}{GetGenericArgsString(cs.TypeGenericArgs)}{GetArgString(cs.TypeArgs)}.{cs.MethodName}{GetGenericArgsString(cs.MethodGenericArgs)}{GetArgString(cs.MethodArgs)}";
        }

        [NotNull]
        private static string GetArgString([NotNull] IEnumerable<string> parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            var str = string.Join(", ", parameters.ToArray());
            return string.IsNullOrEmpty(str) ? string.Empty : $"({str})";
        }

        [NotNull]
        private static string GetGenericArgsString([NotNull] IEnumerable<string> types)
        {
            if (types == null) throw new ArgumentNullException(nameof(types));
            var str = string.Join(", ", types.ToArray());
            return string.IsNullOrEmpty(str) ? string.Empty : $"<{str}>";
        }
    }
}
