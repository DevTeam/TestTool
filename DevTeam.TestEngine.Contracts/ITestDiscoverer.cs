namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;

    public interface ITestDiscoverer
    {
        [NotNull]
        IEnumerable<ITestAssembly> ExploreSources([NotNull] IEnumerable<string> sources);
    }
}