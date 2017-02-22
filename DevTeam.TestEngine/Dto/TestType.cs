namespace DevTeam.TestEngine.Dto
{
    using System;
    using Contracts;

    internal class TestType: ITestType
    {
        public TestType([NotNull] string fullyQualifiedName)
        {
            if (fullyQualifiedName == null) throw new ArgumentNullException(nameof(fullyQualifiedName));
            FullyQualifiedTypeName = fullyQualifiedName;
        }

        public string FullyQualifiedTypeName { get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as ITestType;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return FullyQualifiedTypeName.GetHashCode();
        }

        public static bool operator ==(TestType left, TestType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TestType left, TestType right)
        {
            return !Equals(left, right);
        }

        protected bool Equals(ITestType other)
        {
            return string.Equals(FullyQualifiedTypeName, other.FullyQualifiedTypeName);
        }
    }
}
