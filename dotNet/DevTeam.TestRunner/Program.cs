namespace DevTeam.TestRunner
{
    using System.IO;
    using System.Reflection;
    using IoC;
    using IoC.Configurations.Json;
    using IoC.Contracts;

    public class Program
    {
        public static void Main(string[] args)
        {
            using (var container = new Container("root")
                .Configure().DependsOn<JsonConfiguration>(ReadIoCConfiguration()).ToSelf()
                .Register().Contract<Program>().Autowiring(typeof(Program)).ToSelf())
            {
            }
        }

        public Program()
        {
        }

        private static string ReadIoCConfiguration()
        {
            return File.ReadAllText(Path.Combine(Path.GetDirectoryName(typeof(Program).GetTypeInfo().Assembly.Location), "DevTeam.TestEngine.ioc"));
        }
    }
}
