namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Dto;

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

            if (!_instanceFactory.TryCreateInstance(testInfo, messages, out object testInstance))
            {
                return new ResultDto(State.Failed).WithMessages(messages);
            }

            if (!_methodRunner.TryRun(testInfo, testInstance, messages))
            {
                return new ResultDto(State.Failed).WithMessages(messages);
            }

            if (!_instanceDisposer.TryDispose(testInstance, messages))
            {
                return new ResultDto(State.Failed).WithMessages(messages);
            }

            return new ResultDto(State.Passed).WithMessages(messages);
        }
    }
}
