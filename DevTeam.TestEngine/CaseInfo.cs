namespace DevTeam.TestEngine
{
    using System;
    using Contracts;
    using Contracts.Reflection;
    using TestFramework;

    internal class CaseInfo
    {
        public CaseInfo(
            [NotNull] IAssemblyInfo assembly,
            [NotNull] ITypeInfo type,
            [NotNull] CaseAttribute typeAttribute,
            [NotNull] IMethodInfo method,
            [NotNull] CaseAttribute methodAttribute)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (typeAttribute == null) throw new ArgumentNullException(nameof(typeAttribute));
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (methodAttribute == null) throw new ArgumentNullException(nameof(methodAttribute));
            Assembly = assembly;
            Type = type;
            TypeAttribute = typeAttribute;
            Method = method;
            MethodAttribute = methodAttribute;
        }

        public IAssemblyInfo Assembly { [NotNull] get; }

        public ITypeInfo Type { [NotNull] get; }

        public CaseAttribute TypeAttribute { [NotNull] get; }

        public IMethodInfo Method { [NotNull] get; }

        public CaseAttribute MethodAttribute { [NotNull] get; }
    }
}
