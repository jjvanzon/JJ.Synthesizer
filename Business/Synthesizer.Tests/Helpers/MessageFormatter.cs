using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Mathematics;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions.Comparative;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class MessageFormatter
    {
        private const int MAX_PLOT_COLUMN_COUNT = 40;

        public static string GetNote(TestOptions testOptions)
        {
            if (testOptions == null) throw new ArgumentNullException(nameof(testOptions));

            var sb = new StringBuilder();
            sb.Append("(Note: ");

            if (testOptions.SignificantDigits.HasValue || testOptions.DecimalDigits.HasValue)
            {
                sb.Append("Values are ");
            }

            if (testOptions.SignificantDigits.HasValue)
            {
                sb.Append($"tested for {testOptions.SignificantDigits} significant digits, ");
            }

            if (testOptions.DecimalDigits.HasValue)
            {
                sb.Append($"tested for {testOptions.DecimalDigits} decimal digits, ");
            }

            if (testOptions.MustCompareZeroAndNonZeroOnly)
            {
                sb.Append("only compared for 0 and non-0, ");
            }

            sb.Append("NaN is converted to 0.");

            sb.Append(')');

            return sb.ToString();
        }

        public static string TryGetVarConstMessage(IList<DimensionEnum> inputDimensionEnums, IList<double?> consts)
        {
            if (!consts.Any())
            {
                return null;
            }

            var sb = new StringBuilder();

            sb.Append("Testing for ");

            if (consts.Count > 1) sb.Append('(');

            string concatenatedVarConstDescriptors =
                string.Join(", ", inputDimensionEnums.Zip(consts).Select(x => GetVarConstDescriptor(x.Item1, x.Item2)));

            sb.Append(concatenatedVarConstDescriptors);

            if (consts.Count > 1) sb.Append(')');

            sb.Append(".");

            return sb.ToString();
        }

        private static string GetVarConstDescriptor(DimensionEnum inputDimensionEnum, double? @const)
            => @const.HasValue ? $"const {@const}" : $"var {inputDimensionEnum}";

        public static string GetOutputValueMessage(
            int i,
            IList<DimensionEnum> inputDimensionEnums,
            IList<double> inputValues,
            float outputValue)
        {
            if (!inputDimensionEnums.Any())
            {
                return GetOutputValueMessage_WithoutInputs(i, outputValue);
            }

            string pointDescriptor = GetPointDescriptor(i, inputDimensionEnums, inputValues);
            string message = $"{pointDescriptor} => {outputValue}";
            return message;
        }

        private static string GetOutputValueMessage_WithoutInputs(int i, float outputValue) => $"Result [{i}] = {outputValue}";

        public static string GetOutputValueMessage_NotValid(
            int i,
            IList<DimensionEnum> inputDimensionEnums,
            IList<double> inputValues,
            float expectedOutputValue,
            float actualOutputValue)
        {
            if (inputDimensionEnums == null) throw new ArgumentNullException(nameof(inputDimensionEnums));
            if (inputValues == null) throw new ArgumentNullException(nameof(inputValues));

            if (!inputDimensionEnums.Any() && !inputValues.Any())
            {
                return GetOutputValueMessage_WithoutInputs_NotValue(expectedOutputValue, actualOutputValue);
            }

            string pointDescriptor = GetPointDescriptor(i, inputDimensionEnums, inputValues);

            string message = $"{pointDescriptor} " +
                             $"should have result = {expectedOutputValue}, " +
                             $"but has result = {actualOutputValue} instead.";

            return message;
        }

        private static string GetOutputValueMessage_WithoutInputs_NotValue(float expectedOutputValue, float actualOutputValue)
            => $"Result should be {expectedOutputValue}, but is {actualOutputValue} instead.";

        private static string GetPointDescriptor(int i, IList<DimensionEnum> inputDimensionEnums, IList<double> inputValues)
        {
            if (inputValues.Count != inputDimensionEnums.Count)
            {
                throw new NotEqualException(() => inputValues.Count, () => inputDimensionEnums.Count);
            }

            string concatenatedInputValues = string.Join(
                ", ",
                inputDimensionEnums.Zip(inputValues).Select(x => $"{x.Item1}={x.Item2}"));

            string pointDescriptor = $"Point [{i}]: ({concatenatedInputValues})";
            return pointDescriptor;
        }

        public static IList<string> TryPlot(IList<double[]> inputPoints, IList<double> outputValues, int plotLineCount, bool onlyUseOutputValuesForPlot)
        {
            if (onlyUseOutputValuesForPlot)
            {
                return TryPlot(outputValues, plotLineCount);
            }
            else
            {
                return TryPlot(inputPoints, outputValues, plotLineCount);
            }
        }

        /// <summary> Returns a plot in the form of text lines. Only does so if there are any input dimensions. </summary>
        /// <param name="inputPoints">Only uses the first dimension</param>
        private static IList<string> TryPlot(IList<double[]> inputPoints, IList<double> outputValues, int plotLineCount)
        {
            if (inputPoints == null) throw new ArgumentNullException(nameof(inputPoints));
            if (outputValues == null) throw new ArgumentNullException(nameof(outputValues));

            bool willPlot = WillPlot(inputPoints);
            if (!willPlot)
            {
                return Array.Empty<string>();
            }

            IList<(double, double)> tuples = inputPoints.Select(x => x[0]).Zip(outputValues).ToArray();

            int plotColumnCount = outputValues.Count;
            if (plotColumnCount > MAX_PLOT_COLUMN_COUNT)
            {
                plotColumnCount = MAX_PLOT_COLUMN_COUNT;
            }

            IList<string> plotLines = TextPlotter.Plot(tuples, plotColumnCount, plotLineCount);

            return plotLines;
        }

        private static bool WillPlot(IList<double[]> inputPoints)
        {
            bool hasMultiplePoints = inputPoints.Count > 1;
            if (!hasMultiplePoints)
            {
                return false;
            }

            bool hasDimensions = inputPoints.Any(x => x.Length >= 1);

            return hasDimensions;
        }

        /// <summary> Returns a plot in the form of text lines. Only does so if there are any points. </summary>
        private static IList<string> TryPlot(IList<double> outputValues, int plotLineCount)
        {
            if (outputValues == null) throw new ArgumentNullException(nameof(outputValues));

            bool willPlot = WillPlot(outputValues);
            if (!willPlot)
            {
                return Array.Empty<string>();
            }

            int plotColumnCount = outputValues.Count;
            if (plotColumnCount > MAX_PLOT_COLUMN_COUNT)
            {
                plotColumnCount = MAX_PLOT_COLUMN_COUNT;
            }

            IList<string> plotLines = TextPlotter.Plot(outputValues, plotColumnCount, plotLineCount);

            return plotLines;
        }

        private static bool WillPlot(IList<double> outputValues)
        {
            bool hasMultipleValues = outputValues.Count > 1;
            return hasMultipleValues;
        }
    }
}