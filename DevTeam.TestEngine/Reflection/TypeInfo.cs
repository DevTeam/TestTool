namespace DevTeam.TestEngine.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Contracts;
    using Contracts.Reflection;

    internal class TypeInfo : ITypeInfo
    {
        private readonly System.Reflection.TypeInfo _typeInfo;
        private readonly Func<System.Reflection.MethodInfo, IMethodInfo> _methodInfoFactory;

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

        public bool IsGenericTypeDefinition => _typeInfo.IsGenericTypeDefinition;

        public ITypeInfo[] GenericTypeParameters => _typeInfo.GenericTypeParameters.Select(type => (ITypeInfo)new TypeInfo(type.GetTypeInfo(), _methodInfoFactory)).ToArray();

        public IEnumerable<IMethodInfo> Methods => _typeInfo.DeclaredMethods.Select(i => _methodInfoFactory(i));

        public ITypeInfo MakeGenericType(params Type[] typeArguments)
        {
            if (typeArguments == null) throw new ArgumentNullException(nameof(typeArguments));
            return new TypeInfo(_typeInfo.MakeGenericType(typeArguments).GetTypeInfo(), _methodInfoFactory);
        }

        public IEnumerable<T> GetCustomAttributes<T>() where T : Attribute
        {
            return _typeInfo.GetCustomAttributes<T>();
        }

        public object CreateInstance(params object[] parameters)
        {
            return Activator.CreateInstance(_typeInfo.AsType(), parameters);
        }
    }
}
