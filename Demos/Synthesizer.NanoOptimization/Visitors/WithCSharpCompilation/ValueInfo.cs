using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.NanoOptimization.Visitors.WithCSharpCompilation
{
    internal class ValueInfo
    {
        private static readonly CultureInfo _formattingCulture = new CultureInfo("en-US");

        public string Name { get; }
        public double? Value { get; }

        public ValueInfo(string name, double value)
        {
            if (String.IsNullOrEmpty(name)) throw new NullOrEmptyException(() => name);

            Name = name;
            Value = value;
        }

        public ValueInfo(string name)
        {
            if (String.IsNullOrEmpty(name)) throw new NullOrEmptyException(() => name);

            Name = name;
        }

        public ValueInfo(double value)
        {
            Value = value;
        }

        public string GetLiteral()
        {
            if (!String.IsNullOrEmpty(Name))
            {
                return Name;
            }

            if (Value.HasValue)
            {
                double value = Value.Value;

                if (Double.IsNaN(value) || Double.IsInfinity(value))
                {
                    return "Double.NaN";
                }
                else
                {
                    // TODO: Consider if this produces a complete literal, with exponent, decimal symbol, full precision.
                    string formattedValue = value.ToString(_formattingCulture);
                    return formattedValue;
                }
            }

            throw new Exception("Name and value are both empty, making it impossible to generate a literal.");
        }
    }
}
