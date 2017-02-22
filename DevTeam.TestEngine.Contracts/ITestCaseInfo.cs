namespace DevTeam.TestEngine.Contracts
{
    public interface ITestCaseInfo
    {
        ITestCase Case { [NotNull] get; }

        TestCaseState State { get; }

        ITestCaseResult Result { [CanBeNull] get; }
    }
}
