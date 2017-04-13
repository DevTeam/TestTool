namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    internal class MethodRunner : IMethodRunner
    {
        public bool TryRun(ITestInfo testInfo, object instance, ICollection<IMessage> messages)
        {
            if (testInfo == null) throw new ArgumentNullException(nameof(testInfo));
            try
            {
                var method = testInfo.Method;
                messages.Add(new Message(MessageType.Trace, Stage.Construction, $"Run method {method.Name} for instance #{instance.GetHashCode()}"));
                method.Invoke(instance, testInfo.MethodArgs);
                messages.Add(new Message(MessageType.Trace, Stage.Construction, $"Method {method.Name} for instance #{instance.GetHashCode()} was finished"));
            }
            catch (Exception exception)
            {
                messages.Add(new Message(MessageType.Exception, Stage.Test, exception.Message, exception.StackTrace));
                return false;
            }

            return true;
        }
    }
}
