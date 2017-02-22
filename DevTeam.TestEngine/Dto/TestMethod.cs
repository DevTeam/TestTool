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
            [NotNull] string name,
            [NotNull] string displayName,
            ITestClass testClass,
            [NotNull] IEnumerable<ITestType> parameters)
        {
            if (id == Guid.Empty) throw new ArgumentException("Value cannot be empty.", nameof(id));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(displayName));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            Name = name;
            DisplayName = displayName;
            Class = testClass;
            Parameters = parameters.ToArray();
            _cases = new List<ITestCase>();
        }

        public string Name { get; }

        public string DisplayName { get; }

        public ITestClass Class { get; }

        public IEnumerable<ITestType> Parameters { get; }

        public string CodeFilePath { get; set; }

        public int LineNumber { get; set; }

        public IEnumerable<ITestCase> Cases => _cases;

        public void AddCase(ITestCase testCase)
        {
            if (testCase == null) throw new ArgumentNullException(nameof(testCase));
            _cases.Add(testCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as ITestMethod;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static bool operator ==(TestMethod left, TestMethod right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TestMethod left, TestMethod right)
        {
            return !Equals(left, right);
        }

        protected bool Equals(ITestMethod other)
        {
            return string.Equals(Name, other.Name) && Parameters.SequenceEqual(other.Parameters);
        }
    }
}
