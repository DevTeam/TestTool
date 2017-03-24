namespace DevTeam.TestEngine.Contracts
{
    using System;
    using Reflection;

    public interface ITestInfo
    {
        IAssemblyInfo Assembly { [NotNull] get; }

        ITypeInfo Type { [NotNull] get; }

        Type[] GenericArgs { [NotNull] get; }

        object[] TypeParameters { [NotNull] get; }

        IMethodInfo Method { [NotNull] get; }

        object[] MethodParameters { [NotNull] get; }

        ICase TestCase { [NotNull] get; }
    }
}