namespace DevTeam.TestEngine.Contracts
{
    public interface ITestType
    {
        string FullyQualifiedName { [NotNull] get; }
    }
}
