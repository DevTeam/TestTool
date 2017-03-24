namespace DevTeam.TestEngine.Contracts
{
    using System;

    public interface ITestCase
    {
        Guid Id { get; }

        string Source { [NotNull] get; }

        string FullTypeName { [NotNull] get; }

        string TypeName { [NotNull] get; }

        string[] TypeParameters { [NotNull] get; }

        string MethodName { [NotNull] get; }

        string[] MethodParaeters { [NotNull] get; }

        string CodeFilePath { [CanBeNull] get; }

        int? LineNumber { [CanBeNull] get; }
    }
}