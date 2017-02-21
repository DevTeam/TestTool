namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    internal class TestAssembly: ITestAssembly
    {
        public TestAssembly(
            Guid id,
            [NotNull] string fullyQualifiedName,
            [NotNull] string displayName,
            [NotNull] string source,
            [NotNull] IEnumerable<ITestClass> classes)
        {
            if (id == Guid.Empty) throw new ArgumentException("Value cannot be empty.", nameof(id));
            if (string.IsNullOrWhiteSpace(fullyQualifiedName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(fullyQualifiedName));
            if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(displayName));
            if (string.IsNullOrWhiteSpace(source)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(source));
            if (classes == null) throw new ArgumentNullException(nameof(classes));
            Id = id;
            FullyQualifiedName = fullyQualifiedName;
            DisplayName = displayName;
            Source = source;
            Classes = classes;
        }

        public Guid Id { get; }

        public string FullyQualifiedName { get; }

        public string DisplayName { get; }

        public string Source { get; }

        public IEnumerable<ITestClass> Classes { [NotNull] get; }
    }
}
