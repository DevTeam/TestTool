namespace DevTeam.TestEngine.Contracts.Reflection
{
    using System;
    using System.Reflection;

    public interface IReflection
    {
        [NotNull] IAssemblyInfo LoadAssembly([NotNull] string assemblyFile);

        [NotNull] IAssemblyInfo CreateAssembly([NotNull] Assembly assembly);

        [NotNull] ITypeInfo CreateType([NotNull] Type type);

        [NotNull] IMethodInfo CreateMethod([NotNull] MethodInfo method);

        [NotNull] IPropertyInfo CreateProperty([NotNull] PropertyInfo property);

        [NotNull] IParameterInfo CreateParameter([NotNull] ParameterInfo parameterInfo);
    }
}
