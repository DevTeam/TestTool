namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;

    public interface IMethodRunner
    {
        bool TryRun([NotNull] ITestInfo testInfo, [NotNull] object instance, [NotNull] ICollection<IMessage> messages);
    }
}