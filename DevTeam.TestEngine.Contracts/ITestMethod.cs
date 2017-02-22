namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;

    public interface ITestMethod
    {
        string Name { [NotNull] get; }

        string DisplayName { [NotNull] get; }

        ITestClass Class { [NotNull] get; }

        IEnumerable<ITestType> Parameters { get; }

        [CanBeNull]
        string CodeFilePath { get; set; }

        int LineNumber { get; set; }

        IEnumerable<ITestCase> Cases { [NotNull] get; }

        void AddCase([NotNull] ITestCase testCase);
    }
}