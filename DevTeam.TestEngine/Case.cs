namespace DevTeam.TestEngine
{
    using System;
    using System.Linq;
    using Contracts;

    internal class Case: ICase
    {
        [NotNull] private readonly IDisplayNameFactory _displayNameFactory;
        [NotNull] private readonly ITestInfo _testInfo;

        public Case(
            [NotNull] IDisplayNameFactory displayNameFactory,
            [NotNull] ITestInfo testInfo)
        {
            _displayNameFactory = displayNameFactory;
            _testInfo = testInfo;
            if (displayNameFactory == null) throw new ArgumentNullException(nameof(displayNameFactory));
            if (testInfo == null) throw new ArgumentNullException(nameof(testInfo));
            Id = Guid.NewGuid();
            Source = testInfo.Source;
            CodeFilePath = string.Empty;
            LineNumber = null;

            var typeArgs = testInfo.TypeArgs.Select(arg => arg.ToString());
            var methodArgs = testInfo.MethodArgs.Select(arg => arg.ToString());
            var methodGenerics = testInfo.Method.GenericArguments.Select(type => type.FullName);
            var args = string.Join(",", typeArgs.Concat(methodArgs).Concat(methodGenerics).ToArray());
            FullyQualifiedName = $"{testInfo.Type.FullName}.{testInfo.Method.Name}({args})";
        }

        public Guid Id { get; }

        public string Source { get; }

        public string FullyQualifiedName { get; }

        public string DisplayName => _displayNameFactory.CreateDisplayName(_testInfo);

        public string CodeFilePath { get; }

        public int? LineNumber { get; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
