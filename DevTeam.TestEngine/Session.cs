namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    internal class Session: ISession
    {
        [NotNull] private readonly IEnumerable<IDiscoverer> _discoverers;
        [NotNull] private readonly Dictionary<Guid, ITestInfo> _tests = new Dictionary<Guid, ITestInfo>();

        public Session(
            [NotNull] IEnumerable<IDiscoverer> discoverers)
        {
            if (discoverers == null) throw new ArgumentNullException(nameof(discoverers));
            _discoverers = discoverers;
        }

        public IEnumerable<ICase> Discover(string source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            foreach (var discoverer in _discoverers)
            {
                foreach (var testInfo in discoverer.Discover(source))
                {
                    lock (_tests)
                    {
                        _tests[testInfo.Case.Id] = testInfo;
                    }

                    yield return testInfo.Case;
                }
            }
        }

        public IResult Run(Guid testId)
        {
            ITestInfo testInfo;
            lock (_tests)
            {
                if (!_tests.TryGetValue(testId, out testInfo))
                {
                    return new Result(State.NotFound);
                }
            }

            return testInfo.Runner.Run(testInfo);
        }
    }
}
