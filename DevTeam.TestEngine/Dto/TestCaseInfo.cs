namespace DevTeam.TestEngine.Dto
{
    using System;
    using Contracts;

    internal class TestCaseInfo : ITestCaseInfo
    {
        public TestCaseInfo(
            [NotNull] ITestCase testCasecase,
            TestCaseState state)
        {
            if (testCasecase == null) throw new ArgumentNullException(nameof(testCasecase));
            Case = testCasecase;
            State = state;
        }

        public ITestCase Case { get; }

        public TestCaseState State { get; }

        public ITestCaseResult Result { get; set; }
    }
}
