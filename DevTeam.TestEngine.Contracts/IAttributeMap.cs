namespace DevTeam.TestEngine.Contracts
{
    public interface IAttributeMap
    {
        [NotNull] IAttributeDescriptor GetDescriptor(Wellknown.Attributes attribute);

        [NotNull] IPropertyDescriptor GetDescriptor(Wellknown.Properties property);
    }
}
