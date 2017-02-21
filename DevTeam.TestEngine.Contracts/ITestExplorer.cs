namespace DevTeam.TestEngine.Contracts
{
    public interface ITestExplorer
    {
        [NotNull] TestAssembly Explore([NotNull] string source);
    }
}