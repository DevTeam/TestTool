namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;

    internal class Case: ICase
    {
        public Case(
            Guid id,
            [NotNull] string source,
            [NotNull] string fullTypeName,
            [NotNull] string typeName,
            [NotNull] IEnumerable<string> typeGenericArgs,
            [NotNull] IEnumerable<string> typeArgs,
            [NotNull] string methodName,
            [NotNull] IEnumerable<string> methodGenericArgs,
            [NotNull] IEnumerable<string> methodArgs,
            [CanBeNull] string codeFilePath = null,
            [CanBeNull] int? lineNumber = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (fullTypeName == null) throw new ArgumentNullException(nameof(fullTypeName));
            if (typeName == null) throw new ArgumentNullException(nameof(typeName));
            if (typeGenericArgs == null) throw new ArgumentNullException(nameof(typeGenericArgs));
            if (typeArgs == null) throw new ArgumentNullException(nameof(typeArgs));
            if (methodName == null) throw new ArgumentNullException(nameof(methodName));
            if (methodGenericArgs == null) throw new ArgumentNullException(nameof(methodGenericArgs));
            if (methodArgs == null) throw new ArgumentNullException(nameof(methodArgs));
            Id = id;
            Source = source;
            CodeFilePath = codeFilePath;
            LineNumber = lineNumber;
            FullTypeName = fullTypeName;
            TypeName = typeName;
            TypeGenericArgs = typeGenericArgs;
            TypeArgs = typeArgs;
            MethodName = methodName;
            MethodGenericArgs = methodGenericArgs;
            MethodArgs = methodArgs;
        }

        public Guid Id { get; }

        public string Source { get; }

        public string DysplayName => ToString();

        public string CodeFilePath { get; }

        public int? LineNumber { get; }

        public string FullTypeName { get; }

        public string TypeName { get; }

        public IEnumerable<string> TypeGenericArgs { get; }

        public IEnumerable<string> TypeArgs { get; }

        public string MethodName { get; }

        public IEnumerable<string> MethodGenericArgs { get; }

        public IEnumerable<string> MethodArgs { get; }

        [NotNull]
        public override string ToString()
        {
            return $"{Source}: {FullTypeName}{GetGenericArgsString(TypeGenericArgs)}{GetArgString(TypeArgs)}.{MethodName}{GetGenericArgsString(MethodGenericArgs)}{GetArgString(MethodArgs)}";
        }

        [NotNull]
        private static string GetArgString([NotNull] IEnumerable<string> parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            var str = string.Join(", ", parameters.ToArray());
            return string.IsNullOrEmpty(str) ? string.Empty : $"({str})";
        }

        [NotNull]
        private static string GetGenericArgsString([NotNull] IEnumerable<string> types)
        {
            if (types == null) throw new ArgumentNullException(nameof(types));
            var str = string.Join(", ", types.ToArray());
            return string.IsNullOrEmpty(str) ? string.Empty : $"<{str}>";
        }
    }
}
