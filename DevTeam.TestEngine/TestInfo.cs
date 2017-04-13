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
            [NotNull] IEnumerable<Type> typeGenericArgs,
            [NotNull] IEnumerable<object> typeArgs,
            [NotNull] IMethodInfo method,
            [NotNull] IEnumerable<Type> methodGenericArgs,
            [NotNull] IEnumerable<object> methodArgs,
            bool ignore,
            [NotNull] string ignoreReason)
        {
            if (testCase == null) throw new ArgumentNullException(nameof(testCase));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (typeGenericArgs == null) throw new ArgumentNullException(nameof(typeGenericArgs));
            if (typeArgs == null) throw new ArgumentNullException(nameof(typeArgs));
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (methodGenericArgs == null) throw new ArgumentNullException(nameof(methodGenericArgs));
            if (methodArgs == null) throw new ArgumentNullException(nameof(methodArgs));
            Case = testCase;
            Assembly = assembly;
            Type = type;
            TypeGenericArgs = typeGenericArgs;
            TypeArgs = typeArgs;
            Method = method;
            MethodGenericArgs = methodGenericArgs;
            MethodArgs = methodArgs;
            Ignore = ignore;
            IgnoreReason = ignoreReason;
        }

        public ICase Case { get; }

        public IAssemblyInfo Assembly { get; }

        public ITypeInfo Type { get; }

        public IEnumerable<Type> TypeGenericArgs { get; }

        public IEnumerable<object> TypeArgs { get; }

        public IMethodInfo Method { [NotNull] get; }

        public IEnumerable<Type> MethodGenericArgs { get; }

        public IEnumerable<object> MethodArgs { get; }

        public bool Ignore { get; }

        public string IgnoreReason { get; }
    }
}
