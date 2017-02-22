namespace DevTeam.TestEngine.Contracts
{
    using Reflection;

    public interface ITestExecutionContext
    {
        IReflection Reflection { [NotNull] get; }

        [NotNull]
        IAssemblyInfo InitializeAssembly([NotNull] ITestAssembly testAssembly);

        void UninitializeAssembly([NotNull] ITestAssembly testAssembly, [NotNull] IAssemblyInfo assemblyInfo);

        [NotNull]
        ITypeInfo InitializeType([NotNull] ITestClass testClass, IAssemblyInfo assemblyInfo);

        void UninitializeType([NotNull] ITestClass testClass, [NotNull] ITypeInfo typeInfo);

        [NotNull]
        IMethodInfo InitializeMethod([NotNull] ITestMethod testMethod, [NotNull] ITypeInfo typeInfo);

        void UninitializeMethod([NotNull] ITestMethod testMethod, [NotNull] IMethodInfo methodInfo);

        void InitializeCase([NotNull] ITestCase testCase);

        void UninitializeCase([NotNull] ITestCase testCase);

        [NotNull]
        object InitializeInstance([NotNull] ITestClass testClass, [NotNull] ITypeInfo typeInfo);

        void UninitializeInstance([NotNull] ITestClass testClass, [NotNull] ITypeInfo typeInfo, [NotNull] object instance);

        [NotNull]
        ITestCaseResult ExecuteTest([NotNull] ITestCase testCase, [NotNull] IMethodInfo methodInfo, [NotNull] object instance);
    }
}
