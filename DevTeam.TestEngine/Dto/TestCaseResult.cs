namespace DevTeam.TestEngine.Dto
{
    using System;
    using Contracts;

    internal class TestCaseResult : ITestCaseResult
    {
        public TestCaseResult([NotNull] ITestCase testCasecase)
        {
            if (testCasecase == null) throw new ArgumentNullException(nameof(testCasecase));
            Case = testCasecase;
        }

        public ITestCase Case { get; }
    }
}
