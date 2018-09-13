using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class TestHelper
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
            CalculationMethodEnum calculationMethodEnum)
            => PatchTester_WithConstVarVariations.ExecuteTest(operatorFactoryDelegate, _ => expectedY, _emptyDimensionInfoList, calculationMethodEnum);

        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double> func,
            IList<double> xValues,
            CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(operatorFactoryDelegate, func, TestConstants.DEFAULT_DIMENSION_ENUM, xValues, calculationMethodEnum);

        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double> func,
            DimensionEnum dimensionEnum,
            IList<double> inputValues,
            CalculationMethodEnum calculationMethodEnum)
        {
            var dimensionInfoList = new[] { new DimensionInfo(dimensionEnum, inputValues) };
            PatchTester_WithConstVarVariations.ExecuteTest(operatorFactoryDelegate, arr => func(arr[0]), dimensionInfoList, calculationMethodEnum);
        }

        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double, double> func,
            DimensionEnum xDimensionEnum,
            IList<double> xValues,
            DimensionEnum yDimensionEnum,
            IList<double> yValues,
            CalculationMethodEnum calculationMethodEnum)
        {
            var dimensionInfoList = new[] { new DimensionInfo(xDimensionEnum, xValues), new DimensionInfo(yDimensionEnum, yValues) };

            PatchTester_WithConstVarVariations.ExecuteTest(
                operatorFactoryDelegate,
                arr => func(arr[0], arr[1]),
                dimensionInfoList,
                calculationMethodEnum);
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
            CalculationMethodEnum calculationMethodEnum)
        {
            var dimensionInfoList = new[]
            {
                new DimensionInfo(xDimensionEnum, xValues), new DimensionInfo(yDimensionEnum, yValues), new DimensionInfo(zDimensionEnum, zValues)
            };

            PatchTester_WithConstVarVariations.ExecuteTest(
                operatorFactoryDelegate,
                arr => func(arr[0], arr[1], arr[2]),
                dimensionInfoList,
                calculationMethodEnum);
        }
    }
}