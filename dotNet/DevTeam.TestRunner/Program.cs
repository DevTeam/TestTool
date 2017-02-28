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
            using (var configReader = new StreamReader(typeof(Program).GetTypeInfo().Assembly.GetManifestResourceStream("DevTeam.TestRunner.DevTeam.TestEngine.json")))
            {
                return configReader.ReadToEnd();
            }
        }
    }
}
