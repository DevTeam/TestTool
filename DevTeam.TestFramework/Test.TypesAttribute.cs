namespace DevTeam.TestFramework
{
    using System;
    using System.Collections.Generic;

    public static partial class Test
    {
        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
        public class TypesAttribute : TestAttribute
        {
            public TypesAttribute(params Type[] types)
            {
                if (types == null) throw new ArgumentNullException(nameof(types));
                Types = types;
            }

            public IEnumerable<Type> Types { get; }
        }
    }
}