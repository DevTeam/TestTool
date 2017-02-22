namespace DevTeam.TestEngine.Contracts
{
    public interface ITestType
    {
        string FullyQualifiedTypeName { [NotNull] get; }
    }
}
