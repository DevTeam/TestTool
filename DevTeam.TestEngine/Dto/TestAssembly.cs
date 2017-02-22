namespace DevTeam.TestEngine.Dto
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    internal class TestAssembly: ITestAssembly
    {
        private readonly IList<ITestClass> _classes;

        public TestAssembly(
            [NotNull] string fullyQualifiedAssemblyName,
            [NotNull] string displayName,
            [NotNull] string source,
            [NotNull] Uri testExecutor)
        {
            if (string.IsNullOrWhiteSpace(fullyQualifiedAssemblyName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(fullyQualifiedAssemblyName));
            if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(displayName));
            if (string.IsNullOrWhiteSpace(source)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(source));
            if (testExecutor == null) throw new ArgumentNullException(nameof(testExecutor));
            FullyQualifiedAssemblyName = fullyQualifiedAssemblyName;
            DisplayName = displayName;
            Source = source;
            TestExecutor = testExecutor;
            _classes = new List<ITestClass>();
        }

        public string FullyQualifiedAssemblyName { get; }

        public string DisplayName { get; }

        public string Source { get; }

        public Uri TestExecutor { get; }

        public IEnumerable<ITestClass> Classes => _classes;

        public void AddClass(ITestClass testClass)
        {
            if (testClass == null) throw new ArgumentNullException(nameof(testClass));
            _classes.Add(testClass);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as ITestAssembly;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Source.GetHashCode() * 397) ^ FullyQualifiedAssemblyName.GetHashCode();
            }
        }

        public static bool operator ==(TestAssembly left, TestAssembly right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TestAssembly left, TestAssembly right)
        {
            return !Equals(left, right);
        }

        private bool Equals(ITestAssembly other)
        {
            return string.Equals(Source, other.Source) && string.Equals(FullyQualifiedAssemblyName, other.FullyQualifiedAssemblyName);
        }
    }
}
