namespace DevTeam.TestEngine.Tests.Sandbox
{
    using System.Collections;
    using System.Collections.Generic;
    using TestFramework;

    [Test.CaseSource(typeof(CaseSourceTestCases))]
    public class CaseSourceTest
    {
        public CaseSourceTest(int num, int c)
        {
        }

        [Test.CaseSource(typeof(SuccessTestCases))]
        public void SuccessTest(int num, string str)
        {
        }

        private class CaseSourceTestCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new object[] { 10 };
                yield return new List<object> { 20 };
            }
        }

        private class SuccessTestCases: IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new object[] { 33, "abc"};
                yield return new List<object> { 44, "xyz" };
            }
        }
    }
}
