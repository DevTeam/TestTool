namespace DevTeam.TestEngine.Contracts
{
    using System.Collections.Generic;

    public interface IAttributeDescriptor
    {
        IEnumerable<IPropertyDescriptor> Properties { [NotNull] get; }

        string FullTypeName { [NotNull] get; }
    }
}