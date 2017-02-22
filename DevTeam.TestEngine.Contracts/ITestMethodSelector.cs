namespace DevTeam.TestEngine.Contracts
{
    using Reflection;

    public interface ITestMethodSelector
    {
        IMethodInfo SelectMethod(ITypeInfo typeInfo, ITestMethod testMethod);
    }
}
