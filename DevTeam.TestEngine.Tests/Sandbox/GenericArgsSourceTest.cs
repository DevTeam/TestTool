namespace DevTeam.TestEngine.Tests.Sandbox
{
    using System.Collections;
    using System.Collections.Generic;
    using TestFramework;

    [Test.GenericArgsSource(typeof(GenericArgsSource))]
    public class GenericArgsSourceTest<T1, T2>
    {
        [Test.Case(33, "abc")]
        public void SuccessTest(int num, string str)
        {
        }
    }

    internal class GenericArgsSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[] { typeof(int), typeof(string) };
            yield return new List<object> { typeof(double), typeof(float) };
        }
    }
}
