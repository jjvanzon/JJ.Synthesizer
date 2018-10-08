//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Tests.Helpers;
//using JJ.Framework.Mathematics;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//// ReSharper disable UnusedVariable
//// ReSharper disable AccessToModifiedClosure
//// ReSharper disable SuggestVarOrType_SimpleTypes

//namespace JJ.Business.Synthesizer.Tests
//{
//    [TestClass]
//    public class Synthesizer_Interpolate_Tests
//    {
//        [TestMethod]
//        public void Test_Synthesizer_Interpolate_Stripe_LookAhead_Forward_StartPosition0_WithCalculatorClasses()
//            => Test_Synthesizer_Interpolate_Stripe_LookAhead_Forward_StartPosition0(
//                CalculationEngineEnum.CalculatorClasses);

//        private void Test_Synthesizer_Interpolate_Stripe_LookAhead_Forward_StartPosition0(
//            CalculationEngineEnum calculationEngineEnum)
//            => Test_Synthesizer_Interpolate_Base(
//                calculationEngineEnum,
//                InterpolationTypeEnum.Stripe,
//                FollowingModeEnum.LookAhead,
//                rate: 4.0 / Math.PI,
//                new[]
//                {
//                    (Math.PI * 0.000000, 0.0),
//                    (Math.PI * 0.083333, 0.0),
//                    (Math.PI * 0.166667, MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 0.250000, MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 0.333333, MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 0.416667, 1.0),
//                    (Math.PI * 0.500000, 1.0),
//                    (Math.PI * 0.583333, 1.0),
//                    (Math.PI * 0.666667, MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 0.750000, MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 0.833333, MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 0.916667, 0.0),
//                    (Math.PI * 1.000000, 0.0),
//                    (Math.PI * 1.083333, 0.0),
//                    (Math.PI * 1.166667, -MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 1.250000, -MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 1.333333, -MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 1.416667, -1.0),
//                    (Math.PI * 1.500000, -1.0),
//                    (Math.PI * 1.583333, -1.0),
//                    (Math.PI * 1.666667, -MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 1.750000, -MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 1.833333, -MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 1.916667, 0.0),
//                    (Math.PI * 2.000000, 0.0),
//                    (Math.PI * 2.083333, 0.0)
//                });

//        [TestMethod]
//        public void Test_Synthesizer_Interpolate_Stripe_LagBehind_Forward_StartPosition0_WithCalculatorClasses()
//            => Test_Synthesizer_Interpolate_Stripe_LagBehind_Forward_StartPosition0(
//                CalculationEngineEnum.CalculatorClasses);

//        private void Test_Synthesizer_Interpolate_Stripe_LagBehind_Forward_StartPosition0(
//            CalculationEngineEnum calculationEngineEnum)
//            => Test_Synthesizer_Interpolate_Base(
//                calculationEngineEnum,
//                InterpolationTypeEnum.Stripe,
//                FollowingModeEnum.LagBehind,
//                4.0 / Math.PI,
//                new[]
//                {
//                    (Math.PI * 0.00000000, 0.0),
//                    (Math.PI * 0.08333333, 0.0),
//                    (Math.PI * 0.16666667, MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 0.25000000, MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 0.33333333, MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 0.41666667, 1.0),
//                    (Math.PI * 0.50000000, 1.0),
//                    (Math.PI * 0.58333333, 1.0),
//                    (Math.PI * 0.66666667, MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 0.75000000, MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 0.83333333, MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 0.91666667, 0.0),
//                    (Math.PI * 1.00000000, 0.0),
//                    (Math.PI * 1.08333333, 0.0),
//                    (Math.PI * 1.16666667, -MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 1.25000000, -MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 1.33333333, -MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 1.41666667, -1.0),
//                    (Math.PI * 1.50000000, -1.0),
//                    (Math.PI * 1.58333333, -1.0),
//                    (Math.PI * 1.66666667, -MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 1.75000000, -MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 1.83333333, -MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 1.91666667, 0.0),
//                    (Math.PI * 2.00000000, 0.0),
//                    (Math.PI * 2.08333333, 0.0)
//                });

//        [TestMethod]
//        public void Test_Synthesizer_Interpolate_Line_LagBehind_Forward_StartPosition0_WithCalculatorClasses()
//            => Test_Synthesizer_Interpolate_Line_LagBehind_Forward_StartPosition0(
//                CalculationEngineEnum.CalculatorClasses);

