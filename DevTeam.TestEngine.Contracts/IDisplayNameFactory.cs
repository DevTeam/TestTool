namespace DevTeam.TestEngine.Contracts
{
    public interface IDisplayNameFactory
    {
        [NotNull] string CreateDisplayName([NotNull] ITestInfo testInfo);
    }
}