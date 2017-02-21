namespace DevTeam.TestEngine.Contracts
{
    using System;

    public interface ITestElement
    {
        Guid Id { get; }

        string FullyQualifiedName { [NotNull] get; }

        string DisplayName { [NotNull] get; }
    }
}
