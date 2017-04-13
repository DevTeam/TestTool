namespace DevTeam.TestEngine.Contracts
{
    using System;

    public interface IPropertyDescriptor
    {
        string PropertyName { [NotNull] get; }

        Type PropertyType { [NotNull] get; }
    }
}