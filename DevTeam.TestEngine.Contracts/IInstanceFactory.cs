namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;

    public interface IInstanceFactory
    {
        bool TryCreateInstance([NotNull] ITestInfo testInfo, [NotNull] ICollection<IMessage> messages, out object instance);
    }
}