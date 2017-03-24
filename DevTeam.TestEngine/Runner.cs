namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Dto;

    internal class Runner : IRunner
    {
        public IResult Run([NotNull] ITestInfo testInfo)
        {
            if (testInfo == null) throw new ArgumentNullException(nameof(testInfo));
            var messages = new List<IMessage>();
            object testInstance;
            try
            {
                var instanceType = testInfo.Type;
                if (testInfo.Type.IsGenericTypeDefinition)
                {
                    if (testInfo.GenericArgs.Length == instanceType.GenericTypeParameters.Length)
                    {
                        instanceType = instanceType.MakeGenericType(testInfo.GenericArgs);
                    }
                }

                testInstance = instanceType.CreateInstance(testInfo.TypeParameters);
            }
            catch (Exception exception)
            {
                messages.Add(CreateMessageForException(exception, Stage.Construction));
                return new ResultDto(State.Failed).WithMessages(messages);
            }

            try
            {
                var method = testInfo.Method;
                method.Invoke(testInstance, testInfo.MethodParameters);
            }
            catch (Exception exception)
            {
                messages.Add(CreateMessageForException(exception, Stage.Test));
                return new ResultDto(State.Failed).WithMessages(messages);
            }

            try
            {
                (testInstance as IDisposable)?.Dispose();
            }
            catch (Exception exception)
            {
                messages.Add(CreateMessageForException(exception, Stage.Dispose));
                return new ResultDto(State.Failed).WithMessages(messages);
            }

            return new ResultDto(State.Passed).WithMessages(messages);
        }

        private static IMessage CreateMessageForException(Exception exception, Stage stage)
        {
            return new MessageDto(MessageType.Exception, stage, exception.Message, exception.StackTrace);
        }
    }
}
