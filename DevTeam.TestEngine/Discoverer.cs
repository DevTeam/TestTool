namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Contracts.Reflection;
    using Dto;
    using TestFramework;

    internal class Discoverer : IDiscoverer
    {
        [NotNull] private readonly IReflection _reflection;
        [NotNull] private readonly IGenericArgsProvider _genericArgsProvider;
        [NotNull] private readonly IParametersProvider _parametersProvider;

        public Discoverer(
            [NotNull] IReflection reflection,
            [NotNull] IGenericArgsProvider genericArgsProvider,
            [NotNull] IParametersProvider parametersProvider)
        {
            _reflection = reflection;
            _genericArgsProvider = genericArgsProvider;
            _parametersProvider = parametersProvider;
        }

        public IEnumerable<ITestInfo> Discover(string source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var assembly = _reflection.LoadAssembly(source);
            return
                from type in assembly.DefinedTypes
                from genericArgs in _genericArgsProvider.GetGenericArgs(type).DefaultIfEmpty(new Type[0])
                from typeParameters in _parametersProvider.GetTypeParameters(type).DefaultIfEmpty(new object[0])
                from method in type.Methods
                from methodParameters in _parametersProvider.GetMethodParameters(method).Concat(method.GetCustomAttributes<TestAttribute>().Select(i => new object[0]))
                from testCase in CreateTestInfo(source, assembly, type, genericArgs, typeParameters, method, methodParameters)
                select testCase;
        }

        private IEnumerable<ITestInfo> CreateTestInfo(
            [NotNull] string source,
            [NotNull] IAssemblyInfo assembly,
            [NotNull] ITypeInfo type,
            [NotNull] Type[] genericArgs,
            [NotNull] object[] typeParameters,
            [NotNull] IMethodInfo method,
            [NotNull] object[] methodParameters)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (genericArgs == null) throw new ArgumentNullException(nameof(genericArgs));
            if (typeParameters == null) throw new ArgumentNullException(nameof(typeParameters));
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (methodParameters == null) throw new ArgumentNullException(nameof(methodParameters));
            var testCase = new CaseDto(
                Guid.NewGuid(),
                source,
                type.FullName,
                type.Name,
                genericArgs.Select(i => i.Name).ToArray(),
                typeParameters.Select(i => i.ToString()).ToArray(),
                method.Name,
                methodParameters.Select(i => i.ToString()).ToArray());

            yield return (ITestInfo)new TestInfo(testCase, assembly, type, genericArgs, typeParameters, method, methodParameters);
        }
    }
}
