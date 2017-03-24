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
        [NotNull] private readonly Dictionary<Guid, TestInfo> _cases = new Dictionary<Guid, TestInfo>();

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
                from typeGenericArgsAttribute in type.GetCustomAttributes<Test.GenericArgsAttribute>().DefaultIfEmpty(Test.GenericArgsAttribute.Empty)
                from typeCaseAttribute in type.GetCustomAttributes<Test.CaseAttribute>().DefaultIfEmpty(Test.CaseAttribute.Empty)
                from method in type.Methods
                from methodCaseAttribute in method.GetCustomAttributes<Test.CaseAttribute>()
                from testCase in CreateCase(source, assembly, type, typeGenericArgsAttribute, typeCaseAttribute, method, methodCaseAttribute)
                select testCase;
        }

        public IResult Run(Guid testId)
        {
            if (!_cases.TryGetValue(testId, out TestInfo testInfo))
            {
                return new ResultDto(TestState.NotFound);
            }

            var messages = new List<IMessage>();
            object testInstance;
            try
            {
                var instanceType = testInfo.Type;
                if (testInfo.Type.IsGenericTypeDefinition)
                {
                    if (testInfo.TypeGenericArgsAttribute.Types.Length == instanceType.GenericTypeParameters.Length)
                    {
                        instanceType = instanceType.MakeGenericType(testInfo.TypeGenericArgsAttribute.Types);
                    }
                }

                testInstance = instanceType.CreateInstance(testInfo.TypeCaseAttribute.Parameters);
            }
            catch (Exception exception)
            {
                messages.Add(CreateMessageForException(exception, Stage.Construction));
                return new ResultDto(TestState.Failed).WithMessages(messages);
            }

            try
            {
                var method = testInfo.Method;
                method.Invoke(testInstance, testInfo.MethodCaseAttribute.Parameters);
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
            [NotNull] Test.GenericArgsAttribute typeGenericArgAttribute,
            [NotNull] Test.CaseAttribute typeCaseAttribute,
            [NotNull] IMethodInfo method,
            [NotNull] Test.CaseAttribute methodCaseAttribute)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (typeGenericArgAttribute == null) throw new ArgumentNullException(nameof(typeGenericArgAttribute));
            if (typeCaseAttribute == null) throw new ArgumentNullException(nameof(typeCaseAttribute));
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (methodCaseAttribute == null) throw new ArgumentNullException(nameof(methodCaseAttribute));
            var testCase = new CaseDto(
                Guid.NewGuid(),
                source,
                type.FullName,
                type.Name,
                typeGenericArgAttribute.Types.Select(i => i.Name).ToArray(),
                typeCaseAttribute.Parameters.Select(i => i.ToString()).ToArray(),
                method.Name,
                methodCaseAttribute.Parameters.Select(i => i.ToString()).ToArray());

            _cases[testCase.Id] = new TestInfo(assembly, type, typeGenericArgAttribute, typeCaseAttribute, method, methodCaseAttribute);
            yield return testCase;
        }
    }
}
