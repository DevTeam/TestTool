namespace DevTeam.TestEngine.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Contracts;
    using Contracts.Reflection;

    internal class MethodInfo: IMethodInfo
    {
        [NotNull] private readonly System.Reflection.MethodInfo _methodInfo;
        [NotNull] private readonly Func<System.Reflection.ParameterInfo, IParameterInfo> _parameterInfoFactory;
        [NotNull] private readonly Func<System.Reflection.TypeInfo, ITypeInfo> _typeInfoFactory;

        public MethodInfo(
            [NotNull] System.Reflection.MethodInfo methodInfo,
            [NotNull] Func<System.Reflection.ParameterInfo, IParameterInfo> parameterInfoFactory,
            [NotNull] Func<System.Reflection.TypeInfo, ITypeInfo> typeInfoFactory)
        {
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));
            if (parameterInfoFactory == null) throw new ArgumentNullException(nameof(parameterInfoFactory));
            if (typeInfoFactory == null) throw new ArgumentNullException(nameof(typeInfoFactory));
            _methodInfo = methodInfo;
            _parameterInfoFactory = parameterInfoFactory;
            _typeInfoFactory = typeInfoFactory;
        }

        string IMethodInfo.Name => _methodInfo.Name;

        public IEnumerable<IParameterInfo> Parameters => _methodInfo.GetParameters().Select(i => _parameterInfoFactory(i));

        public bool IsGenericMethodDefinition => _methodInfo.IsGenericMethodDefinition;

        public ITypeInfo[] GetGenericArguments => _methodInfo.GetGenericArguments().Select(type => _typeInfoFactory(type.GetTypeInfo())).ToArray();

        public IMethodInfo MakeGenericMethod(params Type[] typeArguments)
        {
            return new MethodInfo(_methodInfo.MakeGenericMethod(typeArguments), _parameterInfoFactory, _typeInfoFactory);
        }

        public IEnumerable<T> GetCustomAttributes<T>()
             where T : Attribute
        {
            return _methodInfo.GetCustomAttributes<T>();
        }

        public object Invoke(object obj, params object[] parameters)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            return _methodInfo.Invoke(obj, parameters);
        }
    }
}
