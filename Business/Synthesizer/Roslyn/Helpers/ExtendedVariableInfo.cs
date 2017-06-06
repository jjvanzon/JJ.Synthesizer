using System.Diagnostics;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Roslyn.Helpers
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    internal class ExtendedVariableInfo
    {
        public string VariableNameCamelCase { get; }
        /// <summary> Can be null or empty. </summary>
        public string CanonicalName { get; }
        public DimensionEnum DimensionEnum { get; }

        /// <summary> For dimension values this is the dimension stack level. </summary>
        public int ListIndex { get; }
        public double? DefaultValue { get; }
        public double? Value { get; }

        public ExtendedVariableInfo(
            string variableNameCamelCase,
            string canonicalName,
            DimensionEnum dimensionEnum, 
            int listIndex, 
            double? defaultValue)
        {
            if (string.IsNullOrEmpty(variableNameCamelCase)) throw new NullOrEmptyException(() => variableNameCamelCase);

            VariableNameCamelCase = variableNameCamelCase;
            CanonicalName = canonicalName;
            DimensionEnum = dimensionEnum;
            ListIndex = listIndex;
            DefaultValue = defaultValue;
            Value = defaultValue;
        }

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}
