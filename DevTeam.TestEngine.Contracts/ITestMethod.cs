namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;

    public interface ITestMethod : ITestElement
    {
        ITestClass Class { [NotNull] get; }

        IEnumerable<ITestCase> Cases { [NotNull] get; }
    }
}