using System;

namespace DevTeam.TestFramework
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class TestAttribute : Attribute
    {
        public static readonly TestAttribute Empty = new TestAttribute();

        public TestAttribute(params object[] parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            Parameters = parameters;
        }

        public object[] Parameters { get; }
    }
}
