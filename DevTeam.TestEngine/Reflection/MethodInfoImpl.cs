﻿namespace DevTeam.TestEngine.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Contracts;
    using Contracts.Reflection;

    internal class MethodInfoImpl: IMethodInfo
    {
        [NotNull] private readonly IReflection _reflection;
        [NotNull] private readonly MethodInfo _methodInfo;
        
        public MethodInfoImpl(
            [NotNull] IReflection reflection,
            [NotNull] MethodInfo methodInfo)
        {
            if (reflection == null) throw new ArgumentNullException(nameof(reflection));
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));
            _reflection = reflection;
            _methodInfo = methodInfo;
        }

        string IMethodInfo.Name => _methodInfo.Name;

        public IEnumerable<IParameterInfo> Parameters => _methodInfo.GetParameters().Select(i => _reflection.CreateParameter(i));

        public bool IsGenericMethodDefinition => _methodInfo.IsGenericMethodDefinition;

        public ITypeInfo[] GetGenericArguments => _methodInfo.GetGenericArguments().Select(type => _reflection.CreateType(type)).ToArray();

        public IMethodInfo MakeGenericMethod(params Type[] typeArguments)
        {
            return _reflection.CreateMethod(_methodInfo.MakeGenericMethod(typeArguments));
        }

        public IEnumerable<T> GetCustomAttributes<T>()
             where T : Attribute
        {
#if NET35 || NET40
            return _methodInfo.GetCustomAttributes(typeof(T), true).Cast<T>();
#else
            return _methodInfo.GetCustomAttributes<T>(true);
#endif
        }

        public object Invoke(object obj, params object[] parameters)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            return _methodInfo.Invoke(obj, parameters);
        }
    }
}