namespace DevTeam.TestRunner
{
    using System;
    using System.IO;
    using System.Linq;
    using IoC.Contracts;
    using IoC.Configurations.Json;
    using IoC;
    using TestEngine.Contracts;

#if !NET35 && !NET40
    using System.Reflection;
#endif

    public class Program
    {
        private readonly ISession _testSession;

        public static int Main(string[] args)
        {
            //Debugger.Launch();
            using (var container = new Container("root")
                .Configure().DependsOn<JsonConfiguration>(ReadIoCConfiguration()).ToSelf()
                .Register().Autowiring<Program, Program>().ToSelf())
            {
                return container.Resolve().Instance<Program>().Run(args);
            }
        }

        public Program(ISession testSession)
        {
            _testSession = testSession;
        }

        public int Run(string[] args)
        {
            var tests =
                from source in args
                from testCase in _testSession.Discover(source)
                select new { testCase, result = _testSession.Run(testCase.Id)};

            foreach (var test in tests)
            {
                Console.WriteLine($"{test.testCase} - {test.result.State}");
            }

            return 0;
        }

        private static string ReadIoCConfiguration()
        {
            return File.ReadAllText(Path.Combine(GetBinDirectory(), "DevTeam.TestEngine.ioc"));
        }

        private static string GetBinDirectory()
        {
#if NET35 || NET40
            return AppDomain.CurrentDomain.BaseDirectory;
#else
            return Path.GetDirectoryName(typeof(Program).GetTypeInfo().Assembly.Location);
#endif
        }
    }
}
