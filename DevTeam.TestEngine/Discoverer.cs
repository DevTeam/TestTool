namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Contracts.Reflection;

    internal class Discoverer : IDiscoverer
    {
        [NotNull] private readonly IReflection _reflection;
        [NotNull] private readonly IAttributeMap _attributeMap;
        [NotNull] private readonly IAttributeAccessor _attributeAccessor;
        [NotNull] private readonly IGenericArgsProvider _genericArgsProvider;
        [NotNull] private readonly IArgsProvider _argsProvider;
        [NotNull] private readonly Func<ITestInfo, ICase> _caseFactory;

        public Discoverer(
            [NotNull] IReflection reflection,
            [NotNull] IAttributeMap attributeMap,
            [NotNull] IAttributeAccessor attributeAccessor,
            [NotNull] IGenericArgsProvider genericArgsProvider,
            [NotNull] IArgsProvider argsProvider,
            [NotNull] Func<ITestInfo, ICase> caseFactory)
        {
            if (reflection == null) throw new ArgumentNullException(nameof(reflection));
            if (attributeMap == null) throw new ArgumentNullException(nameof(attributeMap));
            if (attributeAccessor == null) throw new ArgumentNullException(nameof(attributeAccessor));
            if (genericArgsProvider == null) throw new ArgumentNullException(nameof(genericArgsProvider));
            if (argsProvider == null) throw new ArgumentNullException(nameof(argsProvider));
            if (caseFactory == null) throw new ArgumentNullException(nameof(caseFactory));
            _reflection = reflection;
            _attributeMap = attributeMap;
            _attributeAccessor = attributeAccessor;
            _genericArgsProvider = genericArgsProvider;
            _argsProvider = argsProvider;
            _caseFactory = caseFactory;
        }

        public IEnumerable<ITestInfo> Discover(string source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var assembly = _reflection.LoadAssembly(source);
            return
                from type in assembly.DefinedTypes
                from typeGenericArgs in _genericArgsProvider.GetGenericArgs(type).DefaultIfEmpty(Enumerable.Empty<Type>())
                let typeGenericArgsArray = typeGenericArgs.ToArray()
                let caseType = DefineType(type, typeGenericArgsArray)
                from typeArgs in _argsProvider.GetTypeParameters(type).DefaultIfEmpty(Enumerable.Empty<object>())
                from method in caseType.Methods
                from methodGenericArgs in _genericArgsProvider.GetGenericArgs(method).DefaultIfEmpty(Enumerable.Empty<Type>())
                let methodGenericArgsArray = methodGenericArgs.ToArray()
                let caseMethod = DefineMethod(method, methodGenericArgsArray)
                where _attributeAccessor.GetAttributes(caseMethod, _attributeMap.GetDescriptor(Wellknown.Attributes.Test)).Any()
                from methodArgs in _argsProvider.GetMethodParameters(caseMethod).DefaultIfEmpty(Enumerable.Empty<object>())
                from testCase in CreateTestInfo(source, assembly, caseType, typeGenericArgsArray, typeArgs, caseMethod, methodGenericArgsArray, methodArgs)
                select testCase;
        }

        private IMethodInfo DefineMethod([NotNull] IMethodInfo method, [NotNull] Type[] genericArgs)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (genericArgs == null) throw new ArgumentNullException(nameof(genericArgs));
            return method.IsGenericMethodDefinition ? method.MakeGenericMethod(genericArgs) : method;
        }

        private static ITypeInfo DefineType([NotNull] ITypeInfo type, [NotNull] Type[] genericArgs)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (genericArgs == null) throw new ArgumentNullException(nameof(genericArgs));
            return type.IsGenericTypeDefinition ? type.MakeGenericType(genericArgs) : type;
        }

        private IEnumerable<ITestInfo> CreateTestInfo(
            [NotNull] string source,
            [NotNull] IAssemblyInfo assembly,
            [NotNull] ITypeInfo type,
            [NotNull] IEnumerable<Type> typeGenericArgs,
            [NotNull] IEnumerable<object> typeArgs,
            [NotNull] IMethodInfo method,
            [NotNull] IEnumerable<Type> methodGenericArgs,
            [NotNull] IEnumerable<object> methodArgs)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (typeGenericArgs == null) throw new ArgumentNullException(nameof(typeGenericArgs));
            if (typeArgs == null) throw new ArgumentNullException(nameof(typeArgs));
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (methodGenericArgs == null) throw new ArgumentNullException(nameof(methodGenericArgs));
            if (methodArgs == null) throw new ArgumentNullException(nameof(methodArgs));

            var ignoreAttribute = _attributeAccessor.GetAttributes(method, _attributeMap.GetDescriptor(Wellknown.Attributes.Ignore)).SingleOrDefault() ?? _attributeAccessor.GetAttributes(type, _attributeMap.GetDescriptor(Wellknown.Attributes.Ignore)).SingleOrDefault();
            var ignoreReason = ignoreAttribute?.GetValue<string>(_attributeMap.GetDescriptor(Wellknown.Properties.Reason)) ?? string.Empty;

            yield return new TestInfo(
                _caseFactory,
                source,
                assembly,
                type,
                typeGenericArgs,
                typeArgs,
                method,
                methodGenericArgs,
                methodArgs,
                ignoreAttribute != null,
                ignoreReason);
        }
    }
}
