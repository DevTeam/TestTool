namespace DevTeam.TestEngine.Contracts
{
    public interface IRunner
    {
        [NotNull] IResult Run([NotNull] ITestInfo testInfo);
    }
}