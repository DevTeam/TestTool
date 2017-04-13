namespace DevTeam.TestFramework
{
    using System;
    using System.Collections.Generic;

    public static partial class Test
    {
        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
        public class ArgsAttribute : TestAttribute
        {
            public static readonly ArgsAttribute Empty = new ArgsAttribute();

            public ArgsAttribute(params object[] parameters)
            {
                if (parameters == null) throw new ArgumentNullException(nameof(parameters));
                Parameters = parameters;
            }

            public IEnumerable<object> Parameters { get; }
        }
    }
}