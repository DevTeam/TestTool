namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    internal class Runner : IRunner
    {
        [NotNull] private readonly IInstanceFactory _instanceFactory;
        [NotNull] private readonly IMethodRunner _methodRunner;
        [NotNull] private readonly IInstanceDisposer _instanceDisposer;

        public Runner(
            [NotNull] IInstanceFactory instanceFactory,
            [NotNull] IMethodRunner methodRunner,
            [NotNull] IInstanceDisposer instanceDisposer)
        {
            if (instanceFactory == null) throw new ArgumentNullException(nameof(instanceFactory));
            if (methodRunner == null) throw new ArgumentNullException(nameof(methodRunner));
            if (instanceDisposer == null) throw new ArgumentNullException(nameof(instanceDisposer));
            _instanceFactory = instanceFactory;
            _methodRunner = methodRunner;
            _instanceDisposer = instanceDisposer;
        }

        public IResult Run(ITestInfo testInfo)
        {
            if (testInfo == null) throw new ArgumentNullException(nameof(testInfo));
            var messages = new List<IMessage>();

            if (testInfo.Ignore)
            {
                messages.Add(new Message(MessageType.State, Stage.Test, testInfo.IgnoreReason));
                return new Result(State.Skiped) { messages };
            }

            if (!_instanceFactory.TryCreateInstance(testInfo, messages, out object testInstance))
            {
                return new Result(State.Failed) { messages };
            }

            if (!_methodRunner.TryRun(testInfo, testInstance, messages))
            {
                return new Result(State.Failed) { messages };
            }

            if (!_instanceDisposer.TryDispose(testInstance, messages))
            {
                return new Result(State.Failed) { messages };
            }

            return new Result(State.Passed) { messages };
        }
    }
}
