namespace DevTeam.TestEngine.Tests
{
    using System;
    using System.IO;
    using System.Reflection;

    internal static class TestsExtensions
    {
        public static Assembly GetAssembly(Type type)
        {
#if !NETCOREAPP1_0
            return type.Assembly;
#else
            return type.GetTypeInfo().Assembly;
#endif
        }

        public static string GetBinDirectory()
        {
#if !NETCOREAPP1_0
            return AppDomain.CurrentDomain.BaseDirectory;
#else
            return Path.GetDirectoryName(GetAssembly(typeof(TestsExtensions)).Location);
#endif
        }

    }
}
