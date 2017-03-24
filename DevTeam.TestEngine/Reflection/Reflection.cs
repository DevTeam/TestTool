namespace DevTeam.TestEngine.Reflection
{
    using System;
    using System.Reflection;
    using Contracts.Reflection;
#if NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_5 || NETSTANDARD1_6
    using System.Runtime.Loader;
#endif

    internal class Reflection: IReflection
    {
        public IAssemblyInfo LoadAssembly(string source)
        {
#if NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_5 || NETSTANDARD1_6
            return new AssemblyInfoImpl(this, AssemblyLoadContext.Default.LoadFromAssemblyPath(source));
#else
            return new AssemblyInfoImpl(this, Assembly.LoadFile(source));
#endif
        }


        public IAssemblyInfo CreateAssembly(Assembly assembly)
        {
            return new AssemblyInfoImpl(this, assembly);
        }

        public ITypeInfo CreateType(Type type)
        {
            return new TypeInfoImpl(this, type);
        }

        public IMethodInfo CreateMethod(MethodInfo method)
        {
            return new MethodInfoImpl(this, method);
        }

        public IParameterInfo CreateParameter(ParameterInfo parameterInfo)
        {
            return new ParameterInfoImpl(this, parameterInfo);
        }
    }
}
