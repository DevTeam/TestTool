namespace DevTeam.TestEngine.Contracts
{
    using System;

    public class TestCase : ITestElement
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

        [CanBeNull] public string CodeFilePath { get; set; }

        public int LineNumber { get; set; }
    }
}
