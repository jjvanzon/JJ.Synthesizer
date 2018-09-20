using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable UnusedVariable
// ReSharper disable AccessToModifiedClosure
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class InterpolateTests
    {
        [TestMethod]
        public void Test_Interpolate_Stripe_LookAhead_DimensionNotTime_Forward_StartPosition0_WithCalculatorClasses()
            => Test_Interpolate_Stripe_LookAhead_DimensionNotTime_Forward_StartPosition0(CalculationEngineEnum.CalculatorClasses);

        private void Test_Interpolate_Stripe_LookAhead_DimensionNotTime_Forward_StartPosition0(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Interpolate_Base(
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LookAhead,
                DimensionEnum.Number,
                rate: 12.0 / MathHelper.TWO_PI,
                new[]
                {
                    (0.00, 1.0),
                    (0.33, 1.0),

                    (0.67, -1.0),
                    (1.00, -1.0),
                    (1.33, -1.0),

                    (1.67, 2.0),
                    (2.00, 2.0),
                    (2.33, 2.0),

                    (2.67, -2.0),
                    (3.00, -2.0),
                    (3.33, -2.0),

                    (3.67, -2.0),
                    (4.00, -2.0)
                },
                calculationEngineEnum);

        [TestMethod]
        public void Test_Interpolate_Line_LagBehind_DimensionTime_Forward_StartPosition0_WithCalculatorClasses()
            => Test_Interpolate_Line_LagBehind_DimensionTime_Forward_StartPosition0(CalculationEngineEnum.CalculatorClasses);

        private void Test_Interpolate_Line_LagBehind_DimensionTime_Forward_StartPosition0(CalculationEngineEnum calculationEngineEnum)
            => Test_Interpolate_Base(
                InterpolationTypeEnum.Line,
                FollowingModeEnum.LagBehind,
                DimensionEnum.Time,
                rate: 12.0 / MathHelper.TWO_PI,
                new[]
                {
                    (0.0, 1.0),
                    (0.5, 0.0),
                    (1.0, -1.0),
                    (1.5, 0.5),
                    (2.0, 2.0),
                    (2.5, 0.0),
                    (3.0, -2.0),
                    (3.5, -2.0),
                    (4.0, -2.0)
                },
                calculationEngineEnum);

        [TestMethod]
        public void Test_Interpolate_Line_LagBehind_DimensionTime_Forward_StartPositionNegative_WithCalculatorClasses()
            => Test_Interpolate_Line_LagBehind_DimensionTime_Forward_StartPositionNegative(CalculationEngineEnum.CalculatorClasses);

        private void Test_Interpolate_Line_LagBehind_DimensionTime_Forward_StartPositionNegative(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Interpolate_Base(
                InterpolationTypeEnum.Line,
                FollowingModeEnum.LagBehind,
                DimensionEnum.Time,
                rate: 12.0 / MathHelper.TWO_PI,
                new[]
                {
                    (-2.0, 1.0),
                    (-1.5, 1.0),
                    (-1.0, 0.0),
                    (-0.5, -1.0),
                    (0.0, 0.5),
                    (0.5, 2.0),
                    (1.0, 0.0),
                    (1.5, -2.0),
                    (2.0, -2.0)
                },
                calculationEngineEnum);

        [TestMethod]
        public void Test_Interpolate_Line_LagBehind_DimensionNotTime_Backward_StartPositionPositive_WithCalculatorClasses()
            => Test_Interpolate_Line_LagBehind_DimensionNotTime_Backward_StartPositionPositive(CalculationEngineEnum.CalculatorClasses);

        private void Test_Interpolate_Line_LagBehind_DimensionNotTime_Backward_StartPositionPositive(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Interpolate_Base(
                InterpolationTypeEnum.Line,
                FollowingModeEnum.LagBehind,
                DimensionEnum.Number,
                rate: 12.0 / MathHelper.TWO_PI,
                new[]
                {
                    (3.0, -2.0),
                    (2.5, -2.0),
                    (2.0, 0.0),
                    (1.5, 2.0),
                    (1.0, 0.5),
                    (0.5, -1.0),
                    (0.0, 0.0),
                    (0.5, 1.0),
                    (1.0, 1.0)
                },
                calculationEngineEnum);

        private void Test_Interpolate_Base(
            InterpolationTypeEnum interpolationTypeEnum,
            FollowingModeEnum followingModeEnum,
            DimensionEnum dimensionEnum,
            double rate,
            IList<(double dimensionValue, double outputValue)> points,
            CalculationEngineEnum calculationEngineEnum)
        {
            IList<DimensionEnum> inputDimensionEnums = new[] { dimensionEnum, DimensionEnum.Rate };
            IList<(double, double)> inputTuples = points.Select(point => (point.dimensionValue, rate)).ToArray();
            IList<double> outputValues = points.Select(point => point.outputValue).ToArray();

            TestExecutor.ExecuteTest(
                x => x.Interpolate(
                    x.New(nameof(SystemPatchNames.Sin), x.PatchInlet(dimensionEnum)),
                    x.PatchInlet(DimensionEnum.Rate),
                    interpolationTypeEnum,
                    dimensionEnum,
                    "",
                    followingModeEnum),
                inputDimensionEnums,
                inputTuples,
                outputValues,
                calculationEngineEnum);
        }
    }
}