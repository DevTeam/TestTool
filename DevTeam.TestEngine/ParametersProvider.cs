namespace DevTeam.TestEngine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Contracts.Reflection;

    internal class ParametersProvider: IParametersProvider
    {
        private readonly IReflection _reflection;

        public ParametersProvider([NotNull] IReflection reflection)
        {
            if (reflection == null) throw new ArgumentNullException(nameof(reflection));
            _reflection = reflection;
        }

        public IEnumerable<object[]> GetTypeParameters(ITypeInfo type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return GetParameters(type.GetCustomAttributes<TestFramework.Test.CaseSourceAttribute>(), type.GetCustomAttributes<TestFramework.Test.CaseAttribute>());
        }

        public IEnumerable<object[]> GetMethodParameters(IMethodInfo method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            return GetParameters(method.GetCustomAttributes<TestFramework.Test.CaseSourceAttribute>(), method.GetCustomAttributes<TestFramework.Test.CaseAttribute>());
        }

        private IEnumerable<object[]> GetParameters(
            [NotNull] IEnumerable<TestFramework.Test.CaseSourceAttribute> caseSources,
            [NotNull] IEnumerable<TestFramework.Test.CaseAttribute> cases)
        {
            if (caseSources == null) throw new ArgumentNullException(nameof(caseSources));
            if (cases == null) throw new ArgumentNullException(nameof(cases));
            var genericArgsFromSources =
                from caseSourceAttribute in caseSources
                from caseSourceType in caseSourceAttribute.CaseSourceTypes
                let caseSourceInstance = _reflection.CreateType(caseSourceType).CreateInstance() as IEnumerable
                from parametersEnum in caseSourceInstance.OfType<IEnumerable>()
                select parametersEnum.OfType<object>().ToArray();

            var parameters =
                from caseAttribute in cases
                select caseAttribute.Parameters;

            return genericArgsFromSources.Concat(parameters);
        }
    }
}
