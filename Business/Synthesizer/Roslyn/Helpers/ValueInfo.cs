using System;
using System.Diagnostics;
using System.Globalization;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Roslyn.Helpers
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal class ValueInfo
    {
        private static readonly CultureInfo _formattingCulture = new CultureInfo("en-US");

        public string NameCamelCase { get; }
        public DimensionEnum DimensionEnum { get; }
        public int ListIndex { get; }
        public double? DefaultValue { get; }
        public double? Value { get; }

        public ValueInfo(string nameCamelCase, DimensionEnum dimensionEnum, int listIndex, double? defaultValue)
        {
            if (String.IsNullOrEmpty(nameCamelCase)) throw new NullOrEmptyException(() => nameCamelCase);

            NameCamelCase = nameCamelCase;
            DimensionEnum = dimensionEnum;
            ListIndex = listIndex;
            DefaultValue = defaultValue;
            Value = defaultValue;
        }

        public ValueInfo(string nameCamelCase)
        {
            if (String.IsNullOrEmpty(nameCamelCase)) throw new NullOrEmptyException(() => nameCamelCase);

            NameCamelCase = nameCamelCase;
        }

        public ValueInfo(double value)
        {
            Value = value;
        }

        public string GetLiteral()
        {
            if (!String.IsNullOrEmpty(NameCamelCase))
            {
                return NameCamelCase;
            }

            if (Value.HasValue)
            {
                string formattedValue = FormatValue();
                return formattedValue;
            }

            throw new Exception($"{nameof(NameCamelCase)} and {nameof(Value)} are both empty, making it impossible to generate a literal.");
        }

        public string FormatValue()
        {
            if (!Value.HasValue) throw new NullException(() => Value);

            double value = Value.Value;

            if (Double.IsNaN(value))
            {
                return "Double.NaN";
            }
            else if (Double.IsPositiveInfinity(value))
            {
                return "Double.PositiveInfinity";
            }
            else if (Double.IsNegativeInfinity(value))
            {
                return "Double.NegativeInfinity";
            }
            else
            {
                // TODO: Low priority: format smaller numbers without exponential notation.
                string formattedValue = value.ToString("0.0###############E0", _formattingCulture);
                return formattedValue;
            }
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
