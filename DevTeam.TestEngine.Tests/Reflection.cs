namespace DevTeam.TestEngine.Tests
{
    using System;
    using System.Reflection;
    using Contracts;

    public class Reflection : IReflection
    {
        public Assembly LoadAssembly(string source)
        {
            if (string.IsNullOrWhiteSpace(source)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(source));
            return Assembly.LoadFile(source);
        }
    }
}
