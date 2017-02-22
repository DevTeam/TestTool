namespace DevTeam.TestEngine.Contracts
{
    public interface ITestCaseResult
    {
        ITestCase Case { [NotNull] get; }
    }
}
