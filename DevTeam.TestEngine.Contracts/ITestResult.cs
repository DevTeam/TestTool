namespace DevTeam.TestEngine.Contracts
{
    public interface ITestResult
    {
        TestState State { get; }

        string Error { [CanBeNull] get; }

        string StackTrace { [CanBeNull] get; }
    }
}
