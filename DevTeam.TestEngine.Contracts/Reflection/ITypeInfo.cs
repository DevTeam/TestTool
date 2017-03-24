namespace DevTeam.TestEngine.Contracts.Reflection
{
    using System;
    using System.Collections.Generic;

    public interface ITypeInfo
    {
        string FullName { [NotNull] get; }

        string Name { [NotNull] get; }

        IEnumerable<IMethodInfo> Methods { [NotNull] get; }

        [NotNull]
        IEnumerable<T> GetCustomAttributes<T>() where T : Attribute;

        object CreateInstance([NotNull] params object[] parameters);
    }
}
