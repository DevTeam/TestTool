namespace DevTeam.TestEngine.Contracts
{
    public interface IResult
    {
        State State { get; }

        IMessage[] Messages { [NotNull] get; }
    }
}
