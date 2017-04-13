namespace SampleTests
{
    using DevTeam.TestFramework;

    public class SampleTests
    {
        [Test]
        public void Success()
        {
        }

		[Test.Ignore("some reason")]
        public void Ignored()
        {
        }
    }
}
