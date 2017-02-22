namespace DevTeam.TestEngine.Dto
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    internal class TestClass : ITestClass
    {
        private readonly IList<ITestMethod> _methods;

        public TestClass(
            [NotNull] string fullyQualifiedTypeName,
            [NotNull] string displayName,
            ITestAssembly testAssembly)
        {
            if (string.IsNullOrWhiteSpace(fullyQualifiedTypeName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(fullyQualifiedTypeName));
            if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(displayName));
            FullyQualifiedTypeName = fullyQualifiedTypeName;
            DisplayName = displayName;
            Assembly = testAssembly;
            _methods = new List<ITestMethod>();
        }

        public string FullyQualifiedTypeName { get; }

        public string DisplayName { get; }

        public ITestAssembly Assembly { get; [NotNull] set; }

        public IEnumerable<ITestMethod> Methods => _methods;

        public void AddMethod(ITestMethod testMethod)
        {
            if (testMethod == null) throw new ArgumentNullException(nameof(testMethod));
            _methods.Add(testMethod);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as ITestClass;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return FullyQualifiedTypeName.GetHashCode();
        }

        public static bool operator ==(TestClass left, TestClass right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TestClass left, TestClass right)
        {
            return !Equals(left, right);
        }

        private bool Equals(ITestClass other)
        {
            return string.Equals(FullyQualifiedTypeName, other.FullyQualifiedTypeName);
        }
    }
}