//        private void Test_Synthesizer_Interpolate_Line_LagBehind_Forward_StartPosition0(
//            CalculationEngineEnum calculationEngineEnum)
//            => Test_Synthesizer_Interpolate_Base(
//                calculationEngineEnum,
//                InterpolationTypeEnum.Line,
//                FollowingModeEnum.LagBehind,
//                rate: 8.0 / MathHelper.TWO_PI,
//                new[]
//                {
//                    (Math.PI * 0.00, 0.0),
//                    (Math.PI * 0.25, MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 0.50, 1.0),
//                    (Math.PI * 0.75, MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 1.00, 0.0),
//                    (Math.PI * 1.25, -MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 1.50, -1.0),
//                    (Math.PI * 1.75, -MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 2.00, 0.0)
//                });

//        [TestMethod]
//        public void Test_Synthesizer_Interpolate_Line_LagBehind_Forward_StartPositionNegative_WithCalculatorClasses()
//            => Test_Synthesizer_Interpolate_Line_LagBehind_Forward_StartPositionNegative(
//                CalculationEngineEnum.CalculatorClasses);

//        private void Test_Synthesizer_Interpolate_Line_LagBehind_Forward_StartPositionNegative(
//            CalculationEngineEnum calculationEngineEnum)
//            => Test_Synthesizer_Interpolate_Base(
//                calculationEngineEnum,
//                InterpolationTypeEnum.Line,
//                FollowingModeEnum.LagBehind,
//                rate: 12.0 / MathHelper.TWO_PI,
//                new[]
//                {
//                    (Math.PI * 0.00, 0.0),
//                    (Math.PI * 0.25, MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 0.50, 1.0),
//                    (Math.PI * 0.75, MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 1.00, 0.0),
//                    (Math.PI * 1.25, -MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 1.50, -1.0),
//                    (Math.PI * 1.75, -MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 2.00, 0.0)
//                });

//        [TestMethod]
//        public void Test_Synthesizer_Interpolate_Line_LagBehind_Backward_StartPositionPositive_WithCalculatorClasses()
//            => Test_Synthesizer_Interpolate_Line_LagBehind_Backward_StartPositionPositive(
//                CalculationEngineEnum.CalculatorClasses);

//        private void Test_Synthesizer_Interpolate_Line_LagBehind_Backward_StartPositionPositive(
//            CalculationEngineEnum calculationEngineEnum)
//            => Test_Synthesizer_Interpolate_Base(
//                calculationEngineEnum,
//                InterpolationTypeEnum.Line,
//                FollowingModeEnum.LagBehind,
//                rate: 12.0 / MathHelper.TWO_PI,
//                new[]
//                {
//                    (Math.PI * 0.00, 0.0),
//                    (Math.PI * 0.25, MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 0.50, 1.0),
//                    (Math.PI * 0.75, MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 1.00, 0.0),
//                    (Math.PI * 1.25, -MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 1.50, -1.0),
//                    (Math.PI * 1.75, -MathHelper.SQRT_2 / 2.0),
//                    (Math.PI * 2.00, 0.0)
//                });

//        private void Test_Synthesizer_Interpolate_Base(
//            CalculationEngineEnum calculationEngineEnum,
//            InterpolationTypeEnum interpolationTypeEnum,
//            FollowingModeEnum followingModeEnum,
//            double rate,
//            IList<(double dimensionValue, double outputValue)> points)
//        {
//            // Transform this method's input into something the TestExecutor wants.
//            IList<DimensionEnum> inputDimensionEnums = new[] { TestConstants.DEFAULT_DIMENSION_ENUM, DimensionEnum.Rate };
//            IList<(double, double)> inputTuples = points.Select(point => (point.dimensionValue, rate)).ToArray();
//            IList<double> outputValues = points.Select(point => point.outputValue).ToArray();

//            TestExecutor.ExecuteTest(
//                x => x.Interpolate(
//                    x.New(nameof(SystemPatchNames.Sin), x.PatchInlet(TestConstants.DEFAULT_DIMENSION_ENUM)),
//                    x.PatchInlet(DimensionEnum.Rate),
//                    interpolationTypeEnum,
//                    TestConstants.DEFAULT_DIMENSION_ENUM,
//                    "",
//                    followingModeEnum),
//                inputDimensionEnums,
//                inputTuples,
//                outputValues,
//                calculationEngineEnum,
//                significantDigits: null,
//                decimalDigits: 6);
//        }
//    }
//}