namespace DevTeam.TestEngine
{
    using System;
    using Contracts;
    using Contracts.Reflection;
    using Dto;

    internal class TestExecutionContext : ITestExecutionContext
    {
        [NotNull] private readonly ITestMethodSelector _testMethodSelector;

        public TestExecutionContext(
            [NotNull] IReflection reflection,
            [NotNull] ITestMethodSelector testMethodSelector)
        {
            if (reflection == null) throw new ArgumentNullException(nameof(reflection));
            if (testMethodSelector == null) throw new ArgumentNullException(nameof(testMethodSelector));
            Reflection = reflection;
            _testMethodSelector = testMethodSelector;
        }

        public IReflection Reflection { get; }

        public IAssemblyInfo InitializeAssembly(ITestAssembly testAssembly)
        {
            if (testAssembly == null) throw new ArgumentNullException(nameof(testAssembly));
            var result =  Reflection.LoadAssembly(testAssembly.Source);
            return result;
        }

        public void UninitializeAssembly(ITestAssembly testAssembly, IAssemblyInfo assemblyInfo)
        {
            if (testAssembly == null) throw new ArgumentNullException(nameof(testAssembly));
            if (assemblyInfo == null) throw new ArgumentNullException(nameof(assemblyInfo));
        }

        public ITypeInfo InitializeType(ITestClass testClass, [NotNull] IAssemblyInfo assemblyInfo)
        {
            if (testClass == null) throw new ArgumentNullException(nameof(testClass));
            if (assemblyInfo == null) throw new ArgumentNullException(nameof(assemblyInfo));
            var result = assemblyInfo.GetType(testClass.FullyQualifiedTypeName);
            return result;
        }

        public void UninitializeType(ITestClass testClass, ITypeInfo typeInfo)
        {
            if (testClass == null) throw new ArgumentNullException(nameof(testClass));
            if (typeInfo == null) throw new ArgumentNullException(nameof(typeInfo));
        }

        public IMethodInfo InitializeMethod(ITestMethod testMethod, ITypeInfo typeInfo)
        {
            if (testMethod == null) throw new ArgumentNullException(nameof(testMethod));
            if (typeInfo == null) throw new ArgumentNullException(nameof(typeInfo));
            var result = _testMethodSelector.SelectMethod(typeInfo, testMethod);
            return result;
        }

        public void UninitializeMethod(ITestMethod testMethod, IMethodInfo methodInfo)
        {
            if (testMethod == null) throw new ArgumentNullException(nameof(testMethod));
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));
        }

        public void InitializeCase(ITestCase testCase)
        {
            if (testCase == null) throw new ArgumentNullException(nameof(testCase));
        }

        public void UninitializeCase(ITestCase testCase)
        {
            if (testCase == null) throw new ArgumentNullException(nameof(testCase));
        }

        public object InitializeInstance(ITestClass testClass, ITypeInfo typeInfo)
        {
            if (testClass == null) throw new ArgumentNullException(nameof(testClass));
            if (typeInfo == null) throw new ArgumentNullException(nameof(typeInfo));
            return typeInfo.CreateInstance();
        }

        public void UninitializeInstance(ITestClass testClass, ITypeInfo typeInfo, object instance)
        {
            if (testClass == null) throw new ArgumentNullException(nameof(testClass));
            if (typeInfo == null) throw new ArgumentNullException(nameof(typeInfo));
            if (instance == null) throw new ArgumentNullException(nameof(instance));
        }

        public ITestCaseResult ExecuteTest(ITestCase testCase, IMethodInfo methodInfo, object instance)
        {
            if (testCase == null) throw new ArgumentNullException(nameof(testCase));
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            methodInfo.Invoke(instance);
            return new TestCaseResult();
        }
    }
}
