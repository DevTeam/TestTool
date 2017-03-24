namespace DevTeam.TestEngine.Tests.Sandbox
{
    using TestFramework;

    [Case("a", 10)]
    [Case("b", 20)]
    public class ParamsTest
    {
        public ParamsTest(string str, int num)
        {
        }

        [Case(33, "abc")]
        [Case(44, "xyz")]
        public void SuccessTest(int num, string str)
        {
        }

        [Case(true)]
        [Case(false)]
        public void FailedTest(bool flag)
        {
        }
    }
}
