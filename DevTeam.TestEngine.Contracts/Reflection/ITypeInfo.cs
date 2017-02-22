namespace DevTeam.TestEngine.Contracts.Reflection
{
    using System.Collections.Generic;

    public interface ITypeInfo
    {
        string FullName { [NotNull] get; }

        string Name { [NotNull] get; }

        IEnumerable<IMethodInfo> Methods { [NotNull] get; }
    }
}
