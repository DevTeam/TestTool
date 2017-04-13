namespace DevTeam.TestEngine.Reflection
{
    using System;
    using System.IO;
    using System.Reflection;
    using Contracts;
    using Contracts.Reflection;
#if NETCOREAPP1_0 || NETSTANDARD1_5
    using System.Runtime.Loader;
#endif

    internal class Reflection: IReflection
    {
        private readonly Func<Assembly, IAssemblyInfo> _assemblyInfoFactory;
        [NotNull] private readonly Func<Type, ITypeInfo> _typeInfoFactory;
        [NotNull] private readonly Func<MethodInfo, IMethodInfo> _methodInfoFactory;
        [NotNull] private readonly Func<PropertyInfo, IPropertyInfo> _propertyInfoFactory;
        [NotNull] private readonly Func<ParameterInfo, IParameterInfo> _parameterInfoFactory;

        public Reflection(
            [NotNull] Func<Assembly, IAssemblyInfo> assemblyInfoFactory,
            [NotNull] Func<Type, ITypeInfo> typeInfoFactory,
            [NotNull] Func<MethodInfo, IMethodInfo> methodInfoFactory,
            [NotNull] Func<PropertyInfo, IPropertyInfo> propertyInfoFactory,
            [NotNull] Func<ParameterInfo, IParameterInfo> parameterInfoFactory)
        {
            if (assemblyInfoFactory == null) throw new ArgumentNullException(nameof(assemblyInfoFactory));
            if (typeInfoFactory == null) throw new ArgumentNullException(nameof(typeInfoFactory));
            if (methodInfoFactory == null) throw new ArgumentNullException(nameof(methodInfoFactory));
            if (propertyInfoFactory == null) throw new ArgumentNullException(nameof(propertyInfoFactory));
            if (parameterInfoFactory == null) throw new ArgumentNullException(nameof(parameterInfoFactory));
            _assemblyInfoFactory = assemblyInfoFactory;
            _typeInfoFactory = typeInfoFactory;
            _methodInfoFactory = methodInfoFactory;
            _propertyInfoFactory = propertyInfoFactory;
            _parameterInfoFactory = parameterInfoFactory;
        }

        public IAssemblyInfo LoadAssembly(string assemblyFile)
        {
            if (assemblyFile == null) throw new ArgumentNullException(nameof(assemblyFile));
#if NETCOREAPP1_0 || NETSTANDARD1_5
            return _assemblyInfoFactory(AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyFile));
#else
            return _assemblyInfoFactory(Assembly.LoadFile(Path.GetFullPath(assemblyFile)));
#endif
        }

        public IAssemblyInfo CreateAssembly(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            return _assemblyInfoFactory(assembly);
        }

        public ITypeInfo CreateType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return _typeInfoFactory(type);
        }

        public IMethodInfo CreateMethod(MethodInfo method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            return _methodInfoFactory(method);
        }

        public IPropertyInfo CreateProperty(PropertyInfo property)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));
            return _propertyInfoFactory(property);
        }

        public IParameterInfo CreateParameter(ParameterInfo parameterInfo)
        {
            if (parameterInfo == null) throw new ArgumentNullException(nameof(parameterInfo));
            return _parameterInfoFactory(parameterInfo);
        }
    }
}
