namespace DevTeam.TestEngine.Contracts
{
    public interface IResult
    {
        TestState State { get; }

        IMessage[] Messages { [NotNull] get; }
    }
}
