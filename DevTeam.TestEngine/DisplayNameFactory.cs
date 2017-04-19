namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Contracts;
    using Contracts.Reflection;

    internal class DisplayNameFactory : IDisplayNameFactory
    {
        // ReSharper disable StringLiteralTypo
        private static readonly Dictionary<Type, string> PrimitiveTypes = new Dictionary<Type, string>
        {
            {typeof(byte), "byte"},
            {typeof(sbyte), "sbyte"},
            {typeof(int), "int"},
            {typeof(uint), "uint"},
            {typeof(short), "short"},
            {typeof(ushort), "ushort"},
            {typeof(long), "long"},
            {typeof(ulong), "ulong"},
            {typeof(float), "float"},
            {typeof(double), "double"},
            {typeof(char), "char"},
            {typeof(object), "object"},
            {typeof(string), "string"},
            {typeof(decimal), "decimal"}
        };

        public string CreateDisplayName(ITestInfo testInfo)
        {
            return $"{GetTypeName(testInfo.Type)}{GetArgString(testInfo.TypeArgs)}.{testInfo.Method.Name}{GetGenericArgsString(testInfo.Method.GenericArguments)}{GetMethodParamsString(testInfo.Method.Parameters, testInfo.MethodArgs)}";
        }

        [NotNull]
        private static string GetTypeName([NotNull] ITypeInfo typeInfo)
        {
            var name = new StringBuilder();
            if (PrimitiveTypes.TryGetValue(typeInfo.Type, out string shortName))
            {
                name.Append(shortName);
            }
            else
            {
                name.Append(typeInfo.Namespace);
                name.Append('.');
                foreach (var ch in typeInfo.Name.TakeWhile(ch => ch != '`'))
                {
                    name.Append(ch);
                }
            }

            if (typeInfo.IsGenericType)
            {
                name.Append(GetGenericArgsString(typeInfo.GenericArguments));
            }

            return name.ToString();
        }

        [NotNull]
        private static string GetMethodParamsString([NotNull] IEnumerable<IParameterInfo> parameters, [NotNull] IEnumerable<object> args)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (args == null) throw new ArgumentNullException(nameof(args));

            var paramsArray = parameters.ToArray();
            var argsArray = args.ToArray();
            var str = new StringBuilder();
            for (var i = 0; i < paramsArray.Length && i < argsArray.Length; i++)
            {
                if (i > 0)
                {
                    str.Append(", ");
                }
                str.Append(paramsArray[i].Name);
                str.Append(": ");
                str.Append(GetValueString(argsArray[i]));
            }
            
            return str.Length == 0 ? string.Empty : $"({str})";
        }

        [NotNull]
        private static string GetArgString([NotNull] IEnumerable<object> args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            var str = string.Join(", ", args.Select(GetValueString).ToArray());
            return string.IsNullOrEmpty(str) ? string.Empty : $"({str})";
        }

        private static string GetValueString(object value)
        {
            if (value == null)
            {
                return "null";
            }

            if (value is string)
            {
                return $"\"{value}\"";
            }

            if (value is char)
            {
                return $"'{value}'";
            }

            return value.ToString();
        }

        [NotNull]
        private static string GetGenericArgsString([NotNull] IEnumerable<ITypeInfo> types)
        {
            if (types == null) throw new ArgumentNullException(nameof(types));
            var str = string.Join(", ", types.Select(GetTypeName).ToArray());
            return string.IsNullOrEmpty(str) ? string.Empty : $"<{str}>";
        }
    }
}
