namespace DevTeam.TestEngine.Tests.Sandbox
{
    using System.Collections;
    using TestFramework;

    [Test.GenericArgsSource(typeof(GenericArgsSingleItemSource))]
    public class GenericArgsSingleItemSourceTest<T>
    {
        [Test.Case(33, "abc")]
        public void SuccessTest(int num, string str)
        {
        }
    }

    internal class GenericArgsSingleItemSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return typeof(int);
            yield return typeof(double);
        }
    }
}
