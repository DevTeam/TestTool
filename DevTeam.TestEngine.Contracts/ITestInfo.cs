namespace DevTeam.TestEngine.Contracts
{
    using System;
    using System.Collections.Generic;
    using Reflection;

    public interface ITestInfo
    {
        IRunner Runner { [NotNull] get; }

        string Source { [NotNull] get; }

        IAssemblyInfo Assembly { [NotNull] get; }

        ITypeInfo Type { [NotNull] get; }

        IEnumerable<Type> TypeGenericArgs { [NotNull] get; }

        IEnumerable<object> TypeArgs { [NotNull] get; }

        IMethodInfo Method { [NotNull] get; }

        IEnumerable<Type> MethodGenericArgs { [NotNull] get; }

        IEnumerable<object> MethodArgs { [NotNull] get; }

        ICase Case { [NotNull] get; }

        bool Ignore { get; }

        string IgnoreReason { [NotNull] get; }
    }
}