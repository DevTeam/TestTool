using System;

namespace DevTeam.TestFramework
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
}
