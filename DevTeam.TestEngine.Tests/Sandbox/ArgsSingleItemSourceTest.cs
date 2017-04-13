namespace DevTeam.TestEngine.Tests.Sandbox
{
    using System.Collections;
    using TestFramework;

    [Test.Args.Source(typeof(ClassArgs))]
    public class ArgsSingleItemSourceTest
    {
        public ArgsSingleItemSourceTest(int num)
        {
        }

        [Test.Args.Source(typeof(SuccessArgs))]
        public void SuccessTest(int num)
        {
        }

        private class ClassArgs : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new object[] { 10 };
                yield return 20;
            }
        }

        private class SuccessArgs: IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new object[] { 33 };
                yield return 44;
            }
        }
    }
}
