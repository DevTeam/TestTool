namespace DevTeam.TestEngine.Contracts
{
    using System;

    public interface ICase
    {
        Guid Id { get; }

        string Source { [NotNull] get; }

        string FullTypeName { [NotNull] get; }

        string TypeName { [NotNull] get; }

        string[] TypeGenericArgs { [NotNull] get; }

        string[] TypeParameters { [NotNull] get; }

        string MethodName { [NotNull] get; }

        string[] MethodParaeters { [NotNull] get; }

        string CodeFilePath { [CanBeNull] get; }

        int? LineNumber { [CanBeNull] get; }
    }
}