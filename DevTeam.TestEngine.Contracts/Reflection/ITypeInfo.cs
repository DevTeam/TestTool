namespace DevTeam.TestEngine.Contracts.Reflection
{
    using System;
    using System.Collections.Generic;

    public interface ITypeInfo
    {
        Type Type { [NotNull] get; }

        string FullName { [NotNull] get; }

        string Name { [NotNull] get; }

        bool IsGenericTypeDefinition { get; }

        IEnumerable<ITypeInfo> GenericTypeParameters { get; }

        IEnumerable<IMethodInfo> Methods { [NotNull] get; }

        IEnumerable<IPropertyInfo> Properties { [NotNull] get; }

        [NotNull]
        ITypeInfo MakeGenericType([NotNull] IEnumerable<Type> typeArguments);

        [NotNull]
        IEnumerable<T> GetCustomAttributes<T>() where T : Attribute;

        object CreateInstance([NotNull] IEnumerable<object> parameters);

        bool IsAssignableFrom([NotNull] ITypeInfo type);
    }
}
