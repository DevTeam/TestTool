namespace DevTeam.TestEngine.Tests.Sandbox
{
    using TestFramework;

    [Test.Args("a", 10)]
    [Test.Args("b", 20)]
    [Test.Types(typeof(string), typeof(int))]
    [Test.Types(typeof(double), typeof(object))]
    public class ClassGenericArgsTest<T1, T2>
    {
        public ClassGenericArgsTest(string str, int num)
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
