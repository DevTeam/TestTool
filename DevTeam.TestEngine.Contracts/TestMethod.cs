namespace DevTeam.TestEngine.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TestMethod : ITestElement
    {
        public TestMethod(
            Guid id,
            [NotNull] string fullyQualifiedName,
            [NotNull] string displayName,
            [NotNull] IEnumerable<TestCase> cases)
        {
            if (id == Guid.Empty) throw new ArgumentException("Value cannot be empty.", nameof(id));
            if (string.IsNullOrWhiteSpace(fullyQualifiedName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(fullyQualifiedName));
            if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(displayName));
            if (cases == null) throw new ArgumentNullException(nameof(cases));
            Id = id;
            FullyQualifiedName = fullyQualifiedName;
            DisplayName = displayName;
            Cases = cases.ToArray();
        }

        public Guid Id { get; }

        public string FullyQualifiedName { get; }

        public string DisplayName { get; }

        public IEnumerable<TestCase> Cases { [NotNull] get; }
    }
}
