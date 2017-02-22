namespace DevTeam.TestEngine.Contracts.Reflection
{
    using System.Collections.Generic;

    public interface IAssemblyInfo
    {
        string FullName { [NotNull] get; }

        string Name { [NotNull] get; }

        IEnumerable<ITypeInfo> DefinedTypes { [NotNull] get; }
    }
}
