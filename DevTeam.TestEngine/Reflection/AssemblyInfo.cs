namespace DevTeam.TestEngine.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Contracts;
    using Contracts.Reflection;

    internal class AssemblyInfo : IAssemblyInfo
    {
        private readonly Assembly _assembly;
        [NotNull] private readonly Func<System.Reflection.TypeInfo, ITypeInfo> _typeInfoFactory;

        public AssemblyInfo(
            [NotNull] Assembly assembly,
            [NotNull] Func<System.Reflection.TypeInfo, ITypeInfo> typeInfoFactory)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (typeInfoFactory == null) throw new ArgumentNullException(nameof(typeInfoFactory));
            _assembly = assembly;
            _typeInfoFactory = typeInfoFactory;
        }

        public string FullName => _assembly.FullName;

        public string Name => _assembly.GetName().Name;

        public IEnumerable<ITypeInfo> DefinedTypes => _assembly.DefinedTypes.Select(i => _typeInfoFactory(i));

        public ITypeInfo GetType(string fullyQualifiedTypeName)
        {
            if (fullyQualifiedTypeName == null) throw new ArgumentNullException(nameof(fullyQualifiedTypeName));
            return _typeInfoFactory(_assembly.GetType(fullyQualifiedTypeName).GetTypeInfo());
        }
    }
}
