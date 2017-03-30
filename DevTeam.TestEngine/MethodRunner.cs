namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Dto;

    internal class MethodRunner : IMethodRunner
    {
        public bool TryRun(ITestInfo testInfo, object instance, ICollection<IMessage> messages)
        {
            if (testInfo == null) throw new ArgumentNullException(nameof(testInfo));
            try
            {
                messages.Add(new MessageDto(MessageType.Trace, Stage.Construction, $"Run method for instance #{instance.GetHashCode()}"));
                var method = testInfo.Method;
                method.Invoke(instance, testInfo.MethodParameters);
                messages.Add(new MessageDto(MessageType.Trace, Stage.Construction, $"Method for instance #{instance.GetHashCode()} was finished"));
            }
            catch (Exception exception)
            {
                messages.Add(new MessageDto(MessageType.Exception, Stage.Test, exception.Message, exception.StackTrace));
                return false;
            }

            return true;
        }
    }
}
