namespace DevTeam.TestEngine.Dto
{
    using System;
    using Contracts;

    internal class TestCase : ITestCase
    {
        public TestCase(
            Guid id,
            [NotNull] string fullyQualifiedCaseName,
            [NotNull] string displayName,
            ITestMethod testMethod)
        {
            if (id == Guid.Empty) throw new ArgumentException("Value cannot be empty.", nameof(id));
            if (fullyQualifiedCaseName == null) throw new ArgumentNullException(nameof(fullyQualifiedCaseName));
            if (displayName == null) throw new ArgumentNullException(nameof(displayName));
            Id = id;
            FullyQualifiedCaseName = fullyQualifiedCaseName;
            DisplayName = displayName;
            Method = testMethod;
        }

        public Guid Id { get; }

        public string FullyQualifiedCaseName { get; }

        public string DisplayName { get; }

        public ITestMethod Method { get; [NotNull] set; }
    }
}
