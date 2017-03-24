namespace DevTeam.TestEngine.Dto
{
    using System;
    using System.IO;
    using Contracts;

    internal class CaseDto: ICase
    {
        public CaseDto(
            Guid id,
            [NotNull] string source,
            [NotNull] string fullTypeName,
            [NotNull] string typeName,
            [NotNull] string[] typeParameters,
            [NotNull] string methodName,
            [NotNull] string[] methodParaeters,
            [CanBeNull] string codeFilePath = null,
            [CanBeNull]int? lineNumber = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (fullTypeName == null) throw new ArgumentNullException(nameof(fullTypeName));
            if (typeName == null) throw new ArgumentNullException(nameof(typeName));
            if (typeParameters == null) throw new ArgumentNullException(nameof(typeParameters));
            if (methodName == null) throw new ArgumentNullException(nameof(methodName));
            if (methodParaeters == null) throw new ArgumentNullException(nameof(methodParaeters));
            Id = id;
            Source = source;
            CodeFilePath = codeFilePath;
            LineNumber = lineNumber;
            FullTypeName = fullTypeName;
            TypeName = typeName;
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

        public string[] TypeParameters { get; }

        public string MethodName { get; }

        public string[] MethodParaeters { get; }

        [NotNull]
        public override string ToString()
        {
            return $"{Path.GetFileName(Source)}: {FullTypeName}{GetParametersString(TypeParameters)}.{MethodName}{GetParametersString(MethodParaeters)}";
        }

        [NotNull]
        private static string GetParametersString([NotNull] string[] parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (parameters.Length == 0)
            {
                return string.Empty;
            }

            return $"({string.Join(", ", parameters)})";
        }
    }
}
