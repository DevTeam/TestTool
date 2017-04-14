namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;
    using Reflection;

    public interface IArgsProvider
    {
        [NotNull] IEnumerable<IEnumerable<object>> GetTypeParameters([NotNull] ITypeInfo type);

        [NotNull] IEnumerable<IEnumerable<object>> GetMethodParameters([NotNull] IMethodInfo method);
    }
}
