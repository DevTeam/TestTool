﻿namespace DevTeam.TestFramework
{
    using System;

    public partial class Test
    {
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