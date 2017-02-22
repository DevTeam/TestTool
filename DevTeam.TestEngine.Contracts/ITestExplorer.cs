namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;

    public interface ITestExplorer
    {
        [NotNull]
        IEnumerable<ITestAssembly> ExploreSources([NotNull] IEnumerable<string> sources);
    }
}