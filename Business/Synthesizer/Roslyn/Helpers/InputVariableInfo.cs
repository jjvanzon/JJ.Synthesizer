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
        public string VariableNameCamelCase { get; }
        /// <summary> Can be null or empty. </summary>
        public string CanonicalName { get; }
        public DimensionEnum DimensionEnum { get; }
        public int ListIndex { get; }
        public double? DefaultValue { get; }
        public double? Value { get; }

        public InputVariableInfo(
            string variableNameCamelCase,
            string canonicalName,
            DimensionEnum dimensionEnum, 
            int listIndex, 
            double? defaultValue)
        {
            if (String.IsNullOrEmpty(variableNameCamelCase)) throw new NullOrEmptyException(() => variableNameCamelCase);

            VariableNameCamelCase = variableNameCamelCase;
            CanonicalName = canonicalName;
            DimensionEnum = dimensionEnum;
            ListIndex = listIndex;
            DefaultValue = defaultValue;
            Value = defaultValue;
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
