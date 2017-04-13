namespace DevTeam.TestFramework
{
    using System;
    using System.Collections.Generic;

    public static partial class Test
    {
        public static class Types
        {
            [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
            public class SourceAttribute : TestAttribute
            {
                public SourceAttribute(params Type[] sourceTypes)
                {
                    if (sourceTypes == null) throw new ArgumentNullException(nameof(sourceTypes));
                    Types = sourceTypes;
                }

                public IEnumerable<Type> Types { get; }
            }
        }
    }
}