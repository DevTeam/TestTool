namespace DevTeam.TestEngine.Dto
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Contracts;

    internal class CaseDto: ICase
    {
        public CaseDto(
            Guid id,
            [NotNull] string source,
            [NotNull] string fullTypeName,
            [NotNull] string typeName,
            [NotNull] string[] typeGenericArgs,
            [NotNull] string[] typeParameters,
            [NotNull] string methodName,
            [NotNull] string[] methodParaeters,
            [CanBeNull] string codeFilePath = null,
            [CanBeNull]int? lineNumber = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (fullTypeName == null) throw new ArgumentNullException(nameof(fullTypeName));
            if (typeName == null) throw new ArgumentNullException(nameof(typeName));
            if (typeGenericArgs == null) throw new ArgumentNullException(nameof(typeGenericArgs));
            if (typeParameters == null) throw new ArgumentNullException(nameof(typeParameters));
            if (methodName == null) throw new ArgumentNullException(nameof(methodName));
            if (methodParaeters == null) throw new ArgumentNullException(nameof(methodParaeters));
            Id = id;
            Source = source;
            CodeFilePath = codeFilePath;
            LineNumber = lineNumber;
            FullTypeName = fullTypeName;
            TypeName = typeName;
            TypeGenericArgs = typeGenericArgs;
            TypeParameters = typeParameters;
            MethodName = methodName;
            MethodParaeters = methodParaeters;
        }

        public Guid Id { get; }

        public string Source { get; }

        public string CodeFilePath { get; }

        public int? LineNumber { get; }

        public string FullTypeName { get; }

        public string TypeName { get; }

        public IEnumerable<string> TypeGenericArgs { get; }

        public IEnumerable<string> TypeParameters { get; }

        public string MethodName { get; }

        public IEnumerable<string> MethodParaeters { get; }

        [NotNull]
        public override string ToString()
        {
            return $"{Path.GetFileName(Source)}: {FullTypeName}{GetTypesString(TypeGenericArgs)}{GetParametersString(TypeParameters)}.{MethodName}{GetParametersString(MethodParaeters)}";
        }

        [NotNull]
        private static string GetParametersString([NotNull] IEnumerable<string> parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            var str = string.Join(", ", parameters.ToArray());
            return string.IsNullOrEmpty(str) ? string.Empty : $"({str})";
        }

        [NotNull]
        private static string GetTypesString([NotNull] IEnumerable<string> types)
        {
            if (types == null) throw new ArgumentNullException(nameof(types));
            var str = string.Join(", ", types.ToArray());
            return string.IsNullOrEmpty(str) ? string.Empty : $"<{str}>";
        }
    }
}
