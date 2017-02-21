namespace DevTeam.TestEngine.Contracts
{
    public interface ITestCase : ITestElement
    {
        ITestMethod Method { [NotNull] get; }

        [CanBeNull]
        string CodeFilePath { get; set; }

        int LineNumber { get; set; }
    }
}