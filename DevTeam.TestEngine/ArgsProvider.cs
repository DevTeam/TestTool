namespace DevTeam.TestEngine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Contracts.Reflection;

    internal class ArgsProvider: IArgsProvider
    {
        private readonly IReflection _reflection;
        [NotNull] private readonly IAttributeMap _attributeMap;
        private readonly IAttributeAccessor _attributeAccessor;

        public ArgsProvider(
            [NotNull] IReflection reflection,
            [NotNull] IAttributeMap attributeMap,
            [NotNull] IAttributeAccessor attributeAccessor)
        {
            if (reflection == null) throw new ArgumentNullException(nameof(reflection));
            if (attributeMap == null) throw new ArgumentNullException(nameof(attributeMap));
            if (attributeAccessor == null) throw new ArgumentNullException(nameof(attributeAccessor));
            _reflection = reflection;
            _attributeMap = attributeMap;
            _attributeAccessor = attributeAccessor;
        }

        public IEnumerable<IEnumerable<object>> GetTypeParameters(ITypeInfo type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return GetParameters(_attributeAccessor.GetAttributes(type, _attributeMap.GetDescriptor(Wellknown.Attributes.CaseSource)), _attributeAccessor.GetAttributes(type, _attributeMap.GetDescriptor(Wellknown.Attributes.Case)));
        }

        public IEnumerable<IEnumerable<object>> GetMethodParameters(IMethodInfo method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            return GetParameters(_attributeAccessor.GetAttributes(method, _attributeMap.GetDescriptor(Wellknown.Attributes.CaseSource)), _attributeAccessor.GetAttributes(method, _attributeMap.GetDescriptor(Wellknown.Attributes.Case)));
        }

        private IEnumerable<IEnumerable<object>> GetParameters(
            [NotNull] IEnumerable<IAttribute> caseSources,
            [NotNull] IEnumerable<IAttribute> cases)
        {
            if (caseSources == null) throw new ArgumentNullException(nameof(caseSources));
            if (cases == null) throw new ArgumentNullException(nameof(cases));
            var genericArgsFromSources =
                from caseSourceAttribute in caseSources
                from caseSourceType in caseSourceAttribute.GetValue<IEnumerable<Type>>(_attributeMap.GetDescriptor(Wellknown.Properties.Types))
                let caseSourceInstance = _reflection.CreateType(caseSourceType).CreateInstance(Enumerable.Empty<object>()) as IEnumerable
                from paramsItem in GetParams(caseSourceInstance)
                select paramsItem;

            var parameters =
                from caseAttribute in cases
                select caseAttribute.GetValue<IEnumerable<object>>(_attributeMap.GetDescriptor(Wellknown.Properties.Parameters));

            return genericArgsFromSources.Concat(parameters);
        }

        private static IEnumerable<IEnumerable<object>> GetParams([NotNull] IEnumerable source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return
                from genericArgsSource in source.OfType<object>()
                let fromEnum = (genericArgsSource as IEnumerable)?.OfType<object>()
                let value = genericArgsSource
                let fromValue = value != null ? Enumerable.Repeat(value, 1) : Enumerable.Empty<object>()
                select fromEnum ?? fromValue;
        }
    }
}
