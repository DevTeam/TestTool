namespace DevTeam.TestFramework
{
    using System;

    public partial class Test
    {
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
        public class GenericArgsSourceAttribute : Attribute
        {
            public GenericArgsSourceAttribute(params Type[] genericArgsSourceTypes)
            {
                if (genericArgsSourceTypes == null) throw new ArgumentNullException(nameof(genericArgsSourceTypes));
                GenericArgsSourceTypes = genericArgsSourceTypes;
            }

            public Type[] GenericArgsSourceTypes { get; }
        }
    }
}