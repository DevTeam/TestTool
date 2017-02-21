namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    internal class TestClass : ITestClass
    {
        public TestClass(
            Guid id,
            [NotNull] string fullyQualifiedName,
            [NotNull] string displayName,
            [NotNull] IEnumerable<ITestMethod> methods)
        {
            if (id == Guid.Empty) throw new ArgumentException("Value cannot be empty.", nameof(id));
            if (string.IsNullOrWhiteSpace(fullyQualifiedName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(fullyQualifiedName));
            if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(displayName));
            if (methods == null) throw new ArgumentNullException(nameof(methods));
            Id = id;
            FullyQualifiedName = fullyQualifiedName;
            DisplayName = displayName;
            Methods = methods;
        }

        public Guid Id { get; }

        public string FullyQualifiedName { get; }

        public string DisplayName { get; }

        public ITestAssembly Assembly { get; [NotNull] set; }

        public IEnumerable<ITestMethod> Methods { [NotNull] get; }
    }
}
