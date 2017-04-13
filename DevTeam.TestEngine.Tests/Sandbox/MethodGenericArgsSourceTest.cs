namespace DevTeam.TestEngine.Tests.Sandbox
{
    using System.Collections;
    using System.Collections.Generic;
    using TestFramework;

    public class MethodGenericArgsSourceTest
    {
        [Test.Types.Source(typeof(GenericArgsSource))]
        [Test.Types(typeof(string), typeof(double))]
        public void SuccessTest<T1, T2>()
        {
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
}
