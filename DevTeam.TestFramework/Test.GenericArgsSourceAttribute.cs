namespace DevTeam.TestFramework
{
    using System;
    using System.Collections.Generic;

    public partial class Test
    {
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
        public class GenericArgsSourceAttribute : Attribute
        {
            public GenericArgsSourceAttribute(params Type[] genericArgsSourceTypes)
            {
                if (genericArgsSourceTypes == null) throw new ArgumentNullException(nameof(genericArgsSourceTypes));
                Types = genericArgsSourceTypes;
            }

            public IEnumerable<Type> Types { get; }
        }
    }
}