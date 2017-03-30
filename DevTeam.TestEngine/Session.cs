namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Dto;

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
            _cases.Clear();
            foreach (var testInfo in _discoverer.Discover(source))
            {
                _cases[testInfo.TestCase.Id] = testInfo;
                yield return testInfo.TestCase;
            }
        }

        public IResult Run(Guid testId)
        {
            if (!_cases.TryGetValue(testId, out ITestInfo testInfo))
            {
                return new ResultDto(State.NotFound);
            }

            return _runner.Run(testInfo);
        }
    }
}
