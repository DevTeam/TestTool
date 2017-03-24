namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;
    using Reflection;

    public interface IParametersProvider
    {
        [NotNull] IEnumerable<object[]> GetTypeParameters([NotNull] ITypeInfo type);

        [NotNull] IEnumerable<object[]> GetMethodParameters([NotNull] IMethodInfo method);
    }
}
