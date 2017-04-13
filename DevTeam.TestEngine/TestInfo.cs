namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Contracts.Reflection;

    internal class TestInfo: ITestInfo
    {
        public TestInfo(
            [NotNull] ICase testCase,
            [NotNull] IAssemblyInfo assembly,
            [NotNull] ITypeInfo type,
            [NotNull] IEnumerable<Type> genericArgs,
            [NotNull] IEnumerable<object> typeParameters,
            [NotNull] IMethodInfo method,
            [NotNull] IEnumerable<object> methodParameters)
        {
            if (testCase == null) throw new ArgumentNullException(nameof(testCase));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (genericArgs == null) throw new ArgumentNullException(nameof(genericArgs));
            if (typeParameters == null) throw new ArgumentNullException(nameof(typeParameters));
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (methodParameters == null) throw new ArgumentNullException(nameof(methodParameters));
            TestCase = testCase;
            Assembly = assembly;
            Type = type;
            GenericArgs = genericArgs;
            TypeParameters = typeParameters;
            Method = method;
            MethodParameters = methodParameters;
        }

        public ICase TestCase { [NotNull] get; }

        public IAssemblyInfo Assembly { [NotNull] get; }

        public ITypeInfo Type { [NotNull] get; }

        public IEnumerable<Type> GenericArgs { [NotNull] get; }

        public IEnumerable<object> TypeParameters { [NotNull] get; }

        public IMethodInfo Method { [NotNull] get; }

        public IEnumerable<object> MethodParameters { [NotNull] get; }
    }
}
