namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Contracts;
    using TestFramework;

    public class TestExplorer : ITestExplorer
    {
        private readonly IReflection _reflection;

        public TestExplorer([NotNull] IReflection reflection)
        {
            if (reflection == null) throw new ArgumentNullException(nameof(reflection));
            _reflection = reflection;
        }

        public ITestAssembly Explore(string source)
        {
            if (string.IsNullOrWhiteSpace(source)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(source));
            var assembly = _reflection.LoadAssembly(source);
            var testAssembly = new TestAssembly(Guid.NewGuid(), assembly.FullName, assembly.GetName().Name, source, GetClasses(assembly).ToArray());
            foreach (var testClass in testAssembly.Classes.Cast<TestClass>())
            {
                testClass.Assembly = testAssembly;
                foreach (var testMethod in testClass.Methods.Cast<TestMethod>())
                {
                    testMethod.Class = testClass;
                    foreach (var testCase in testMethod.Cases.Cast<TestCase>())
                    {
                        testCase.Method = testMethod;
                    }
                }
            }

            return testAssembly;
        }

        private IEnumerable<TestClass> GetClasses([NotNull] Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            return
                from type in assembly.DefinedTypes
                select new TestClass(Guid.NewGuid(), type.FullName, type.Name, GetMethods(type).ToArray());
        }

        private IEnumerable<TestMethod> GetMethods([NotNull] TypeInfo type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return
                from method in type.DeclaredMethods
                select new TestMethod(Guid.NewGuid(), method.ToString(), method.Name, GetCases(method).ToArray());
        }

        private IEnumerable<TestCase> GetCases([NotNull] MethodInfo method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            var testAttributes = method.GetCustomAttributes<TestAttribute>();
            if (testAttributes.Any())
            {
                yield return new TestCase(Guid.NewGuid(), "", "");
            }
        }
    }
}
