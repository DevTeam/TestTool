namespace DevTeam.TestEngine.Contracts.Reflection
{
    using System;
    using System.Collections.Generic;

    public interface IMethodInfo
    {
        string Name { [NotNull] get; }

        IEnumerable<IParameterInfo> Parameters { [NotNull] get; }

        bool IsGenericMethodDefinition { get; }

        ITypeInfo[] GetGenericArguments { get; }

        [NotNull]
        IMethodInfo MakeGenericMethod([NotNull] params Type[] typeArguments);

        [NotNull]
        IEnumerable<T> GetCustomAttributes<T>() where T : Attribute;

        [CanBeNull]
        object Invoke([NotNull] object obj, [NotNull] params object[] parameters);
    }
}
