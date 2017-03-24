using System;

namespace DevTeam.TestFramework
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class TestAttribute : Attribute
    {
    }

    public class Test
    {
        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
        public class CaseAttribute : Attribute
        {
            public static readonly CaseAttribute Empty = new CaseAttribute();

            public CaseAttribute(params object[] parameters)
            {
                if (parameters == null) throw new ArgumentNullException(nameof(parameters));
                Parameters = parameters;
            }

            public object[] Parameters { get; }
        }

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

        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
        public class GenericArgsAttribute : Attribute
        {
            public GenericArgsAttribute(params Type[] types)
            {
                if (types == null) throw new ArgumentNullException(nameof(types));
                Types = types;
            }

            public Type[] Types { get; }
        }

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

        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
        public class IgnoreAttribute : Attribute
        {
            public IgnoreAttribute(string reason = "")
            {
                Reason = reason;
            }

            public string Reason { get; }
        }
    }
}
