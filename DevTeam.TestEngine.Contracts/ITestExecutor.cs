namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;

    public interface ITestExecutor
    {
        [NotNull]
        IEnumerable<ITestCaseInfo> Run([NotNull] IEnumerable<ITestAssembly> testAssemblies);
    }
}