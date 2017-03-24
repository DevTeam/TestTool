namespace DevTeam.TestEngine.Tests.Sandbox
{
    using TestFramework;

    [Test("a", 10)]
    [Test("b", 20)]
    public class ParamsTest
    {
        public ParamsTest(string str, int num)
        {
        }

        [Test(33, "abc")]
        [Test(44, "xyz")]
        public void SuccessTest(int num, string str)
        {
        }

        [Test(true)]
        [Test(false)]
        public void FailedTest(bool flag)
        {
        }
    }
}
