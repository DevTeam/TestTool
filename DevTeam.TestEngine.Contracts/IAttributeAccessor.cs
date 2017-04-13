namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;
    using Reflection;

    public interface IAttributeAccessor
    {
        [NotNull] IEnumerable<IAttribute> GetAttributes([NotNull] IMethodInfo method, [NotNull] IAttributeDescriptor descriptor);

        [NotNull] IEnumerable<IAttribute> GetAttributes([NotNull] ITypeInfo type, [NotNull] IAttributeDescriptor descriptor);
    }
}