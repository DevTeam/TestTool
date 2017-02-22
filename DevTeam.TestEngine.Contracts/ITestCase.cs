namespace DevTeam.TestEngine.Contracts
{
    using System;

    public interface ITestCase
    {
        Guid Id { get; }

        string FullyQualifiedCaseName { [NotNull] get; }

        string DisplayName { [NotNull] get; }

        ITestMethod Method { [NotNull] get; }
    }
}