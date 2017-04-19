namespace TestProject
{
    using DevTeam.TestFramework;

    [Test.Args("abc")]
    [Test.Args("xyz")]
    [Test.Types(typeof(char), typeof(int))]
    [Test.Types(typeof(double), typeof(float))]
    public class UnitTest1<T1, T2>
    {
        public UnitTest1(string str)
        {
        }

        [Test.Args(1)]
        [Test.Args(2)]
        [Test.Types(typeof(int))]
        [Test.Types(typeof(string))]
        public void Success<T>(int num)
        {
        }

        [Test.Ignore("some reason")]
        [Test.Args('a')]
        [Test.Args('b')]
        public void Ignored(char ch)
        {
        }
    }
}
