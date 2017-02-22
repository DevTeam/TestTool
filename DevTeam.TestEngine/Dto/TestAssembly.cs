namespace DevTeam.TestEngine.Dto
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    internal class TestAssembly: ITestAssembly
    {
        private readonly IList<ITestClass> _classes;

        public TestAssembly(
            Guid id,
            [NotNull] string fullyQualifiedName,
            [NotNull] string displayName,
            [NotNull] string source)
        {
            if (id == Guid.Empty) throw new ArgumentException("Value cannot be empty.", nameof(id));
            if (string.IsNullOrWhiteSpace(fullyQualifiedName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(fullyQualifiedName));
            if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(displayName));
            if (string.IsNullOrWhiteSpace(source)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(source));
            Id = id;
            FullyQualifiedName = fullyQualifiedName;
            DisplayName = displayName;
            Source = source;
            _classes = new List<ITestClass>();
        }

        public Guid Id { get; }

        public string FullyQualifiedName { get; }

        public string DisplayName { get; }

        public string Source { get; }

        public IEnumerable<ITestClass> Classes => _classes;

        public void Add([NotNull] ITestClass testClass)
        {
            if (testClass == null) throw new ArgumentNullException(nameof(testClass));
            _classes.Add(testClass);
        }
    }
}
