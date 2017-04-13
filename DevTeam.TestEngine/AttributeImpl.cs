namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Contracts.Reflection;

    internal class AttributeImpl: IAttribute
    {
        private readonly Dictionary<string, object> _properties;

        public AttributeImpl(
            [NotNull] IAttributeDescriptor descriptor,
            [NotNull] Attribute attribute,
            [NotNull] IReflection reflection)
        {
            if (descriptor == null) throw new ArgumentNullException(nameof(descriptor));
            if (attribute == null) throw new ArgumentNullException(nameof(attribute));
            if (reflection == null) throw new ArgumentNullException(nameof(reflection));
            var type = attribute.GetType();
            Descriptor = descriptor;
            var typeInfo = reflection.CreateType(type);
            _properties = (
                from property in typeInfo.Properties
                select new { name = property.Name, value = property.GetValue(attribute)}).ToDictionary(i => i.name, i=> i.value);
        }

        public IAttributeDescriptor Descriptor { get; }

        public bool TryGetValue(IPropertyDescriptor descriptor, out object value)
        {
            if (descriptor == null) throw new ArgumentNullException(nameof(descriptor));
            return _properties.TryGetValue(descriptor.PropertyName, out value);
        }

        public T GetValue<T>(IPropertyDescriptor descriptor)
        {
            if (!TryGetValue(descriptor, out object value))
            {
                throw new InvalidOperationException($"Property {descriptor.PropertyName} was not found in the ${Descriptor.FullTypeName}");
            }

            return (T) value;
        }
    }
}
