namespace DevTeam.TestEngine.Tests.Sandbox
{
    using System.Collections;
    using System.Collections.Generic;
    using TestFramework;

    [Test.Args.Source(typeof(ClassArgs))]
    public class ArgsSourceTest
    {
        public ArgsSourceTest(int num)
        {
        }

        [Test.Args.Source(typeof(SuccessArgs))]
        public void SuccessTest(int num, string str)
        {
        }

        private class ClassArgs : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new object[] { 10 };
                yield return new List<object> { 20 };
            }
        }

        private class SuccessArgs: IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new object[] { 33, "abc"};
                yield return new List<object> { 44, "xyz" };
            }
        }
    }
}
