namespace DevTeam.TestEngine.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Contracts;
    using Contracts.Reflection;

    internal class TypeInfoImpl : ITypeInfo
    {
        [NotNull] private readonly IReflection _reflection;
        [NotNull] private readonly Type _type;

#if !NET35 && !NET40
        private readonly TypeInfo _typeInfo;
#endif
        public TypeInfoImpl(
            [NotNull] IReflection reflection,
            [NotNull] Type type)
        {
            if (reflection == null) throw new ArgumentNullException(nameof(reflection));
            if (type == null) throw new ArgumentNullException(nameof(type));
            _reflection = reflection;
            _type = type;
#if !NET35 && !NET40
            _typeInfo = type.GetTypeInfo();
#endif
        }

        public Type Type => _type;

        public string FullName => _type.FullName;
        public string Namespace => _type.Namespace;

        public string Name => _type.Name;

        public object CreateInstance(IEnumerable<object> args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            return Activator.CreateInstance(_type, args.ToArray());
        }


#if NET35
        public ITypeInfo BaseType => _type.BaseType != null ? new TypeInfoImpl(_reflection, _type.BaseType) : null;

        public bool IsGenericTypeDefinition => _type.IsGenericTypeDefinition;

        public bool IsGenericType => _type.IsGenericType;

        public IEnumerable<ITypeInfo> GenericArguments => _type.GetGenericArguments().Select(type => _reflection.CreateType(type));

        public IEnumerable<IMethodInfo> Methods => _type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty).Select(method => _reflection.CreateMethod(method));

        public IEnumerable<IPropertyInfo> Properties => _type.GetProperties().Select(property => _reflection.CreateProperty(property));

        public ITypeInfo MakeGenericType(IEnumerable<Type> genericTypeArguments)
        {
            if (genericTypeArguments == null) throw new ArgumentNullException(nameof(genericTypeArguments));
            return _reflection.CreateType(_type.MakeGenericType(genericTypeArguments.ToArray()));
        }

        public IEnumerable<T> GetCustomAttributes<T>() where T : Attribute
        {
            return _type.GetCustomAttributes(typeof(T), true).Cast<T>();
        }

        public bool IsAssignableFrom(ITypeInfo type)
        {
            return _type.IsAssignableFrom(type.Type);
        }
#else
        public ITypeInfo BaseType => _typeInfo.BaseType != null ? new TypeInfoImpl(_reflection, _typeInfo.BaseType) : null;

        public bool IsGenericTypeDefinition => _typeInfo.IsGenericTypeDefinition;

        public bool IsGenericType => _typeInfo.IsGenericType;

        public IEnumerable<ITypeInfo> GenericArguments => _typeInfo.GetGenericArguments().Select(type => _reflection.CreateType(type));

        public IEnumerable<IMethodInfo> Methods => _typeInfo.DeclaredMethods.Select(i => _reflection.CreateMethod(i));

        public IEnumerable<IPropertyInfo> Properties => _typeInfo.GetProperties().Select(property => _reflection.CreateProperty(property));

        public ITypeInfo MakeGenericType(IEnumerable<Type> genericTypeArguments)
        {
            if (genericTypeArguments == null) throw new ArgumentNullException(nameof(genericTypeArguments));
            var genericType = _type.MakeGenericType(genericTypeArguments.ToArray());
            return _reflection.CreateType(genericType);
        }

        public IEnumerable<T> GetCustomAttributes<T>() where T : Attribute
        {
            return _typeInfo.GetCustomAttributes<T>();
        }

        public bool IsAssignableFrom(ITypeInfo type)
        {
            return _typeInfo.IsAssignableFrom(type.Type.GetTypeInfo());
        }
#endif

        public override string ToString()
        {
            return Name;
        }
    }
}
