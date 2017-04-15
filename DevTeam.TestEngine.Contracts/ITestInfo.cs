namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;
    using Reflection;

    public interface ITestInfo
    {
        IRunner Runner { [NotNull] get; }

        ICase Case { [NotNull] get; }

        string Source { [NotNull] get; }

        IAssemblyInfo Assembly { [NotNull] get; }

        ITypeInfo Type { [NotNull] get; }

        IEnumerable<object> TypeArgs { [NotNull] get; }

        IMethodInfo Method { [NotNull] get; }

        IEnumerable<object> MethodArgs { [NotNull] get; }

        bool Ignore { get; }

        string IgnoreReason { [NotNull] get; }
    }
}