namespace DevTeam.TestEngine.Contracts.Reflection
{
    using System;
    using System.Collections.Generic;

    public interface ITypeInfo : IMemberInfo
    {
        Type Type { [NotNull] get; }

        string FullName { [NotNull] get; }

        string Name { [NotNull] get; }

        ITypeInfo BaseType { [CanBeNull] get; }

        bool IsGenericTypeDefinition { get; }

        IEnumerable<ITypeInfo> GenericTypeParameters { get; }

        IEnumerable<IMethodInfo> Methods { [NotNull] get; }

        IEnumerable<IPropertyInfo> Properties { [NotNull] get; }

        [NotNull]
        ITypeInfo MakeGenericType([NotNull] IEnumerable<Type> genericTypeArguments);

        object CreateInstance([NotNull] IEnumerable<object> args);

        bool IsAssignableFrom([NotNull] ITypeInfo type);
    }
}
