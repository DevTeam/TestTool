namespace DevTeam.TestEngine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Contracts.Reflection;

    internal class GenericArgsProvider: IGenericArgsProvider
    {
        private readonly IReflection _reflection;
        [NotNull] private readonly IAttributeMap _attributeMap;
        [NotNull] private readonly IAttributeAccessor _attributeAccessor;

        public GenericArgsProvider(
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

        public IEnumerable<IEnumerable<Type>> GetGenericArgs(ITypeInfo type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            var genericArgsFromSources =
                from genericArgsSourceAttribute in _attributeAccessor.GetAttributes(type, _attributeMap.GetDescriptor(Wellknown.Attributes.GenericArgsSource))
                from genericArgsSourceType in genericArgsSourceAttribute.GetValue<IEnumerable<Type>>(_attributeMap.GetDescriptor(Wellknown.Properties.Types))
                let genericArgsSourceInstance = _reflection.CreateType(genericArgsSourceType).CreateInstance(Enumerable.Empty<object>()) as IEnumerable
                from genericArgsItem in GetGenericArgs(genericArgsSourceInstance)
                select genericArgsItem;

            var genericArgs =
                from genericArgsAtr in _attributeAccessor.GetAttributes(type, _attributeMap.GetDescriptor(Wellknown.Attributes.GenericArgs))
                select genericArgsAtr.GetValue<IEnumerable<Type>>(_attributeMap.GetDescriptor(Wellknown.Properties.Types));

            return genericArgsFromSources.Concat(genericArgs);
        }

        private static IEnumerable<IEnumerable<Type>> GetGenericArgs([NotNull] IEnumerable source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return
                from genericArgsSource in source.OfType<object>()
                let fromEnum = (genericArgsSource as IEnumerable)?.OfType<Type>()
                let type = genericArgsSource as Type
                let fromType = type != null ? Enumerable.Repeat(type, 1) : Enumerable.Empty<Type>()
                select fromEnum ?? fromType;
        }
    }
}
