namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;

    public interface ITestClass
    {
        string FullyQualifiedTypeName { [NotNull] get; }

        string DisplayName { [NotNull] get; }

        ITestAssembly Assembly { [NotNull] get; }

        IEnumerable<ITestMethod> Methods { [NotNull] get; }

        void AddMethod([NotNull] ITestMethod testMethod);
    }
}