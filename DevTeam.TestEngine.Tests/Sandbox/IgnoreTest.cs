namespace DevTeam.TestEngine.Tests.Sandbox
{
    using TestFramework;

    public class IgnoreTest
    {
        [Test.Ignore]
        public void Ignore()
        {
        }

        [Test.Ignore("some reason")]
        public void IgnoreWithReason()
        {
        }
    }
}
