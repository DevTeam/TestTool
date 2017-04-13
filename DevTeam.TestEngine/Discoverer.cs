namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Contracts.Reflection;
    using Dto;

    internal class Discoverer : IDiscoverer
    {
        [NotNull] private readonly IReflection _reflection;
        [NotNull] private readonly IAttributeMap _attributeMap;
        [NotNull] private readonly IAttributeAccessor _attributeAccessor;
        [NotNull] private readonly IGenericArgsProvider _genericArgsProvider;
        [NotNull] private readonly IParametersProvider _parametersProvider;

        public Discoverer(
            [NotNull] IReflection reflection,
            [NotNull] IAttributeMap attributeMap,
            [NotNull] IAttributeAccessor attributeAccessor,
            [NotNull] IGenericArgsProvider genericArgsProvider,
            [NotNull] IParametersProvider parametersProvider)
        {
            if (reflection == null) throw new ArgumentNullException(nameof(reflection));
            if (attributeMap == null) throw new ArgumentNullException(nameof(attributeMap));
            if (attributeAccessor == null) throw new ArgumentNullException(nameof(attributeAccessor));
            if (genericArgsProvider == null) throw new ArgumentNullException(nameof(genericArgsProvider));
            if (parametersProvider == null) throw new ArgumentNullException(nameof(parametersProvider));
            _reflection = reflection;
            _attributeMap = attributeMap;
            _attributeAccessor = attributeAccessor;
            _genericArgsProvider = genericArgsProvider;
            _parametersProvider = parametersProvider;
        }

        public IEnumerable<ITestInfo> Discover(string source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var assembly = _reflection.LoadAssembly(source);
            return
                from type in assembly.DefinedTypes
                from genericArgs in _genericArgsProvider.GetGenericArgs(type).DefaultIfEmpty(Enumerable.Empty<Type>())
                from typeParameters in _parametersProvider.GetTypeParameters(type).DefaultIfEmpty(Enumerable.Empty<object>())
                from method in type.Methods
                from methodParameters in _parametersProvider.GetMethodParameters(method).Concat(_attributeAccessor.GetAttributes(method, _attributeMap.GetDescriptor(Wellknown.Attributes.Test)).Select(i => Enumerable.Empty<object>()))
                from testCase in CreateTestInfo(source, assembly, type, genericArgs, typeParameters, method, methodParameters)
                select testCase;
        }

        private IEnumerable<ITestInfo> CreateTestInfo(
            [NotNull] string source,
            [NotNull] IAssemblyInfo assembly,
            [NotNull] ITypeInfo type,
            [NotNull] IEnumerable<Type> genericArgs,
            [NotNull] IEnumerable<object> typeParameters,
            [NotNull] IMethodInfo method,
            [NotNull] IEnumerable<object> methodParameters)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (genericArgs == null) throw new ArgumentNullException(nameof(genericArgs));
            if (typeParameters == null) throw new ArgumentNullException(nameof(typeParameters));
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (methodParameters == null) throw new ArgumentNullException(nameof(methodParameters));

            var genericArgsArray = genericArgs as Type[] ?? genericArgs.ToArray();
            var typeParametersArray = typeParameters as object[] ?? typeParameters.ToArray();
            var methodParametersArray = methodParameters as object[] ?? methodParameters.ToArray();

            var testCase = new CaseDto(
                Guid.NewGuid(),
                source,
                type.FullName,
                type.Name,
                genericArgsArray.Select(i => i.Name).ToArray(),
                typeParametersArray.Select(i => i.ToString()).ToArray(),
                method.Name,
                methodParametersArray.Select(i => i.ToString()).ToArray());

            // method.GetCustomAttributes<Test.IgnoreAttribute>().SingleOrDefault() ?? type.GetCustomAttributes<Test.IgnoreAttribute>().SingleOrDefault();

            yield return new TestInfo(
                testCase,
                assembly,
                type,
                genericArgsArray,
                typeParametersArray,
                method,
                methodParametersArray);
        }
    }
}
