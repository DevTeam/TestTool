namespace DevTeam.TestEngine.Dto
{
    using System;
    using Contracts;

    internal class MessageDto: IMessage
    {
        public MessageDto(
            MessageType type,
            Stage stage,
            [NotNull] string message,
            [CanBeNull] string stackTrace = null,
            [CanBeNull] DateTimeOffset? time = null)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            Time = time ?? DateTimeOffset.Now;
            Type = type;
            Stage = stage;
            Message = message;
            StackTrace = stackTrace;
        }

        public MessageType Type { get; }

        public Stage Stage { get; }

        public DateTimeOffset Time { get; }

        public string Message { get; }

        public string StackTrace { get; }

        public override string ToString()
        {
            return $"{Time} {Type} on {Stage}: \"{Message}\"" + (StackTrace != null ? $"\n{StackTrace}" : string.Empty);
        }
    }
}
