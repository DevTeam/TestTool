namespace DevTeam.TestEngine.Contracts.Reflection
{
    using System;
    using System.Collections.Generic;

    public interface IMethodInfo
    {
        string Name { [NotNull] get; }

        IEnumerable<IParameterInfo> Parameters { [NotNull] get; }

        [NotNull]
        IEnumerable<T> GetCustomAttributes<T>() where T : Attribute;
    }
}
