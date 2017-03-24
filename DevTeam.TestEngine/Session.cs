namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Contracts.Reflection;
    using Dto;
    using TestFramework;

    internal class Session: ISession
    {
        [NotNull] private readonly IReflection _reflection;
        [NotNull] private readonly Dictionary<Guid, CaseInfo> _cases = new Dictionary<Guid, CaseInfo>();

        public Session([NotNull] IReflection reflection)
        {
            if (reflection == null) throw new ArgumentNullException(nameof(reflection));
            _reflection = reflection;
        }

        public IEnumerable<ICase> Discover(string source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            _cases.Clear();
            var assembly = _reflection.LoadAssembly(source);
            return
                from type in assembly.DefinedTypes
                from typeAttribute in type.GetCustomAttributes<CaseAttribute>().DefaultIfEmpty(CaseAttribute.Empty)
                from method in type.Methods
                from methodAttribute in method.GetCustomAttributes<CaseAttribute>()
                from testCase in CreateCase(source, assembly, type, typeAttribute, method, methodAttribute)
                select testCase;
        }

        public IResult Run(Guid testId)
        {
            if (!_cases.TryGetValue(testId, out CaseInfo testInfo))
            {
                return new ResultDto(TestState.NotFound);
            }

            var messages = new List<IMessage>();
            object testInstance;
            try
            {
                testInstance = testInfo.Type.CreateInstance(testInfo.TypeAttribute.Parameters);
            }
            catch (Exception exception)
            {
                messages.Add(CreateMessageForException(exception, Stage.Construction));
                return new ResultDto(TestState.Failed).WithMessages(messages);
            }

            try
            {
                testInfo.Method.Invoke(testInstance, testInfo.MethodAttribute.Parameters);
            }
            catch (Exception exception)
            {
                messages.Add(CreateMessageForException(exception, Stage.Test));
                return new ResultDto(TestState.Failed).WithMessages(messages);
            }

            try
            {
                (testInstance as IDisposable)?.Dispose();
            }
            catch (Exception exception)
            {
                messages.Add(CreateMessageForException(exception, Stage.Dispose));
                return new ResultDto(TestState.Failed).WithMessages(messages);
            }

            return new ResultDto(TestState.Passed).WithMessages(messages);
        }

        private static IMessage CreateMessageForException(Exception exception, Stage stage)
        {
            return new MessageDto(MessageType.Exception, stage, exception.Message, exception.StackTrace);
        }

        private IEnumerable<ICase> CreateCase(
            [NotNull] string source,
            [NotNull] IAssemblyInfo assembly,
            [NotNull] ITypeInfo type,
            [NotNull] CaseAttribute typeAttribute,
            [NotNull] IMethodInfo method,
            [NotNull] CaseAttribute methodAttribute)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (typeAttribute == null) throw new ArgumentNullException(nameof(typeAttribute));
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (methodAttribute == null) throw new ArgumentNullException(nameof(methodAttribute));
            var testCase = new CaseDto(
                Guid.NewGuid(),
                source,
                type.FullName,
                type.Name,
                typeAttribute.Parameters.Select(i => i.ToString()).ToArray(),
                method.Name,
                methodAttribute.Parameters.Select(i => i.ToString()).ToArray());

            _cases[testCase.Id] = new CaseInfo(assembly, type, typeAttribute, method, methodAttribute);
            yield return testCase;
        }
    }
}
