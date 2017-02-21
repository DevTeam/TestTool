using System;

namespace DevTeam.TestAdapter
{
    using System.Reflection;
    using System.Runtime.Loader;
    using TestEngine.Contracts;

    public class Reflection : IReflection
    {
        public Assembly LoadAssembly(string source)
        {
            if (string.IsNullOrWhiteSpace(source)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(source));
            return AssemblyLoadContext.Default.LoadFromAssemblyPath(source);
        }
    }
}
