namespace DevTeam.TestEngine.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface ICase
    {
        Guid Id { get; }

        string Source { [NotNull] get; }

        string DisplayName { [NotNull] get; }

        string CodeFilePath { [CanBeNull] get; }

        int? LineNumber { [CanBeNull] get; }
    }
}