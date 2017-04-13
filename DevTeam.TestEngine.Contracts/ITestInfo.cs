namespace DevTeam.TestEngine.Contracts
{
    using System;
    using System.Collections.Generic;
    using Reflection;

    public interface ITestInfo
    {
        IAssemblyInfo Assembly { [NotNull] get; }

        ITypeInfo Type { [NotNull] get; }

        IEnumerable<Type> GenericArgs { [NotNull] get; }

        IEnumerable<object> TypeParameters { [NotNull] get; }

        IMethodInfo Method { [NotNull] get; }

        IEnumerable<object> MethodParameters { [NotNull] get; }

        ICase TestCase { [NotNull] get; }
    }
}