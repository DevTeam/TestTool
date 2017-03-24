namespace DevTeam.TestEngine.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Contracts;
    using Contracts.Reflection;

    internal class AssemblyInfoImpl : IAssemblyInfo
    {
        [NotNull] private readonly IReflection _reflection;
        private readonly Assembly _assembly;
        
        public AssemblyInfoImpl(
            [NotNull] IReflection reflection,
            [NotNull] Assembly assembly)
        {
            if (reflection == null) throw new ArgumentNullException(nameof(reflection));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            _reflection = reflection;
            _assembly = assembly;
        }

        public string FullName => _assembly.FullName;

        public string Name => _assembly.GetName().Name;

#if NET35 || NET40
        public IEnumerable<ITypeInfo> DefinedTypes => _assembly.GetTypes().Select(i => _reflection.CreateType(i));
#else
        public IEnumerable<ITypeInfo> DefinedTypes => _assembly.DefinedTypes.Select(i => _reflection.CreateType(i.AsType()));
#endif

        public ITypeInfo GetType(string fullyQualifiedTypeName)
        {
            if (fullyQualifiedTypeName == null) throw new ArgumentNullException(nameof(fullyQualifiedTypeName));
            return _reflection.CreateType(_assembly.GetType(fullyQualifiedTypeName));
        }
    }
}
