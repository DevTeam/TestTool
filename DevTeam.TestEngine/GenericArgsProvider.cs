namespace DevTeam.TestEngine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Contracts.Reflection;

    internal class GenericArgsProvider: IGenericArgsProvider
    {
        private readonly IReflection _reflection;

        public GenericArgsProvider([NotNull] IReflection reflection)
        {
            if (reflection == null) throw new ArgumentNullException(nameof(reflection));
            _reflection = reflection;
        }

        public IEnumerable<Type[]> GetGenericArgs(ITypeInfo type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            var genericArgsFromSources =
                from genericArgsSourceAttribute in type.GetCustomAttributes<TestFramework.Test.GenericArgsSourceAttribute>()
                from genericArgsSourceType in genericArgsSourceAttribute.GenericArgsSourceTypes
                let genericArgsSourceInstance = _reflection.CreateType(genericArgsSourceType).CreateInstance() as IEnumerable
                from genericArgsEnum in genericArgsSourceInstance.OfType<IEnumerable>()
                select genericArgsEnum.OfType<Type>().ToArray();

            var genericArgs =
                from genericArgsAtr in type.GetCustomAttributes<TestFramework.Test.GenericArgsAttribute>()
                select genericArgsAtr.Types;

            return genericArgsFromSources.Concat(genericArgs);
        }
    }
}
