namespace DevTeam.TestEngine.Dto
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Contracts;

    internal class ResultDto: IResult, IEnumerable<IMessage>
    {
        private readonly List<IMessage> _messages = new List<IMessage>();

        public ResultDto(TestState state)
        {
            State = state;
        }

        public TestState State { get; }

        public IMessage[] Messages => _messages.ToArray();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IMessage> GetEnumerator()
        {
            return _messages.GetEnumerator();
        }

        public ResultDto WithMessages([NotNull] IEnumerable<IMessage> messages)
        {
            if (messages == null) throw new ArgumentNullException(nameof(messages));
            _messages.AddRange(messages);
            return this;
        }
    }
}
