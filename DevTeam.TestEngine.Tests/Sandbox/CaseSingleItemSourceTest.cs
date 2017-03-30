namespace DevTeam.TestEngine.Tests.Sandbox
{
    using System.Collections;
    using TestFramework;

    [Test.CaseSource(typeof(CaseSourceTestCases))]
    public class CaseSingleItemSourceTest
    {
        public CaseSingleItemSourceTest(int num)
        {
        }

        [Test.CaseSource(typeof(SuccessTestCases))]
        public void SuccessTest(int num)
        {
        }

        private class CaseSourceTestCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new object[] { 10 };
                yield return 20;
            }
        }

        private class SuccessTestCases: IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new object[] { 33, "abc"};
                yield return 44;
            }
        }
    }
}
