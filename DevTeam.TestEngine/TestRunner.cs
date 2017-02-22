namespace DevTeam.TestEngine
{
    using System.Collections.Generic;
    using Contracts;

    internal class TestRunner : ITestRunner
    {
        public IEnumerable<ITestCaseResult> Run(ITestClass testClass)
        {
            yield break;
        }
    }
}
