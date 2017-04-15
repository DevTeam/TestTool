namespace DevTeam.TestEngine.Tests.Sandbox
{
    using TestFramework;

    [Test.Args("a", 10)]
    [Test.Args("b", 20)]
    public class SimpleArgsTest
    {
        public SimpleArgsTest(string str, int num)
        {
        }

        [Test.Args(33, "abc")]
        [Test.Args(44, "xyz")]
        public void SuccessTest(int num, string str)
        {
        }

        [Test.Args(true)]
        [Test.Args(false)]
        public void FailedTest(bool flag)
        {
        }
    }
}
