namespace DevTeam.TestEngine.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface ITestAssembly
    {
        string FullyQualifiedAssemblyName { [NotNull] get; }

        string DisplayName { [NotNull] get; }

        string Source { [CanBeNull] get; }

        Uri TestExecutor { [CanBeNull] get; }

        IEnumerable<ITestClass> Classes { [NotNull] get; }

        void AddClass([NotNull] ITestClass testClass);
    }
}