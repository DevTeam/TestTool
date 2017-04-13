namespace DevTeam.TestEngine
{
    using System;
    using Contracts;
    using System.Collections.Generic;

    internal class AttributeMap : IAttributeMap
    {
        internal static readonly IPropertyDescriptor PropertyParameters = new PropertyDescriptor("Parameters", typeof(IEnumerable<object>));
        internal static readonly IPropertyDescriptor PropertyTypes = new PropertyDescriptor("Types", typeof(IEnumerable<Type>));

        internal static readonly IAttributeDescriptor AttributeTest = new AttributeDescriptor("DevTeam.TestFramework.TestAttribute");
        internal static readonly IAttributeDescriptor AttributeCase = new AttributeDescriptor("DevTeam.TestFramework.Test+CaseAttribute", PropertyParameters);
        internal static readonly IAttributeDescriptor AttributeCaseSource = new AttributeDescriptor("DevTeam.TestFramework.Test+CaseSourceAttribute", PropertyTypes);
        internal static readonly IAttributeDescriptor AttributeGenericArgs = new AttributeDescriptor("DevTeam.TestFramework.Test+GenericArgsAttribute", PropertyTypes);
        internal static readonly IAttributeDescriptor AttributeGenericArgsSource = new AttributeDescriptor("DevTeam.TestFramework.Test+GenericArgsSourceAttribute", PropertyTypes);

        public IAttributeDescriptor GetDescriptor(Wellknown.Attributes attribute)
        {
            switch (attribute)
            {
                case Wellknown.Attributes.Test:
                    return AttributeTest;

                case Wellknown.Attributes.Case:
                    return AttributeCase;

                case Wellknown.Attributes.GenericArgs:
                    return AttributeGenericArgs;

                case Wellknown.Attributes.CaseSource:
                    return AttributeCaseSource;

                case Wellknown.Attributes.GenericArgsSource:
                    return AttributeGenericArgsSource;

                default:
                    throw new ArgumentOutOfRangeException(nameof(attribute), attribute, null);
            }
        }

        public IPropertyDescriptor GetDescriptor(Wellknown.Properties property)
        {
            switch (property)
            {
                case Wellknown.Properties.Parameters:
                    return PropertyParameters;

                case Wellknown.Properties.Types:
                    return PropertyTypes;

                default:
                    throw new ArgumentOutOfRangeException(nameof(property), property, null);
            }
        }

        private class AttributeDescriptor : IAttributeDescriptor
        {
            private readonly IPropertyDescriptor[] _properties;

            public AttributeDescriptor([NotNull] string fullTypeName, [NotNull] params IPropertyDescriptor[] properties)
            {
                if (fullTypeName == null) throw new ArgumentNullException(nameof(fullTypeName));
                if (properties == null) throw new ArgumentNullException(nameof(properties));
                FullTypeName = fullTypeName;
                _properties = properties;
            }

            public string FullTypeName { get; }

            public IEnumerable<IPropertyDescriptor> Properties => _properties;
        }

        private class PropertyDescriptor : IPropertyDescriptor
        {
            public PropertyDescriptor([NotNull] string propertyName, [NotNull] Type propertyType)
            {
                if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
                if (propertyType == null) throw new ArgumentNullException(nameof(propertyType));
                PropertyName = propertyName;
                PropertyType = propertyType;
            }

            public string PropertyName { get; }

            public Type PropertyType { get; }
        }
    }
}
