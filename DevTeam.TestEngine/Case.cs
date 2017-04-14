﻿namespace DevTeam.TestEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;

    internal class Case: ICase
    {
        [NotNull] private readonly IDisplayNameFactory _displayNameFactory;
        [NotNull] private readonly ITestInfo _testInfo;

        public Case(
            [NotNull] IDisplayNameFactory displayNameFactory,
            [NotNull] ITestInfo testInfo)
        {
            _displayNameFactory = displayNameFactory;
            _testInfo = testInfo;
            if (displayNameFactory == null) throw new ArgumentNullException(nameof(displayNameFactory));
            if (testInfo == null) throw new ArgumentNullException(nameof(testInfo));
            Id = Guid.NewGuid();
            Source = testInfo.Source;
            CodeFilePath = string.Empty;
            LineNumber = null;
            FullTypeName = testInfo.Type.FullName;
            TypeName = testInfo.Type.Name;
            TypeGenericArgs = testInfo.TypeGenericArgs.Select(i => i.Name);
            TypeArgs = testInfo.TypeArgs.Select(i => i.ToString());
            MethodName = testInfo.Method.Name;
            MethodGenericArgs = testInfo.MethodGenericArgs.Select(i => i.Name);
            MethodArgs = testInfo.MethodArgs.Select(i => i.ToString());
        }

        public Guid Id { get; }

        public string Source { get; }

        public string DisplayName => _displayNameFactory.CreateDisplayName(_testInfo);

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
            return DisplayName;
        }
    }
}
