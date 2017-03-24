using System;

namespace DevTeam.TestFramework
{
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

        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
        public class GenericArgsAttribute : Attribute
        {
            public static readonly GenericArgsAttribute Empty = new GenericArgsAttribute();

            public GenericArgsAttribute(params Type[] types)
            {
                if (types == null) throw new ArgumentNullException(nameof(types));
                Types = types;
            }

            public Type[] Types { get; }
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
