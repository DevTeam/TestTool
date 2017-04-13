namespace DevTeam.TestFramework
{
    using System;
    using System.Collections.Generic;

    public partial class Test
    {
        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
        public class CaseSourceAttribute : Attribute
        {
            public CaseSourceAttribute(params Type[] caseSourceTypes)
            {
                if (caseSourceTypes == null) throw new ArgumentNullException(nameof(caseSourceTypes));
                Types = caseSourceTypes;
            }

            public IEnumerable<Type> Types { get; }
        }
    }
}