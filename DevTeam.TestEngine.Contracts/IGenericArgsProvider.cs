namespace DevTeam.TestEngine.Contracts
{
    using System;
    using System.Collections.Generic;
    using Reflection;

    public interface IGenericArgsProvider
    {
        [NotNull] IEnumerable<Type[]> GetGenericArgs([NotNull] ITypeInfo type);
    }
}
