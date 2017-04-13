namespace DevTeam.TestEngine.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface ICase
    {
        Guid Id { get; }

        string Source { [NotNull] get; }

        string FullTypeName { [NotNull] get; }

        string TypeName { [NotNull] get; }

        IEnumerable<string> TypeGenericArgs { [NotNull] get; }

        IEnumerable<string> TypeParameters { [NotNull] get; }

        string MethodName { [NotNull] get; }

        IEnumerable<string> MethodParaeters { [NotNull] get; }

        string CodeFilePath { [CanBeNull] get; }

        int? LineNumber { [CanBeNull] get; }
    }
}