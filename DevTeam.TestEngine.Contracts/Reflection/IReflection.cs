namespace DevTeam.TestEngine.Contracts.Reflection
{
    public interface IReflection
    {
        [NotNull] IAssemblyInfo LoadAssembly([NotNull] string source);
    }
}
