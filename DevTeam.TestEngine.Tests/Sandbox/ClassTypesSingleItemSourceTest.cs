namespace DevTeam.TestEngine.Tests.Sandbox
{
    using System.Collections;
    using TestFramework;

    [Test.Types.Source(typeof(GenericArgsSource))]
    public class ClassTypesSingleItemSourceTest<T>
    {
        [Test.Args(33, "abc")]
        public void SuccessTest(int num, string str)
        {
        }
    }

    internal class GenericArgsSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return typeof(int);
            yield return typeof(double);
        }
    }
}
