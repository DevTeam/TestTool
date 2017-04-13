namespace DevTeam.TestEngine.Contracts.Reflection
{
    public interface IPropertyInfo
    {
        string Name { [NotNull] get; }

        [CanBeNull] object GetValue([NotNull] object instance, [NotNull] params object[] index);
    }
}
