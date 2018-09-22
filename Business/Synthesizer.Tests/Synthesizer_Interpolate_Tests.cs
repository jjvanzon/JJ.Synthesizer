using System;
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
    public class Synthesizer_Interpolate_Tests
    {
        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LookAhead_DimensionNotTime_Forward_StartPosition0_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Stripe_LookAhead_DimensionNotTime_Forward_StartPosition0(
                CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LookAhead_DimensionNotTime_Forward_StartPosition0(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LookAhead,
                DimensionEnum.Number,
                rate: 8.0 / MathHelper.TWO_PI,
                new[]
                {
                    (Math.PI * 0.00, 0.0),
                    (Math.PI * 0.01, 0.0),
                    (Math.PI * 0.24, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 0.25, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 0.26, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 0.49, 1.0),
                    (Math.PI * 0.50, 1.0),
                    (Math.PI * 0.51, 1.0),
                    (Math.PI * 0.74, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 0.75, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 0.76, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 0.99, 0.0),
                    (Math.PI * 1.00, 0.0),
                    (Math.PI * 1.01, 0.0),
                    (Math.PI * 1.24, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 1.25, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 1.26, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 1.49, -1.0),
                    (Math.PI * 1.50, -1.0),
                    (Math.PI * 1.51, -1.0),
                    (Math.PI * 1.74, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 1.75, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 1.76, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 1.99, 0.0),
                    (Math.PI * 2.00, 0.0),
                    (Math.PI * 2.01, 0.0)
                });

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Line_LagBehind_DimensionTime_Forward_StartPosition0_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Line_LagBehind_DimensionTime_Forward_StartPosition0(
                CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Line_LagBehind_DimensionTime_Forward_StartPosition0(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Line,
                FollowingModeEnum.LagBehind,
                DimensionEnum.Time,
                rate: 8.0 / MathHelper.TWO_PI,
                new[]
                {
                    (Math.PI * 0.00, 0.0),
                    (Math.PI * 0.25, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 0.50, 1.0),
                    (Math.PI * 0.75, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 1.00, 0.0),
                    (Math.PI * 1.25, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 1.50, -1.0),
                    (Math.PI * 1.75, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 2.00, 0.0)
                });

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Line_LagBehind_DimensionTime_Forward_StartPositionNegative_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Line_LagBehind_DimensionTime_Forward_StartPositionNegative(
                CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Line_LagBehind_DimensionTime_Forward_StartPositionNegative(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Line,
                FollowingModeEnum.LagBehind,
                DimensionEnum.Time,
                rate: 12.0 / MathHelper.TWO_PI,
                new[]
                {
                    (Math.PI * 0.00, 0.0),
                    (Math.PI * 0.25, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 0.50, 1.0),
                    (Math.PI * 0.75, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 1.00, 0.0),
                    (Math.PI * 1.25, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 1.50, -1.0),
                    (Math.PI * 1.75, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 2.00, 0.0)
                });

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Line_LagBehind_DimensionNotTime_Backward_StartPositionPositive_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Line_LagBehind_DimensionNotTime_Backward_StartPositionPositive(
                CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Line_LagBehind_DimensionNotTime_Backward_StartPositionPositive(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Line,
                FollowingModeEnum.LagBehind,
                DimensionEnum.Number,
                rate: 12.0 / MathHelper.TWO_PI,
                new[]
                {
                    (Math.PI * 0.00, 0.0),
                    (Math.PI * 0.25, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 0.50, 1.0),
                    (Math.PI * 0.75, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 1.00, 0.0),
                    (Math.PI * 1.25, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 1.50, -1.0),
                    (Math.PI * 1.75, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 2.00, 0.0)
                });

        private void Test_Synthesizer_Interpolate_Base(
            CalculationEngineEnum calculationEngineEnum,
            InterpolationTypeEnum interpolationTypeEnum,
            FollowingModeEnum followingModeEnum,
            DimensionEnum dimensionEnum,
            double rate,
            IList<(double dimensionValue, double outputValue)> points)
        {
            // Transform this method's input into something the TestExecutor wants.
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
                calculationEngineEnum,
                significantDigits: null,
                decimalDigits: 6);
        }
    }
}