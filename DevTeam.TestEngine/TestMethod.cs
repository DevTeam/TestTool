namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    internal class TestMethod : ITestMethod
    {
        public TestMethod(
            Guid id,
            [NotNull] string fullyQualifiedName,
            [NotNull] string displayName,
            [NotNull] IEnumerable<ITestCase> cases)
        {
            if (id == Guid.Empty) throw new ArgumentException("Value cannot be empty.", nameof(id));
            if (string.IsNullOrWhiteSpace(fullyQualifiedName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(fullyQualifiedName));
            if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(displayName));
            if (cases == null) throw new ArgumentNullException(nameof(cases));
            Id = id;
            FullyQualifiedName = fullyQualifiedName;
            DisplayName = displayName;
            Cases = cases;
        }

        public Guid Id { get; }

        public string FullyQualifiedName { get; }

        public string DisplayName { get; }

        public ITestClass Class { get; [NotNull] set; }

        public IEnumerable<ITestCase> Cases { [NotNull] get; }
    }
}
