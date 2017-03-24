namespace DevTeam.TestEngine.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface ITestSession
    {
        [NotNull]
        IEnumerable<ITestCase> Discover([NotNull] string source);

        [NotNull]
        ITestResult Run(Guid testId);
    }
}
