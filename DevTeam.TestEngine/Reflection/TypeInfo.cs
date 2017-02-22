namespace DevTeam.TestEngine.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Contracts.Reflection;

    internal class TypeInfo : ITypeInfo
    {
        private readonly System.Reflection.TypeInfo _typeInfo;
        [NotNull] private readonly Func<System.Reflection.MethodInfo, IMethodInfo> _methodInfoFactory;

        public TypeInfo(
            [NotNull] System.Reflection.TypeInfo typeInfo,
            [NotNull] Func<System.Reflection.MethodInfo, IMethodInfo> methodInfoFactory)
        {
            if (typeInfo == null) throw new ArgumentNullException(nameof(typeInfo));
            if (methodInfoFactory == null) throw new ArgumentNullException(nameof(methodInfoFactory));
            _typeInfo = typeInfo;
            _methodInfoFactory = methodInfoFactory;
        }

        public string FullName => _typeInfo.FullName;

        public string Name => _typeInfo.Name;

        public IEnumerable<IMethodInfo> Methods => _typeInfo.DeclaredMethods.Select(i => _methodInfoFactory(i));
    }
}
