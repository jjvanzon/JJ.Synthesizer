using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions.Comparative;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class TestMessageFormatter
    {
        public static string Note { get; } =
            $"(Note: Values are tested for {TestConstants.DEFAULT_SIGNIFICANT_DIGITS} significant digits and NaN is converted to 0.)";

        public static string TryGetVarConstMessage(IList<DimensionEnum> inputDimensionEnums, params double?[] consts)
        {
            if (!consts.Any())
            {
                return null;
            }

            var sb = new StringBuilder();

            sb.Append("Testing for ");

            if (consts.Length > 1) sb.Append('(');

            string concatenatedVarConstDescriptors =
                string.Join(", ", inputDimensionEnums.Zip(consts).Select(x => GetVarConstDescriptor(x.Item1, x.Item2)));

            sb.Append(concatenatedVarConstDescriptors);

            if (consts.Length > 1) sb.Append(')');

            sb.Append(".");

            return sb.ToString();
        }

        private static string GetVarConstDescriptor(DimensionEnum inputDimensionEnum, double? @const)
            => @const.HasValue ? $"const {@const}" : $"var {inputDimensionEnum}";

        public static string GetOutputValueMessage(int i, IList<DimensionEnum> inputDimensionEnums, IList<double> inputValues, float outputValue)
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

        private static string GetOutputValueMessage_WithoutInputs_NotValue(float expectedOutputValue, float actualOutputValue) => $"Result should be {expectedOutputValue}, but is {actualOutputValue} instead.";

        private static string GetPointDescriptor(int i, IList<DimensionEnum> inputDimensionEnums, IList<double> inputValues)
        {
            if (inputValues.Count != inputDimensionEnums.Count)
            {
                throw new NotEqualException(() => inputValues.Count, () => inputDimensionEnums.Count);
            }

            string concatenatedInputValues = string.Join(", ", inputDimensionEnums.Zip(inputValues).Select(x => $"{x.Item1}={x.Item2}"));
            string pointDescriptor = $"Point [{i}]: ({concatenatedInputValues})";
            return pointDescriptor;
        }
    }
}