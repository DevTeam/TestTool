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

        public MethodInfo(
            [NotNull] System.Reflection.MethodInfo methodInfo,
            [NotNull] Func<System.Reflection.ParameterInfo, IParameterInfo> parameterInfoFactory)
        {
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));
            if (parameterInfoFactory == null) throw new ArgumentNullException(nameof(parameterInfoFactory));
            _methodInfo = methodInfo;
            _parameterInfoFactory = parameterInfoFactory;
        }

        string IMethodInfo.Name => _methodInfo.Name;

        public IEnumerable<IParameterInfo> Parameters => _methodInfo.GetParameters().Select(i => _parameterInfoFactory(i));

        public IEnumerable<T> GetCustomAttributes<T>()
             where T : Attribute
        {
            return _methodInfo.GetCustomAttributes<T>();
        }
    }
}
