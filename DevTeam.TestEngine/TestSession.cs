namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Contracts.Reflection;
    using Dto;
    using TestFramework;

    internal class TestSession: ITestSession
    {
        [NotNull] private readonly IReflection _reflection;
        [NotNull] private readonly Dictionary<Guid, TestCaseInfo> _tests = new Dictionary<Guid, TestCaseInfo>();

        public TestSession([NotNull] IReflection reflection)
        {
            if (reflection == null) throw new ArgumentNullException(nameof(reflection));
            _reflection = reflection;
        }

        public IEnumerable<ITestCase> Discover(string source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            _tests.Clear();
            var assembly = _reflection.LoadAssembly(source);
            return
                from type in assembly.DefinedTypes
                from typeAttribute in type.GetCustomAttributes<TestAttribute>().DefaultIfEmpty(TestAttribute.Empty)
                from method in type.Methods
                from methodAttribute in method.GetCustomAttributes<TestAttribute>()
                from testCase in CreateTestCase(source, assembly, type, typeAttribute, method, methodAttribute)
                select testCase;
        }

        public ITestResult Run(Guid testId)
        {
            if (!_tests.TryGetValue(testId, out TestCaseInfo testInfo))
            {
                return new TestResult(TestState.NotFound);
            }

            var testInstance = testInfo.Type.CreateInstance(testInfo.TypeAttribute.Parameters);
            try
            {
                testInfo.Method.Invoke(testInstance, testInfo.MethodAttribute.Parameters);
                return new TestResult(TestState.Passed);
            }
            catch (Exception exception)
            {
                return new TestResult(TestState.Failed, exception);
            }
        }

        private IEnumerable<ITestCase> CreateTestCase(
            [NotNull] string source,
            [NotNull] IAssemblyInfo assembly,
            [NotNull] ITypeInfo type,
            [NotNull] TestAttribute typeAttribute,
            [NotNull] IMethodInfo method,
            [NotNull] TestAttribute methodAttribute)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (typeAttribute == null) throw new ArgumentNullException(nameof(typeAttribute));
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (methodAttribute == null) throw new ArgumentNullException(nameof(methodAttribute));
            var testCase = new TestCase(
                Guid.NewGuid(),
                source,
                type.FullName,
                type.Name,
                typeAttribute.Parameters.Select(i => i.ToString()).ToArray(),
                method.Name,
                methodAttribute.Parameters.Select(i => i.ToString()).ToArray());

            _tests[testCase.Id] = new TestCaseInfo(assembly, type, typeAttribute, method, methodAttribute);
            yield return testCase;
        }
    }
}
