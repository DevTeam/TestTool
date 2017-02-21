namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;

    public interface ITestClass : ITestElement
    {
        ITestAssembly Assembly { [NotNull] get; }

        IEnumerable<ITestMethod> Methods { [NotNull] get; }
    }
}