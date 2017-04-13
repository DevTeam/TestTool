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
                    var genericArgs = testInfo.GenericArgs.ToArray();
                    if (genericArgs.Length == instanceType.GenericTypeParameters.Count())
                    {
                        messages.Add(new MessageDto(MessageType.Trace, Stage.Construction, $"Make generic type {instanceType.FullName} with generic arguments [{string.Join(", ", genericArgs.Select(i => i.FullName).ToArray())}]"));
                        instanceType = instanceType.MakeGenericType(genericArgs);
                    }
                }

                var typeParameters = testInfo.TypeParameters.ToArray();
                var typeParametersStr = string.Join(", ", typeParameters.Select(i => i?.ToString() ?? "null").DefaultIfEmpty("empty").ToArray());
                messages.Add(new MessageDto(MessageType.Trace, Stage.Construction, $"Create instance of type {instanceType.FullName} with parameters [{typeParametersStr}]"));
                instance = instanceType.CreateInstance(typeParameters);
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
