namespace DevTeam.TestEngine.Contracts
{
    using System;
    using System.Collections.Generic;
    using Reflection;

    public interface IGenericArgsProvider
    {
        [NotNull] IEnumerable<IEnumerable<Type>> GetGenericArgs([NotNull] IMemberInfo memberInfo);
    }
}
