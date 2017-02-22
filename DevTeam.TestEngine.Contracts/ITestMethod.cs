namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;

    public interface ITestMethod : ITestElement
    {
        ITestClass Class { [NotNull] get; }

        IEnumerable<ITestType> Parameters { get; }

        [CanBeNull]
        string CodeFilePath { get; set; }

        int LineNumber { get; set; }

        IEnumerable<ITestCase> Cases { [NotNull] get; }
    }
}