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
        private static readonly IList<DimensionInfo> _emptyDimensionInfoList = Array.Empty<DimensionInfo>();

        public static double CalculateOneValue(IPatchCalculator patchCalculator, double time = 0.0)
        {
            const int frameCount = 1;
            var buffer = new float[1];
            patchCalculator.Calculate(buffer, frameCount, time);
            return buffer[0];
        }

        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            double expectedY,
            CalculationEngineEnum calculationEngineEnum,
            bool mustCompareZeroAndNonZeroOnly = false)
            => ExecuteTest(
                operatorFactoryDelegate,
                _ => expectedY,
                _emptyDimensionInfoList,
                calculationEngineEnum,
                mustCompareZeroAndNonZeroOnly);

        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double> func,
            IList<double> xValues,
            CalculationEngineEnum calculationEngineEnum,
            bool mustCompareZeroAndNonZeroOnly = false)
            => ExecuteTest(
                operatorFactoryDelegate,
                func,
                TestConstants.DEFAULT_DIMENSION_ENUM,
                xValues,
                calculationEngineEnum,
                mustCompareZeroAndNonZeroOnly);

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
    }
}