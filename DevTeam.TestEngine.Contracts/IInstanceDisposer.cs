namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;

    public interface IInstanceDisposer
    {
        bool TryDispose([NotNull] object instance, [NotNull] ICollection<IMessage> messages);
    }
}