namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Contracts.Reflection;
    using Dto;
    using TestFramework;

    internal class TestExplorer : ITestExplorer
    {
        [NotNull] private readonly IReflection _reflection;
        [NotNull] private readonly ITestElementFactory _testElementFactory;

        public TestExplorer(
            [NotNull] IReflection reflection,
            [NotNull] ITestElementFactory testElementFactory)
        {
            if (reflection == null) throw new ArgumentNullException(nameof(reflection));
            if (testElementFactory == null) throw new ArgumentNullException(nameof(testElementFactory));
            _reflection = reflection;
            _testElementFactory = testElementFactory;
        }

        public IEnumerable<ITestAssembly> ExploreSources(IEnumerable<string> sources)
        {
            foreach (var source in sources)
            {
                var assembly = _reflection.LoadAssembly(source);
                var testAssembly = _testElementFactory.CreateTestAssembly(source, assembly);
                var testClasses = ExploreTestClasses(testAssembly, assembly).ToArray();
                if (testClasses.Length == 0)
                {
                    continue;
                }

                foreach (var testClass in testClasses)
                {
                    testAssembly.Add(testClass);
                }

                yield return testAssembly;
            }
        }

        private IEnumerable<ITestClass> ExploreTestClasses([NotNull] TestAssembly testAssembly, [NotNull] IAssemblyInfo assemblyInfo)
        {
            if (testAssembly == null) throw new ArgumentNullException(nameof(testAssembly));
            if (assemblyInfo == null) throw new ArgumentNullException(nameof(assemblyInfo));
            foreach (var typeInfo in assemblyInfo.DefinedTypes)
            {
                var testClass = _testElementFactory.CreateTestClass(testAssembly, typeInfo);
                var testMethods = ExploreTestMethods(testClass, typeInfo).ToArray();
                if (testMethods.Length == 0)
                {
                    continue;
                }

                foreach (var testMethod in testMethods)
                {
                    testClass.Add(testMethod);
                }

                yield return testClass;
            }
        }

        private IEnumerable<ITestMethod> ExploreTestMethods([NotNull] ITestClass testClass, [NotNull] ITypeInfo typeInfo)
        {
            foreach (var methodInfo in typeInfo.Methods)
            {
                if (!methodInfo.GetCustomAttributes<TestAttribute>().Any())
                {
                    continue;
                }

                var testMethod = _testElementFactory.CreateTestMethod(testClass, methodInfo);
                var testCases = ExploreTestCases(testMethod, methodInfo).ToArray();
                if (testCases.Length == 0)
                {
                    continue;
                }

                foreach (var testCase in testCases)
                {
                    testMethod.Add(testCase);
                }

                yield return testMethod;
            }
        }

        private IEnumerable<ITestCase> ExploreTestCases([NotNull] ITestMethod testMethod, [NotNull] IMethodInfo methodInfo)
        {
            yield return _testElementFactory.CreateTestCase(testMethod);
        }
    }
}
