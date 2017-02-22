namespace DevTeam.TestEngine.Dto
{
    using System;
    using Contracts;

    internal class TestType: ITestType
    {
        public TestType([NotNull] string fullyQualifiedName)
        {
            if (fullyQualifiedName == null) throw new ArgumentNullException(nameof(fullyQualifiedName));
            FullyQualifiedName = fullyQualifiedName;
        }

        public string FullyQualifiedName { get; }
    }
}
