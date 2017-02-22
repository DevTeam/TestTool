namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Dto;

    internal class TestExecutor : ITestExecutor
    {
        public static readonly Uri Id = new Uri("executor://devteam/DefaultTestExecutor");
        private readonly Func<ITestExecutionContext> _testExecutionContexFactory;
        
        public TestExecutor(
            [NotNull] Func<ITestExecutionContext> testExecutionContexFactory)
        {
            if (testExecutionContexFactory == null) throw new ArgumentNullException(nameof(testExecutionContexFactory));
            _testExecutionContexFactory = testExecutionContexFactory;
        }

        public IEnumerable<ITestCaseInfo> Run(IEnumerable<ITestAssembly> testAssemblies)
        {
            if (testAssemblies == null) throw new ArgumentNullException(nameof(testAssemblies));
            foreach (var testAssembly in testAssemblies)
            {
                var testExecutionContex = _testExecutionContexFactory();
                var assemblyInfo = testExecutionContex.InitializeAssembly(testAssembly);
                try
                {
                    foreach (var testClass in testAssembly.Classes)
                    {
                        if (testClass.Assembly.TestExecutor != Id)
                        {
                            continue;
                        }

                        var typeInfo = testExecutionContex.InitializeType(testClass, assemblyInfo);
                        try
                        {
                            var instance = testExecutionContex.InitializeInstance(testClass, typeInfo);
                            try
                            {
                                foreach (var testMethod in testClass.Methods)
                                {
                                    var methodInfo = testExecutionContex.InitializeMethod(testMethod, typeInfo);
                                    try
                                    {
                                        foreach (var testCase in testMethod.Cases)
                                        {
                                            testExecutionContex.InitializeCase(testCase);
                                            try
                                            {
                                                yield return new TestCaseInfo(testCase, TestCaseState.Starting);
                                                var testResult = testExecutionContex.ExecuteTest(testCase, methodInfo, instance);
                                                yield return new TestCaseInfo(testCase, TestCaseState.Success) { Result = testResult };
                                            }
                                            finally
                                            {
                                                testExecutionContex.UninitializeCase(testCase);
                                            }
                                        }
                                    }
                                    finally
                                    {
                                        testExecutionContex.UninitializeMethod(testMethod, methodInfo);
                                    }
                                }
                            }
                            finally
                            {
                                testExecutionContex.UninitializeInstance(testClass, typeInfo, instance);
                            }
                        }
                        finally
                        {
                            testExecutionContex.UninitializeType(testClass, typeInfo);
                        }
                    }
                }
                finally
                {
                    testExecutionContex.UninitializeAssembly(testAssembly, assemblyInfo);
                }
            }
        }
    }
}
