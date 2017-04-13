namespace DevTeam.TestEngine.Contracts
{
    public interface IAttribute
    {
        IAttributeDescriptor Descriptor { [NotNull] get; }

        bool TryGetValue([NotNull] IPropertyDescriptor descriptor, out object value);

        T GetValue<T>([NotNull] IPropertyDescriptor descriptor);
    }
}
