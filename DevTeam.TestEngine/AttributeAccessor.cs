namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Contracts.Reflection;

    internal class AttributeAccessor: IAttributeAccessor
    {
        private readonly IReflection _reflection;
        [NotNull] private readonly Func<IAttributeDescriptor, Attribute, IAttribute> _attributeFactory;

        public AttributeAccessor(
            [NotNull] IReflection reflection,
            [NotNull] Func<IAttributeDescriptor, Attribute, IAttribute> attributeFactory)
        {
            if (reflection == null) throw new ArgumentNullException(nameof(reflection));
            if (attributeFactory == null) throw new ArgumentNullException(nameof(attributeFactory));
            _reflection = reflection;
            _attributeFactory = attributeFactory;
        }

        public IEnumerable<IAttribute> GetAttributes(IMemberInfo memberInfo, IAttributeDescriptor descriptor)
        {
            if (memberInfo == null) throw new ArgumentNullException(nameof(memberInfo));
            if (descriptor == null) throw new ArgumentNullException(nameof(descriptor));
            return memberInfo.GetCustomAttributes<Attribute>().Where(attribute => MatchTypeName(attribute.GetType(), descriptor.FullTypeName)).Select(attribute => _attributeFactory(descriptor, attribute)).Where(attribute => MatchAttribute(attribute, descriptor));
        }

        private bool MatchTypeName(Type type, string fullTypeName)
        {
            var curType = _reflection.CreateType(type);
            do
            {
                if (fullTypeName.Equals(curType.FullName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                curType = curType.BaseType;
            }
            while (curType.Type != typeof(Attribute));

            return false;
        }

        private bool MatchAttribute([NotNull] IAttribute attribute, [NotNull] IAttributeDescriptor descriptor)
        {
            if (attribute == null) throw new ArgumentNullException(nameof(attribute));
            if (descriptor == null) throw new ArgumentNullException(nameof(descriptor));
            return descriptor.Properties.All(propertyDescriptor => MatchProperty(attribute, propertyDescriptor));
        }

        private bool MatchProperty(IAttribute attribute, IPropertyDescriptor propertyDescriptor)
        {
            if (!attribute.TryGetValue(propertyDescriptor, out object value))
            {
                return true;
            }

            var propertyTypeInfo = _reflection.CreateType(propertyDescriptor.PropertyType);
            var valueTypeInfo = _reflection.CreateType(value?.GetType() ?? typeof(object));
            return propertyTypeInfo.IsAssignableFrom(valueTypeInfo);
        }
    }
}
