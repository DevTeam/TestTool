namespace DevTeam.TestEngine.Contracts
{
    using System;

    public interface IMessage
    {
        MessageType Type { get; }

        Stage Stage { get; }

        DateTimeOffset Time { get; }

        string Text { [NotNull] get; }

        string StackTrace { [CanBeNull] get; }
    }
}
