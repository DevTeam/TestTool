namespace DevTeam.TestEngine.Dto
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    internal class TestClass : ITestClass
    {
        private readonly IList<ITestMethod> _methods;

        public TestClass(
            Guid id,
            [NotNull] string fullyQualifiedName,
            [NotNull] string displayName,
            ITestAssembly testAssembly)
        {
            if (id == Guid.Empty) throw new ArgumentException("Value cannot be empty.", nameof(id));
            if (string.IsNullOrWhiteSpace(fullyQualifiedName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(fullyQualifiedName));
            if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(displayName));
            Id = id;
            FullyQualifiedName = fullyQualifiedName;
            DisplayName = displayName;
            _methods = new List<ITestMethod>();
        }

        public Guid Id { get; }

        public string FullyQualifiedName { get; }

        public string DisplayName { get; }

        public ITestAssembly Assembly { get; [NotNull] set; }

        public IEnumerable<ITestMethod> Methods => _methods;

        public void Add([NotNull] ITestMethod testMethod)
        {
            if (testMethod == null) throw new ArgumentNullException(nameof(testMethod));
            _methods.Add(testMethod);
        }
    }
}
