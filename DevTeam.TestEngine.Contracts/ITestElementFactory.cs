namespace DevTeam.TestEngine.Contracts
{
    using System;
    using System.Collections.Generic;
    using Reflection;

    public interface ITestElementFactory
    {
        [NotNull]
        ITestAssembly CreateTestAssembly([NotNull] string source, [NotNull] IAssemblyInfo assemblyInfo, [NotNull] Uri testExecutor);

        [NotNull]
        ITestClass CreateTestClass([NotNull] ITestAssembly testAssembly, [NotNull] ITypeInfo typeInfo);

        [NotNull]
        ITestMethod CreateTestMethod([NotNull] ITestClass testClass, [NotNull] IMethodInfo methodInfo);

        [NotNull]
        ITestCase CreateTestCase([NotNull] ITestMethod testMethod, [CanBeNull] Guid? id = null);

        [NotNull]
        IEnumerable<ITestAssembly> RestoreTestAssemblies([NotNull] IDictionary<Guid, string> cases);
    }
}