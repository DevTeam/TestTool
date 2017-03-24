namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;

    public interface IDiscoverer
    {
        [NotNull] IEnumerable<ITestInfo> Discover([NotNull] string source);
    }
}