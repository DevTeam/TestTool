namespace DevTeam.TestEngine.Reflection
{
    using System;
    using Contracts.Reflection;
    using System.Reflection;
    using Contracts;

    internal class PropertyInfoImpl : IPropertyInfo
    {
        private readonly PropertyInfo _property;

        public PropertyInfoImpl(
            [NotNull] IReflection reflection,
            [NotNull] PropertyInfo property)
        {
            if (reflection == null) throw new ArgumentNullException(nameof(reflection));
            if (property == null) throw new ArgumentNullException(nameof(property));
            _property = property;
        }

        public string Name => _property.Name;

        public object GetValue(object instance, params object[] index)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (index == null) throw new ArgumentNullException(nameof(index));
            return _property.GetValue(instance, index);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
