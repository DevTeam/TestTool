namespace DevTeam.TestEngine.Tests.Sandbox
{
    using System.Collections;
    using System.Collections.Generic;
    using TestFramework;

    [Test.Types.Source(typeof(GenericArgs))]
    public class ClassGenericArgsSourceTest<T1, T2>
    {
        [Test.Args(33, "abc")]
        public void SuccessTest(int num, string str)
        {
        }
    }

    internal class GenericArgs : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[] { typeof(int), typeof(string) };
            yield return new List<object> { typeof(double), typeof(float) };
        }
    }
}
