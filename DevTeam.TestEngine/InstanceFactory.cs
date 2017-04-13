namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;

    internal class InstanceFactory : IInstanceFactory
    {
        public bool TryCreateInstance(ITestInfo testInfo, ICollection<IMessage> messages, out object instance)
        {
            if (testInfo == null) throw new ArgumentNullException(nameof(testInfo));
            if (messages == null) throw new ArgumentNullException(nameof(messages));
            try
            {
                var typeParameters = testInfo.TypeArgs.ToArray();
                var typeParametersStr = string.Join(", ", typeParameters.Select(i => i?.ToString() ?? "null").DefaultIfEmpty("empty").ToArray());
                messages.Add(new Message(MessageType.Trace, Stage.Construction, $"Create instance of type {testInfo.Type.FullName} with parameters [{typeParametersStr}]"));
                instance = testInfo.Type.CreateInstance(typeParameters);
                messages.Add(new Message(MessageType.Trace, Stage.Construction, $"Instance #{instance.GetHashCode()} of type {testInfo.Type.FullName} was created"));
                return true;
            }
            catch (Exception exception)
            {
                messages.Add(new Message(MessageType.Exception, Stage.Construction, exception.Message, exception.StackTrace));
                instance = default(object);
                return false;
            }
        }
    }
}
