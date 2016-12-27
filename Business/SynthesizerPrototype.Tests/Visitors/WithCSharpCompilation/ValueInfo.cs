using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JJ.Framework.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace JJ.Business.SynthesizerPrototype.Tests.Visitors.WithCSharpCompilation
{
    internal class ValueInfo
    {
        private static readonly CultureInfo _formattingCulture = new CultureInfo("en-US");

        public string NameCamelCase { get; }
        public double? Value { get; }

        public ValueInfo(string nameCamelCase, double value)
        {
            if (String.IsNullOrEmpty(nameCamelCase)) throw new NullOrEmptyException(() => nameCamelCase);

            NameCamelCase = nameCamelCase;
            Value = value;
        }

        public ValueInfo(string name)
        {
            if (String.IsNullOrEmpty(name)) throw new NullOrEmptyException(() => name);

            NameCamelCase = name;
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
    }
}
