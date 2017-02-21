namespace DevTeam.TestEngine.Contracts
{
    public interface ITestExplorer
    {
        [NotNull] ITestAssembly Explore([NotNull] string source);
    }
}