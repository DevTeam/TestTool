namespace DevTeam.TestEngine.Contracts.Reflection
{
    public interface IParameterInfo
    {
        ITypeInfo ParameterType { [NotNull] get; }
    }
}
