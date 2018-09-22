using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class TestExecutor
    {
        public static double CalculateOneValue(IPatchCalculator patchCalculator, double time = 0.0)
        {
            const int frameCount = 1;
            var buffer = new float[1];
            patchCalculator.Calculate(buffer, frameCount, time);
            return buffer[0];
        }

        // With Func

        /// <summary> 1-dimensional with func. </summary>
        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double> func,
            DimensionEnum dimensionEnum,
            double[] inputValues,
            CalculationEngineEnum calculationEngineEnum,
            bool mustCompareZeroAndNonZeroOnly = false)
            => ExecuteTest(
                operatorFactoryDelegate,
                arr => func(arr[0]),
                new[] { dimensionEnum },
                new[] { inputValues },
                calculationEngineEnum,
                mustCompareZeroAndNonZeroOnly);

        /// <summary> 2-dimensional cartesian product with func. </summary>
        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double, double> func,
            DimensionEnum xDimensionEnum,
            double[] xValues,
            DimensionEnum yDimensionEnum,
            double[] yValues,
            CalculationEngineEnum calculationEngineEnum,
            bool mustCompareZeroAndNonZeroOnly = false)
            => ExecuteTest(
                operatorFactoryDelegate,
                arr => func(arr[0], arr[1]),
                new[] { xDimensionEnum, yDimensionEnum },
                new[] { xValues, yValues },
                calculationEngineEnum,
                mustCompareZeroAndNonZeroOnly);

        /// <summary> 3-dimensional cartesian product with func. </summary>
        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double, double, double> func,
            DimensionEnum xDimensionEnum,
            double[] xValues,
            DimensionEnum yDimensionEnum,
            double[] yValues,
            DimensionEnum zDimensionEnum,
            double[] zValues,
            CalculationEngineEnum calculationEngineEnum,
            bool mustCompareZeroAndNonZeroOnly = false)
            => ExecuteTest(
                operatorFactoryDelegate,
                arr => func(arr[0], arr[1], arr[2]),
                new[] { xDimensionEnum, yDimensionEnum, zDimensionEnum },
                new[] { xValues, yValues, zValues },
                calculationEngineEnum,
                mustCompareZeroAndNonZeroOnly);

        /// <summary> N-dimensional cartesian with func. </summary>
        private static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double[], double> funcWithArray,
            IList<DimensionEnum> inputDimensionEnums,
            IList<double[]> inputValues,
            CalculationEngineEnum calculationEngineEnum,
            bool mustCompareZeroAndNonZeroOnly)
        {
            IList<double[]> inputPoints = inputValues.CrossJoin(x => x.ToArray()).ToArray();

            using (var patchVarConstTester = new PatchVarConstTester())
            {
                (IList<string> logMessages, IList<string> errorMessages) =
                    patchVarConstTester.ExecuteTest(
                        operatorFactoryDelegate,
                        funcWithArray,
                        inputDimensionEnums,
                        inputPoints,
                        calculationEngineEnum,
                        mustCompareZeroAndNonZeroOnly);

                logMessages.ForEach(Console.WriteLine);

                if (errorMessages.Any())
                {
                    Assert.Fail(string.Join(Environment.NewLine, errorMessages));
                }
            }
        }

        // With ExpectedOutputValues

        /// <summary> 0-dimensional with expected output value. </summary>
        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            double expectedOutputValue,
            CalculationEngineEnum calculationEngineEnum,
            bool mustCompareZeroAndNonZeroOnly = false)
            => ExecuteTest(
                operatorFactoryDelegate,
                Array.Empty<DimensionEnum>(),
                Array.Empty<double[]>(),
                expectedOutputValue.AsArray(),
                calculationEngineEnum,
                mustCompareZeroAndNonZeroOnly);

        /// <summary> 2-part tuples with expected output values. </summary>
        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            IList<DimensionEnum> inputDimensionEnums,
            IList<(double, double)> inputTuples,
            IList<double> expectedOutputValues,
            CalculationEngineEnum calculationEngineEnum,
            bool mustCompareZeroAndNonZeroOnly = false)
        {
            if (inputTuples == null) throw new ArgumentNullException(nameof(inputTuples));

            IList<double[]> inputPoints = inputTuples.Select(x => new[] { x.Item1, x.Item2 }).ToArray();

            ExecuteTest(
                operatorFactoryDelegate,
                inputDimensionEnums,
                inputPoints,
                expectedOutputValues,
                calculationEngineEnum,
                mustCompareZeroAndNonZeroOnly);
        }

        /// <summary> N-dimensional tuples with expected output values. </summary>
        private static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            IList<DimensionEnum> dimensionEnums,
            IList<double[]> inputPoints,
            IList<double> expectedOutputValues,
            CalculationEngineEnum calculationEngineEnum,
            bool mustCompareZeroAndNonZeroOnly)
        {
            using (var patchVarConstTester = new PatchVarConstTester())
            {
                (IList<string> logMessages, IList<string> errorMessages) =
                    patchVarConstTester.ExecuteTest(
                        operatorFactoryDelegate,
                        expectedOutputValues,
                        dimensionEnums,
                        inputPoints,
                        calculationEngineEnum,
                        mustCompareZeroAndNonZeroOnly);

                logMessages.ForEach(Console.WriteLine);

                if (errorMessages.Any())
                {
                    Assert.Fail(string.Join(Environment.NewLine, errorMessages));
                }
            }
        }
    }
}