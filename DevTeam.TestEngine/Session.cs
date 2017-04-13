namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    internal class Session: ISession
    {
        [NotNull] private readonly IDiscoverer _discoverer;
        [NotNull] private readonly IRunner _runner;
        [NotNull] private readonly Dictionary<Guid, ITestInfo> _cases = new Dictionary<Guid, ITestInfo>();

        public Session(
            [NotNull] IDiscoverer discoverer,
            [NotNull] IRunner runner)
        {
            if (discoverer == null) throw new ArgumentNullException(nameof(discoverer));
            if (runner == null) throw new ArgumentNullException(nameof(runner));
            _discoverer = discoverer;
            _runner = runner;
        }

        public IEnumerable<ICase> Discover(string source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            lock (_cases)
            {
                _cases.Clear();
            }

            foreach (var testInfo in _discoverer.Discover(source))
            {
                lock (_cases)
                {
                    _cases[testInfo.Case.Id] = testInfo;
                }

                yield return testInfo.Case;
            }
        }

        public IResult Run(Guid testId)
        {
            ITestInfo testInfo;
            lock (_cases)
            {
                if (!_cases.TryGetValue(testId, out testInfo))
                {
                    return new Result(State.NotFound);
                }

                _cases.Remove(testId);
            }

            return _runner.Run(testInfo);
        }
    }
}
