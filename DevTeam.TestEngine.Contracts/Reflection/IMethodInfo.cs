namespace DevTeam.TestEngine.Contracts.Reflection
{
    using System;
    using System.Collections.Generic;

    public interface IMethodInfo
    {
        string Name { [NotNull] get; }

        IEnumerable<IParameterInfo> Parameters { [NotNull] get; }

        bool IsGenericMethodDefinition { get; }

        IEnumerable<ITypeInfo> GetGenericArguments { get; }

        [NotNull]
        IMethodInfo MakeGenericMethod([NotNull] IEnumerable<Type> typeArguments);

        [NotNull]
        IEnumerable<T> GetCustomAttributes<T>() where T : Attribute;

        [CanBeNull]
        object Invoke([NotNull] object obj, [NotNull] IEnumerable<object> parameters);
    }
}
