namespace DevTeam.TestEngine.Contracts.Reflection
{
    using System;
    using System.Collections.Generic;

    public interface IMethodInfo: IMemberInfo
    {
        string Name { [NotNull] get; }

        IEnumerable<IParameterInfo> Parameters { [NotNull] get; }

        bool IsGenericMethodDefinition { get; }

        IEnumerable<ITypeInfo> GenericArguments { get; }

        [NotNull]
        IMethodInfo MakeGenericMethod([NotNull] IEnumerable<Type> genericTypeArguments);


        [CanBeNull]
        object Invoke([NotNull] object obj, [NotNull] IEnumerable<object> parameters);
    }
}
