using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.Comparative;
using JJ.Framework.Mathematics;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class OutletTester
    {
        private readonly bool _mustCompareZeroAndNonZeroOnly;
        private readonly IPatchCalculator _calculator;

        public OutletTester(
            Outlet outlet,
            PatchFacade patchFacade,
            CalculationEngineEnum calculationEngineEnum,
            bool mustCompareZeroAndNonZeroOnly)
        {
            if (patchFacade == null) throw new ArgumentNullException(nameof(patchFacade));

            _mustCompareZeroAndNonZeroOnly = mustCompareZeroAndNonZeroOnly;
            _calculator = patchFacade.CreateCalculator(outlet, 2, 1, 0, new CalculatorCache(), calculationEngineEnum);
        }

        public (IList<string> logMessages, IList<string> errorMessages) ExecuteTest(
            IList<DimensionEnum> inputDimensionEnums,
            IList<double[]> inputPoints,
            IList<double> expectedOutputValues)
        {
            // Pre-Conditions
            if (inputDimensionEnums == null) throw new ArgumentNullException(nameof(inputDimensionEnums));
            if (inputPoints == null) throw new ArgumentNullException(nameof(inputPoints));
            if (expectedOutputValues == null) throw new ArgumentNullException(nameof(expectedOutputValues));
            if (expectedOutputValues.Count == 0) throw new CollectionEmptyException(nameof(expectedOutputValues));

            bool hasInput = inputDimensionEnums.Count != 0 || inputPoints.Count != 0;

            if (hasInput)
            {
                if (inputPoints.Count != expectedOutputValues.Count)
                {
                    throw new NotEqualException(() => inputPoints.Count, () => expectedOutputValues.Count);
                }

                for (var i = 0; i < inputPoints.Count; i++)
                {
                    if (inputPoints[i].Length != inputDimensionEnums.Count)
                    {
                        throw new NotEqualException(() => inputPoints[i].Length, () => inputDimensionEnums.Count);
                    }
                }
            }

            int? timeDimensionIndex = inputDimensionEnums.TryGetIndexOf(x => x == DimensionEnum.Time);
            var hasReset = false;
            IList<double> actualOutputValues = new double[expectedOutputValues.Count];

            for (var pointIndex = 0; pointIndex < expectedOutputValues.Count; pointIndex++)
            {
                double time = 0;

                if (hasInput)
                {
                    IList<double> inputValues = inputPoints[pointIndex];

                    // Set Input Values
                    for (var dimensionIndex = 0; dimensionIndex < inputDimensionEnums.Count; dimensionIndex++)
                    {
                        DimensionEnum inputDimensionEnum = inputDimensionEnums[dimensionIndex];
                        double inputValue = inputValues[dimensionIndex];
                        _calculator.SetValue(inputDimensionEnum, inputValue);
                    }

                    // Determine Time
                    if (timeDimensionIndex.HasValue)
                    {
                        time = inputValues[timeDimensionIndex.Value];
                    }
                }

                // Reset upon the first iteration. (Note: Input values must be set before you call Reset.)
                if (!hasReset)
                {
                    _calculator.Reset(time);
                    hasReset = true;
                }

                // Calculate Value
                var buffer = new float[1];
                _calculator.Calculate(buffer, buffer.Length, time);
                double actualOutputValue = buffer[0];

                actualOutputValues[pointIndex] = actualOutputValue;
            }

            // Check
            var logMessages = new List<string>();
            var errorMessages = new List<string>();

            for (var i = 0; i < expectedOutputValues.Count; i++)
            {
                IList<double> inputValues = hasInput ? inputPoints[i] : Array.Empty<double>();
                double expectedOutputValue = expectedOutputValues[i];
                double actualOutputValue = actualOutputValues[i];

                float canonicalExpectedOutputValue = ToCanonical(expectedOutputValue);
                float canonicalActualOutputValue = ToCanonical(actualOutputValue);

                if (canonicalExpectedOutputValue != canonicalActualOutputValue)
                {
                    string failureMessage = TestMessageFormatter.GetOutputValueMessage_NotValid(
                        i,
                        inputDimensionEnums,
                        inputValues,
                        canonicalExpectedOutputValue,
                        canonicalActualOutputValue);

                    logMessages.Add(failureMessage);
                    errorMessages.Add(failureMessage);
                }
                else
                {
                    logMessages.Add(
                        TestMessageFormatter.GetOutputValueMessage(i, inputDimensionEnums, inputValues, canonicalActualOutputValue));
                }
            }

            return (logMessages, errorMessages);
        }

        /// <summary> Converts to float, rounds to significant digits and converts NaN to 0 which 'winmm' would trip over. </summary>
        private float ToCanonical(double input)
        {
            var output = (float)input;

            output = MathHelper.RoundToSignificantDigits(output, TestConstants.DEFAULT_SIGNIFICANT_DIGITS);

            // Calculation engine will not output NaN.
            if (float.IsNaN(output))
            {
                output = 0;
            }

            if (_mustCompareZeroAndNonZeroOnly)
            {
                if (output != 0)
                {
                    output = 1;
                }
            }

            return output;
        }
    }
}