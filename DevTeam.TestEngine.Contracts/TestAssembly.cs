namespace DevTeam.TestEngine.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TestAssembly: ITestElement
    {
        public TestAssembly(
            Guid id,
            [NotNull] string fullyQualifiedName,
            [NotNull] string displayName,
            [NotNull] string source,
            [NotNull] IEnumerable<TestClass> classes)
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
            Classes = classes.ToArray();
        }

        public Guid Id { get; }

        public string FullyQualifiedName { get; }

        public string DisplayName { get; }

        public string Source { get; }

        public IEnumerable<TestClass> Classes { [NotNull] get; }
    }
}
