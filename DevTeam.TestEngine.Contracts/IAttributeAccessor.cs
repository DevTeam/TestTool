namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;
    using Reflection;

    public interface IAttributeAccessor
    {
        [NotNull] IEnumerable<IAttribute> GetAttributes([NotNull] IMemberInfo memberInfo, [NotNull] IAttributeDescriptor descriptor);
    }
}