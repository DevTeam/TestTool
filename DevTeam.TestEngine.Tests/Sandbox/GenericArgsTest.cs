namespace DevTeam.TestEngine.Tests.Sandbox
{
    using TestFramework;

    [Test.Case("a", 10)]
    [Test.Case("b", 20)]
    [Test.GenericArgs(typeof(string), typeof(int))]
    [Test.GenericArgs(typeof(double), typeof(object))]
    public class GenericArgsTest<T1, T2>
    {
        public GenericArgsTest(string str, int num)
        {
        }

        [Test.Case(33, "abc")]
        [Test.Case(44, "xyz")]
        public void SuccessTest(int num, string str)
        {
        }

        [Test.Case(true)]
        [Test.Case(false)]
        public void FailedTest(bool flag)
        {
        }
    }
}
