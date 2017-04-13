namespace DevTeam.TestEngine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Contracts;

    internal class Result: IResult, IEnumerable<IMessage>
    {
        private readonly List<IMessage> _messages = new List<IMessage>();

        public Result(State state)
        {
            State = state;
        }

        public State State { get; }

        public IMessage[] Messages => _messages.ToArray();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IMessage> GetEnumerator()
        {
            return _messages.GetEnumerator();
        }

        public void Add([NotNull] IEnumerable<IMessage> messages)
        {
            if (messages == null) throw new ArgumentNullException(nameof(messages));
            _messages.AddRange(messages);
        }
    }
}
