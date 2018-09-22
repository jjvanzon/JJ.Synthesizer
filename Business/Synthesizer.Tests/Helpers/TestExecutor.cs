using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions.Comparative;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class TestExecutor
    {
        private static readonly IList<DimensionInfo> _emptyDimensionInfoList = Array.Empty<DimensionInfo>();

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
            IList<double> inputValues,
            CalculationEngineEnum calculationEngineEnum,
            bool mustCompareZeroAndNonZeroOnly = false)
        {
            var dimensionInfoList = new[] { new DimensionInfo(dimensionEnum, inputValues) };

            ExecuteTest(
                operatorFactoryDelegate,
                arr => func(arr[0]),
                dimensionInfoList,
                calculationEngineEnum,
                mustCompareZeroAndNonZeroOnly);
        }

        /// <summary> 2-dimensional cartesian product with func. </summary>
        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double, double> func,
            DimensionEnum xDimensionEnum,
            IList<double> xValues,
            DimensionEnum yDimensionEnum,
            IList<double> yValues,
            CalculationEngineEnum calculationEngineEnum,
            bool mustCompareZeroAndNonZeroOnly = false)
        {
            var dimensionInfoList = new[] { new DimensionInfo(xDimensionEnum, xValues), new DimensionInfo(yDimensionEnum, yValues) };

            ExecuteTest(
                operatorFactoryDelegate,
                arr => func(arr[0], arr[1]),
                dimensionInfoList,
                calculationEngineEnum,
                mustCompareZeroAndNonZeroOnly);
        }

        /// <summary> 3-dimensional cartesian product with func. </summary>
        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double, double, double> func,
            DimensionEnum xDimensionEnum,
            IList<double> xValues,
            DimensionEnum yDimensionEnum,
            IList<double> yValues,
            DimensionEnum zDimensionEnum,
            IList<double> zValues,
            CalculationEngineEnum calculationEngineEnum,
            bool mustCompareZeroAndNonZeroOnly = false)
        {
            var dimensionInfoList = new[]
            {
                new DimensionInfo(xDimensionEnum, xValues), new DimensionInfo(yDimensionEnum, yValues),
                new DimensionInfo(zDimensionEnum, zValues)
            };

            ExecuteTest(
                operatorFactoryDelegate,
                arr => func(arr[0], arr[1], arr[2]),
                dimensionInfoList,
                calculationEngineEnum,
                mustCompareZeroAndNonZeroOnly);
        }

        /// <summary> N-dimensional with func. </summary>
        private static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double[], double> funcWithArray,
            IList<DimensionInfo> dimensionInfoList,
            CalculationEngineEnum calculationEngineEnum,
            bool mustCompareZeroAndNonZeroOnly)
        {
            (IList<string> logMessages, IList<string> errorMessages) = PatchTester_MultipleConstVarVariations.ExecuteTest(
                operatorFactoryDelegate,
                funcWithArray,
                dimensionInfoList,
                calculationEngineEnum,
                mustCompareZeroAndNonZeroOnly);

            logMessages.ForEach(Console.WriteLine);

            if (errorMessages.Any())
            {
                Assert.Fail(string.Join(Environment.NewLine, errorMessages));
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
                // TODO: It feels strange that this would delegate to an overload that takes func.
                // It should delegate to an n-dimension variation of an overload with expected output values.
                _ => expectedOutputValue,
                _emptyDimensionInfoList,
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
            if (inputDimensionEnums == null) throw new ArgumentNullException(nameof(inputDimensionEnums));
            if (inputTuples == null) throw new ArgumentNullException(nameof(inputTuples));
            if (inputDimensionEnums.Count != 2) throw new NotEqualException(() => inputDimensionEnums.Count, 2);

            DimensionEnum xDimensionEnum = inputDimensionEnums[0];
            IList<double> xValues = inputTuples.Select(x => x.Item1).ToArray();
            DimensionEnum yDimensionEnum = inputDimensionEnums[1];
            IList<double> yValues = inputTuples.Select(x => x.Item2).ToArray();

            // TODO: Calling overload with cartesian product for now. That will change in the future.

            // Generate more output values because it needs one for each combination in the cartesian product.
            expectedOutputValues = expectedOutputValues.Repeat(expectedOutputValues.Count).ToArray();

            ExecuteTest(
                operatorFactoryDelegate,
                xDimensionEnum,
                xValues,
                yDimensionEnum,
                yValues,
                expectedOutputValues,
                calculationEngineEnum,
                mustCompareZeroAndNonZeroOnly);
        }

        /// <summary> 2-dimensional cartesian product with expected output values. </summary>
        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            DimensionEnum xDimensionEnum,
            IList<double> xValues,
            DimensionEnum yDimensionEnum,
            IList<double> yValues,
            IList<double> expectedOutputValues,
            CalculationEngineEnum calculationEngineEnum,
            bool mustCompareZeroAndNonZeroOnly = false)
        {
            var dimensionInfoList = new[] { new DimensionInfo(xDimensionEnum, xValues), new DimensionInfo(yDimensionEnum, yValues) };

            ExecuteTest(
                operatorFactoryDelegate,
                expectedOutputValues,
                dimensionInfoList,
                calculationEngineEnum,
                mustCompareZeroAndNonZeroOnly);
        }

        /// <summary> N-dimensional with expected output values. </summary>
        private static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            IList<double> expectedOutputValues,
            IList<DimensionInfo> dimensionInfoList,
            CalculationEngineEnum calculationEngineEnum,
            bool mustCompareZeroAndNonZeroOnly)
        {
            (IList<string> logMessages, IList<string> errorMessages) = PatchTester_MultipleConstVarVariations.ExecuteTest(
                operatorFactoryDelegate,
                expectedOutputValues,
                dimensionInfoList,
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