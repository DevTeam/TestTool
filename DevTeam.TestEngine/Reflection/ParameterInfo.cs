namespace DevTeam.TestEngine.Reflection
{
    using System;
    using System.Reflection;
    using Contracts;
    using Contracts.Reflection;

    internal class ParameterInfo : IParameterInfo
    {
        private readonly System.Reflection.ParameterInfo _parameterInfo;
        private readonly Func<System.Reflection.TypeInfo, ITypeInfo> _typeInfoFactory;

        public ParameterInfo(
            [NotNull] System.Reflection.ParameterInfo parameterInfo,
            [NotNull] Func<System.Reflection.TypeInfo, ITypeInfo> typeInfoFactory)
        {
            if (parameterInfo == null) throw new ArgumentNullException(nameof(parameterInfo));
            if (typeInfoFactory == null) throw new ArgumentNullException(nameof(typeInfoFactory));
            _parameterInfo = parameterInfo;
            _typeInfoFactory = typeInfoFactory;
        }

        public ITypeInfo ParameterType => _typeInfoFactory(_parameterInfo.ParameterType.GetTypeInfo());
    }
}
