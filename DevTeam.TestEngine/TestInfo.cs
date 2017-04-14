﻿namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Contracts.Reflection;

    internal class TestInfo: ITestInfo
    {
        [NotNull] private readonly Func<ITestInfo, ICase> _caseFactory;
        private ICase _case;

        public TestInfo(
            [NotNull] Func<ITestInfo, ICase> caseFactory,
            [NotNull] string source,
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
            _caseFactory = caseFactory;
            if (caseFactory == null) throw new ArgumentNullException(nameof(caseFactory));
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (typeGenericArgs == null) throw new ArgumentNullException(nameof(typeGenericArgs));
            if (typeArgs == null) throw new ArgumentNullException(nameof(typeArgs));
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (methodGenericArgs == null) throw new ArgumentNullException(nameof(methodGenericArgs));
            if (methodArgs == null) throw new ArgumentNullException(nameof(methodArgs));
            Source = source;
            Assembly = assembly;
            Type = type;
            TypeGenericArgs = typeGenericArgs.ToArray();
            TypeArgs = typeArgs.ToArray();
            Method = method;
            MethodGenericArgs = methodGenericArgs.ToArray();
            MethodArgs = methodArgs.ToArray();
            Ignore = ignore;
            IgnoreReason = ignoreReason;
        }

        public ICase Case => _case = _case ?? _caseFactory(this);

        public string Source { [NotNull] get; }

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
