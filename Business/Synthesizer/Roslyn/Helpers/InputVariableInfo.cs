using System;
using System.Diagnostics;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Roslyn.Helpers
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal class InputVariableInfo
    {
        public string NameCamelCase { get; }
        public DimensionEnum DimensionEnum { get; }
        public int ListIndex { get; }
        public double? DefaultValue { get; }
        public double? Value { get; }

        public InputVariableInfo(string nameCamelCase, DimensionEnum dimensionEnum, int listIndex, double? defaultValue)
        {
            if (String.IsNullOrEmpty(nameCamelCase)) throw new NullOrEmptyException(() => nameCamelCase);

            NameCamelCase = nameCamelCase;
            DimensionEnum = dimensionEnum;
            ListIndex = listIndex;
            DefaultValue = defaultValue;
            Value = defaultValue;
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
