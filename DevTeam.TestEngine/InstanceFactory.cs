namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Dto;

    internal class InstanceFactory : IInstanceFactory
    {
        public bool TryCreateInstance(ITestInfo testInfo, ICollection<IMessage> messages, out object instance)
        {
            if (testInfo == null) throw new ArgumentNullException(nameof(testInfo));
            if (messages == null) throw new ArgumentNullException(nameof(messages));
            try
            {
                var instanceType = testInfo.Type;
                if (testInfo.Type.IsGenericTypeDefinition)
                {
                    if (testInfo.GenericArgs.Length == instanceType.GenericTypeParameters.Length)
                    {
                        messages.Add(new MessageDto(MessageType.Trace, Stage.Construction, $"Make generic type {instanceType.FullName} with generic arguments {string.Join(", ", testInfo.GenericArgs.Select(i => i.FullName).ToArray())}"));
                        instanceType = instanceType.MakeGenericType(testInfo.GenericArgs);
                    }
                }

                messages.Add(new MessageDto(MessageType.Trace, Stage.Construction, $"Create instance of type {instanceType.FullName}"));
                instance = instanceType.CreateInstance(testInfo.TypeParameters);
                messages.Add(new MessageDto(MessageType.Trace, Stage.Construction, $"Instance #{instance.GetHashCode()} of type {instanceType.FullName} was created"));
                return true;
            }
            catch (Exception exception)
            {
                messages.Add(new MessageDto(MessageType.Exception, Stage.Construction, exception.Message, exception.StackTrace));
                instance = default(object);
                return false;
            }
        }
    }
}
