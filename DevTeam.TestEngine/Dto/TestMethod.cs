namespace DevTeam.TestEngine.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;

    internal class TestMethod : ITestMethod
    {
        private readonly IList<ITestCase> _cases;

        public TestMethod(
            Guid id,
            [NotNull] string fullyQualifiedName,
            [NotNull] string displayName,
            ITestClass testClass,
            [NotNull] IEnumerable<ITestType> parameters)
        {
            if (id == Guid.Empty) throw new ArgumentException("Value cannot be empty.", nameof(id));
            if (string.IsNullOrWhiteSpace(fullyQualifiedName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(fullyQualifiedName));
            if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(displayName));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            Class = testClass;
            Id = id;
            FullyQualifiedName = fullyQualifiedName;
            DisplayName = displayName;
            Parameters = parameters.ToArray();
            _cases = new List<ITestCase>();
        }

        public Guid Id { get; }

        public string FullyQualifiedName { get; }

        public string DisplayName { get; }

        public ITestClass Class { get; }

        public IEnumerable<ITestType> Parameters { get; }

        public string CodeFilePath { get; set; }

        public int LineNumber { get; set; }

        public IEnumerable<ITestCase> Cases => _cases;

        public void Add([NotNull] ITestCase testCase)
        {
            if (testCase == null) throw new ArgumentNullException(nameof(testCase));
            _cases.Add(testCase);
        }
    }
}
