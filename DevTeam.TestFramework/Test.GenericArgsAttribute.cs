namespace DevTeam.TestFramework
{
    using System;

    public partial class Test
    {
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
    }
}