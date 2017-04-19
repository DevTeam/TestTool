namespace DevTeam.TestEngine.Contracts
{
    using System;

    public interface ICase
    {
        Guid Id { get; }

        string Source { [NotNull] get; }

        string FullyQualifiedName { [NotNull] get; }

        string DisplayName { [NotNull] get; }

        string CodeFilePath { [CanBeNull] get; }

        int? LineNumber { [CanBeNull] get; }
    }
}