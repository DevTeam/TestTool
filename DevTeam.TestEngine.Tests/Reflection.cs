﻿namespace DevTeam.TestEngine.Tests
{
    using System;
    using System.Reflection;
    using Contracts;
    using Contracts.Reflection;

    internal class Reflection: IReflection
    {
        private readonly Func<Assembly, IAssemblyInfo> _assemblyInfoFactory;

        public Reflection([NotNull] Func<Assembly, IAssemblyInfo> assemblyInfoFactory)
        {
            if (assemblyInfoFactory == null) throw new ArgumentNullException(nameof(assemblyInfoFactory));
            _assemblyInfoFactory = assemblyInfoFactory;
        }

        public IAssemblyInfo LoadAssembly(string source)
        {
            return _assemblyInfoFactory(Assembly.LoadFile(source));
        }
    }
}