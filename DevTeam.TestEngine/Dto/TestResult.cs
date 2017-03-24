namespace DevTeam.TestEngine.Dto
{
    using System;
    using Contracts;

    internal class TestResult: ITestResult
    {
        [CanBeNull] private readonly Exception _exception;

        public TestResult(TestState state, [CanBeNull] Exception exception = null)
        {
            _exception = exception;
            State = state;
        }

        public TestState State { get; }

        public string Error => _exception?.Message;

        public string StackTrace => _exception?.StackTrace;
    }
}
