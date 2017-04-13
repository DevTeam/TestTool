namespace DevTeam.TestFramework
{
    using System;

    public static partial class Test
    {
        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
        public class IgnoreAttribute : TestAttribute
        {
            public IgnoreAttribute(string reason = "")
            {
                Reason = reason;
            }

            public string Reason { get; }
        }
    }
}