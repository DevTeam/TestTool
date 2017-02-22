namespace DevTeam.TestEngine.Contracts
{
    public interface ITestCase : ITestElement
    {
        ITestMethod Method { [NotNull] get; }
    }
}