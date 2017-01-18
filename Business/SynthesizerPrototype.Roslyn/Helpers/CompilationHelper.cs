using System.Globalization;

namespace JJ.Business.SynthesizerPrototype.Roslyn.Helpers
{
    internal static class CompilationHelper
    {
        private static readonly CultureInfo _formattingCulture = new CultureInfo("en-US");

        public static string FormatValue(double value)
        {
            if (double.IsNaN(value))
            {
                return "double.NaN";
            }
            else if (double.IsPositiveInfinity(value))
            {
                return "double.PositiveInfinity";
            }
            else if (double.IsNegativeInfinity(value))
            {
                return "double.NegativeInfinity";
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
