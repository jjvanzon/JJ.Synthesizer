using System.Globalization;

namespace JJ.Business.Synthesizer.Roslyn.Helpers
{
    internal static class CompilationHelper
    {
        private const double MAXIMUM_VALUE_WITHOUT_SCIENTIFIC_NOTATION = 50000;
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
                if (value <= MAXIMUM_VALUE_WITHOUT_SCIENTIFIC_NOTATION)
                {
                    string formattedValue = value.ToString("0.0###############", _formattingCulture);
                    return formattedValue;
                }
                else
                {

                    string formattedValue = value.ToString("0.0###############E0", _formattingCulture);
                    return formattedValue;
                }
            }
        }
    }
}
