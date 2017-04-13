namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    internal class InstanceDisposer : IInstanceDisposer
    {
        public bool TryDispose(object instance, ICollection<IMessage> messages)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (messages == null) throw new ArgumentNullException(nameof(messages));
            try
            {
                var disposable = instance as IDisposable;
                if (disposable != null)
                {
                    messages.Add(new Message(MessageType.Trace, Stage.Construction, $"Dispose instance #{instance.GetHashCode()}"));
                    disposable.Dispose();
                    messages.Add(new Message(MessageType.Trace, Stage.Construction, $"Instance #{instance.GetHashCode()} was disposed"));
                }
            }
            catch (Exception exception)
            {
                messages.Add(new Message(MessageType.Exception, Stage.Dispose, exception.Message, exception.StackTrace));
                return false;
            }

            return true;
        }
    }
}
