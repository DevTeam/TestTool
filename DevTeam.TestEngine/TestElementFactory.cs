namespace DevTeam.TestEngine
{
    using System;
    using System.Linq;
    using Contracts;
    using Contracts.Reflection;
    using Dto;

    internal class TestElementFactory : ITestElementFactory
    {

        public TestAssembly CreateTestAssembly(string source, IAssemblyInfo assemblyInfo)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (assemblyInfo == null) throw new ArgumentNullException(nameof(assemblyInfo));
            return new TestAssembly(Guid.NewGuid(), assemblyInfo.FullName, assemblyInfo.Name, source);
        }

        public TestClass CreateTestClass(ITestAssembly testAssembly, ITypeInfo typeInfo)
        {
            if (testAssembly == null) throw new ArgumentNullException(nameof(testAssembly));
            if (typeInfo == null) throw new ArgumentNullException(nameof(typeInfo));
            return new TestClass(Guid.NewGuid(), typeInfo.FullName, typeInfo.Name, testAssembly);
        }

        public TestMethod CreateTestMethod(ITestClass testClass, IMethodInfo methodInfo)
        {
            if (testClass == null) throw new ArgumentNullException(nameof(testClass));
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));
            return new TestMethod(Guid.NewGuid(), methodInfo.ToString(), methodInfo.Name, testClass, methodInfo.Parameters.Select(i => new TestType(i.ParameterType.FullName)));
        }

        public TestCase CreateTestCase(ITestMethod testMethod)
        {
            return new TestCase(Guid.NewGuid(), string.Empty, string.Empty, testMethod);
        }
    }
}
