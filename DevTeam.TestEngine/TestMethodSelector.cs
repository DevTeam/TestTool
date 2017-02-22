namespace DevTeam.TestEngine
{
    using System.Linq;
    using Contracts;
    using Contracts.Reflection;

    internal class TestMethodSelector : ITestMethodSelector
    {
        public IMethodInfo SelectMethod(ITypeInfo typeInfo, ITestMethod testMethod)
        {
            return typeInfo.Methods.Single(i => i.Name == testMethod.Name);
        }
    }
}
