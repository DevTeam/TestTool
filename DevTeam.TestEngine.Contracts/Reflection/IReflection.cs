namespace DevTeam.TestEngine.Contracts.Reflection
{
    using System;
    using System.Reflection;

    public interface IReflection
    {
        [NotNull] IAssemblyInfo LoadAssembly([NotNull] string source);

        IAssemblyInfo CreateAssembly([NotNull] Assembly assembly);

        ITypeInfo CreateType([NotNull] Type type);

        IMethodInfo CreateMethod([NotNull] MethodInfo method);

        IParameterInfo CreateParameter([NotNull] ParameterInfo parameterInfo);
    }
}
