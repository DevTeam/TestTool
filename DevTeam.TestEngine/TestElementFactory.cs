namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Contracts;
    using Contracts.Reflection;
    using Dto;

    internal class TestElementFactory : ITestElementFactory
    {
        private readonly IReflection _reflection;

        public TestElementFactory([NotNull] IReflection reflection)
        {
            if (reflection == null) throw new ArgumentNullException(nameof(reflection));
            _reflection = reflection;
        }

        public ITestAssembly CreateTestAssembly(string source, IAssemblyInfo assemblyInfo, Uri testExecutor)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (assemblyInfo == null) throw new ArgumentNullException(nameof(assemblyInfo));
            if (testExecutor == null) throw new ArgumentNullException(nameof(testExecutor));
            return new TestAssembly(assemblyInfo.FullName, assemblyInfo.Name, source, testExecutor);
        }

        public ITestClass CreateTestClass(ITestAssembly testAssembly, ITypeInfo typeInfo)
        {
            if (testAssembly == null) throw new ArgumentNullException(nameof(testAssembly));
            if (typeInfo == null) throw new ArgumentNullException(nameof(typeInfo));
            return new TestClass(typeInfo.FullName, typeInfo.Name, testAssembly);
        }

        public ITestMethod CreateTestMethod(ITestClass testClass, IMethodInfo methodInfo)
        {
            if (testClass == null) throw new ArgumentNullException(nameof(testClass));
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));
            return new TestMethod(Guid.NewGuid(), methodInfo.Name, methodInfo.ToString(), testClass, methodInfo.Parameters.Select(i => new TestType(i.ParameterType.FullName)));
        }

        public ITestCase CreateTestCase(ITestMethod testMethod, Guid? id = null)
        {
            var fullyQualifiedCaseName = new StringBuilder();
            var displayName = new StringBuilder();
            fullyQualifiedCaseName.AppendLine(testMethod.Class.Assembly.TestExecutor.ToString());
            fullyQualifiedCaseName.AppendLine(testMethod.Class.Assembly.Source);
            fullyQualifiedCaseName.AppendLine(testMethod.Class.FullyQualifiedTypeName);
            fullyQualifiedCaseName.AppendLine(testMethod.Name);
            displayName.Append(testMethod.Name);
            return new TestCase(id ?? Guid.NewGuid(), fullyQualifiedCaseName.ToString(), displayName.ToString(), testMethod);
        }

        public IEnumerable<ITestAssembly> RestoreTestAssemblies(IDictionary<Guid, string> cases)
        {
            if (cases == null) throw new ArgumentNullException(nameof(cases));
            var assemblies = new Dictionary<ITestAssembly, ITestAssembly>();
            var classes = new Dictionary<ITestClass, ITestClass>();
            var methods = new Dictionary<ITestMethod, ITestMethod>();

            var separators = new[] {System.Environment.NewLine};
            foreach (var caseItem in cases)
            {
                var caseNameParts= caseItem.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (caseNameParts.Length != 4)
                {
                    continue;
                }

                var testExecutor = caseNameParts[0];
                var source = caseNameParts[1];
                var typeName = caseNameParts[2];
                var methodName = caseNameParts[3];

                var assembly = _reflection.LoadAssembly(source);
                var type = assembly.GetType(typeName);
                var method = type.Methods.SingleOrDefault(i => i.Name == methodName);

                var testAssembly = CreateTestAssembly(source, _reflection.LoadAssembly(source), new Uri(testExecutor));
                ITestAssembly currentTestAssembly;
                if (!assemblies.TryGetValue(testAssembly, out currentTestAssembly))
                {
                    currentTestAssembly = testAssembly;
                    assemblies.Add(testAssembly, testAssembly);
                }

                var testClass = CreateTestClass(currentTestAssembly, type);
                ITestClass currentTestClass;
                if (!classes.TryGetValue(testClass, out currentTestClass))
                {
                    currentTestClass = testClass;
                    classes.Add(testClass, testClass);
                    currentTestAssembly.AddClass(currentTestClass);
                }

                var testMethod = CreateTestMethod(currentTestClass, method);
                ITestMethod currentTestMethod;
                if (!methods.TryGetValue(testMethod, out currentTestMethod))
                {
                    currentTestMethod = testMethod;
                    methods.Add(testMethod, currentTestMethod);
                    currentTestClass.AddMethod(currentTestMethod);
                }

                var testCase = CreateTestCase(currentTestMethod, caseItem.Key);
                currentTestMethod.AddCase(testCase);
            }

            return assemblies.Keys;
        }
    }
}
