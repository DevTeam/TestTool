namespace DevTeam.TestEngine.Contracts
{
    using System.Reflection;

    public interface IReflection
    {
        [NotNull] Assembly LoadAssembly([NotNull] string source);
    }
}
