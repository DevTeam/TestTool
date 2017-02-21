namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;

    public interface ITestAssembly : ITestElement
    {
        string Source { [CanBeNull] get; }

        IEnumerable<ITestClass> Classes { [NotNull] get; }
    }
}