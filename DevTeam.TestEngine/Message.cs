namespace DevTeam.TestEngine
{
    using System;
    using Contracts;

    internal class Message: IMessage
    {
        public Message(
            MessageType type,
            Stage stage,
            [NotNull] string text,
            [CanBeNull] string stackTrace = null)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            Time = DateTimeOffset.Now;
            Type = type;
            Stage = stage;
            Text = text;
            StackTrace = stackTrace;
        }

        public MessageType Type { get; }

        public Stage Stage { get; }

        public DateTimeOffset Time { get; }

        public string Text { get; }

        public string StackTrace { get; }

        public override string ToString()
        {
            return $"{Time} {Type} on {Stage}: \"{Text}\"" + (StackTrace != null ? $"\n{StackTrace}" : string.Empty);
        }
    }
}
