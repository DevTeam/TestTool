namespace DevTeam.TestEngine.Contracts.Reflection
{
    public interface IParameterInfo
    {
        ITypeInfo ParameterType { [NotNull] get; }

        string Name { [NotNull] get; }
    }
}
