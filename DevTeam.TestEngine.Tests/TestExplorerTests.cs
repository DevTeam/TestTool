namespace DevTeam.TestEngine.Tests
{
    using Moq;

    using NUnit.Framework;

    using Shouldly;

    [TestFixture]
    public class TestExplorerTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void Should()
        {
            // Given
            var explorer = CreateInstance();

            // When
            explorer.Explore(@"C:\Projects\DevTeam\TestTool\dotNetCore\SimpleTests\bin\Debug\SimpleTests.dll");

            // Then
        }

        private TestExplorer CreateInstance()
        {
            return new TestExplorer(new Reflection());
        }
    }
}
