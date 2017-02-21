namespace DevTeam.TestEngine
{
    using System;
    using Contracts;

    internal class TestCase : ITestCase
    {
        public TestCase(
            Guid id,
            [NotNull] string fullyQualifiedName,
            [NotNull] string displayName)
        {
            if (id == Guid.Empty) throw new ArgumentException("Value cannot be empty.", nameof(id));
            if (fullyQualifiedName == null) throw new ArgumentNullException(nameof(fullyQualifiedName));
            if (displayName == null) throw new ArgumentNullException(nameof(displayName));
            Id = id;
            FullyQualifiedName = fullyQualifiedName;
            DisplayName = displayName;
        }

        public Guid Id { get; }

        public string FullyQualifiedName { get; }

        public string DisplayName { get; }

        public ITestMethod Method { get; [NotNull] set; }

        [CanBeNull] public string CodeFilePath { get; set; }

        public int LineNumber { get; set; }
    }
}
