namespace DevTeam.TestEngine.Contracts.Reflection
{
    using System;
    using System.Collections.Generic;

    public interface IMemberInfo
    {
        [NotNull] IEnumerable<T> GetCustomAttributes<T>() where T : Attribute;
    }
}
