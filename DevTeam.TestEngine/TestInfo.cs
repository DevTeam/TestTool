namespace DevTeam.TestEngine
{
    using System;
    using Contracts;
    using Contracts.Reflection;
    using TestFramework;

    internal class TestInfo
    {
        public TestInfo(
            [NotNull] IAssemblyInfo assembly,
            [NotNull] ITypeInfo type,
            [NotNull] Test.GenericArgsAttribute typeGenericArgsAttribute,
            [NotNull] Test.CaseAttribute typeCaseAttribute,
            [NotNull] IMethodInfo method,
            [NotNull] Test.CaseAttribute methodCaseAttribute)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (typeGenericArgsAttribute == null) throw new ArgumentNullException(nameof(typeGenericArgsAttribute));
            if (typeCaseAttribute == null) throw new ArgumentNullException(nameof(typeCaseAttribute));
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (methodCaseAttribute == null) throw new ArgumentNullException(nameof(methodCaseAttribute));
            Assembly = assembly;
            Type = type;
            TypeGenericArgsAttribute = typeGenericArgsAttribute;
            TypeCaseAttribute = typeCaseAttribute;
            Method = method;
            MethodCaseAttribute = methodCaseAttribute;
        }

        public IAssemblyInfo Assembly { [NotNull] get; }

        public ITypeInfo Type { [NotNull] get; }

        public Test.GenericArgsAttribute TypeGenericArgsAttribute { [NotNull] get; }

        public Test.CaseAttribute TypeCaseAttribute { [NotNull] get; }

        public IMethodInfo Method { [NotNull] get; }

        public Test.CaseAttribute MethodCaseAttribute { [NotNull] get; }
    }
}
