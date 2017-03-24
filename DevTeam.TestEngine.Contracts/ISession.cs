namespace DevTeam.TestEngine.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface ISession
    {
        [NotNull]
        IEnumerable<ICase> Discover([NotNull] string source);

        [NotNull]
        IResult Run(Guid testId);
    }
}
