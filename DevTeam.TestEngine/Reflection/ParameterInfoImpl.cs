namespace DevTeam.TestEngine.Reflection
{
    using System;
    using System.Reflection;
    using Contracts;
    using Contracts.Reflection;

    internal class ParameterInfoImpl : IParameterInfo
    {
        [NotNull] private readonly IReflection _reflection;
        private readonly ParameterInfo _parameterInfo;

        public ParameterInfoImpl(
            [NotNull] IReflection reflection,
            [NotNull] ParameterInfo parameterInfo)
        {
            if (reflection == null) throw new ArgumentNullException(nameof(reflection));
            if (parameterInfo == null) throw new ArgumentNullException(nameof(parameterInfo));
            _reflection = reflection;
            _parameterInfo = parameterInfo;
        }

        public ITypeInfo ParameterType => _reflection.CreateType(_parameterInfo.ParameterType);

        public string Name => _parameterInfo.Name;
    }
}
