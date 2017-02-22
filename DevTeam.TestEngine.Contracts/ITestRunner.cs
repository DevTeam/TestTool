namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;

    public interface ITestRunner
    {
        [NotNull]
        IEnumerable<ITestCaseResult> Run([NotNull] ITestClass testClass);
    }
}