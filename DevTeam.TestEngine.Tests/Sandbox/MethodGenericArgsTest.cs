namespace DevTeam.TestEngine.Tests.Sandbox
{
    using TestFramework;

    public class MethodGenericArgsTest
    {
        [Test.Types(typeof(int), typeof(string))]
        [Test.Types(typeof(string), typeof(double))]
        public void SuccessTest<T1, T2>()
        {
        }
    }
}
