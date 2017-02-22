namespace DevTeam.TestEngine
{
    using Contracts;
    using Contracts.Reflection;
    using Dto;

    internal interface ITestElementFactory
    {
        [NotNull]
        TestAssembly CreateTestAssembly([NotNull] string source, [NotNull] IAssemblyInfo assemblyInfo);

        [NotNull]
        TestClass CreateTestClass([NotNull] ITestAssembly testAssembly, [NotNull] ITypeInfo typeInfo);

        [NotNull]
        TestMethod CreateTestMethod([NotNull] ITestClass testClass, [NotNull] IMethodInfo methodInfo);

        [NotNull]
        TestCase CreateTestCase([NotNull] ITestMethod testMethod);
    }
}