namespace DevTeam.TestFramework
{
    using System;

    public partial class Test
    {
        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
        public class CaseSourceAttribute : Attribute
        {
            public CaseSourceAttribute(params Type[] caseSourceTypes)
            {
                if (caseSourceTypes == null) throw new ArgumentNullException(nameof(caseSourceTypes));
                CaseSourceTypes = caseSourceTypes;
            }

            public Type[] CaseSourceTypes { get; }
        }
    }
}