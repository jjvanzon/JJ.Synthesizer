using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable UnusedVariable
// ReSharper disable AccessToModifiedClosure
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_Interpolate_Line_Tests : Synthesizer_Interpolate_Tests_Base
    {
        // LookAhead

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Line_LookAhead_Forward_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Line_LookAhead_Forward(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Line_LookAhead_Forward(CalculationEngineEnum calculationEngineEnum)
        {
            const double y0 = 0;
            const double y1 = -MathHelper.SQRT_2 / 2.0;
            const double y2 = -1;
            const double y3 = -MathHelper.SQRT_2 / 2.0;
            const double y4 = 0;
            const double y5 = MathHelper.SQRT_2 / 2.0;
            const double y6 = 1;
            const double y7 = MathHelper.SQRT_2 / 2.0;
            const double y8 = 0;

            Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Line,
                FollowingModeEnum.LookAhead,
                rate: 4.0 / Math.PI,
                new[]
                {
                    (Math.PI * -12 / 12, y0),
                    (Math.PI * -11 / 12, (2 * y0 + y1) / 3),
                    (Math.PI * -10 / 12, (y0 + 2 * y1) / 3),
                    (Math.PI * -09 / 12, y1),
                    (Math.PI * -08 / 12, (2 * y1 + y2) / 3),
                    (Math.PI * -07 / 12, (y1 + 2 * y2) / 3),
                    (Math.PI * -06 / 12, y2),
                    (Math.PI * -05 / 12, (2 * y2 + y3) / 3),
                    (Math.PI * -04 / 12, (y2 + 2 * y3) / 3),
                    (Math.PI * -03 / 12, y3),
                    (Math.PI * -02 / 12, (2 * y3 + y4) / 3),
                    (Math.PI * -01 / 12, (y3 + 2 * y4) / 3),
                    (Math.PI * 00 / 12, y4),
                    (Math.PI * 01 / 12, (2 * y4 + y5) / 3),
                    (Math.PI * 02 / 12, (y4 + 2 * y5) / 3),
                    (Math.PI * 03 / 12, y5),
                    (Math.PI * 04 / 12, (2 * y5 + y6) / 3),
                    (Math.PI * 05 / 12, (y5 + 2 * y6) / 3),
                    (Math.PI * 06 / 12, y6),
                    (Math.PI * 07 / 12, (2 * y6 + y7) / 3),
                    (Math.PI * 08 / 12, (y6 + 2 * y7) / 3),
                    (Math.PI * 09 / 12, y7),
                    (Math.PI * 10 / 12, (2 * y7 + y8) / 3),
                    (Math.PI * 11 / 12, (y7 + 2 * y8) / 3),
                    (Math.PI * 12 / 12, y8)
                });
        }

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Line_LookAhead_Backward_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Line_LookAhead_Backward(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Line_LookAhead_Backward(CalculationEngineEnum calculationEngineEnum)
        {
            const double y0 = 0;
            const double y1 = MathHelper.SQRT_2 / 2.0;
            const double y2 = 1;
            const double y3 = MathHelper.SQRT_2 / 2.0;
            const double y4 = 0;
            const double y5 = -MathHelper.SQRT_2 / 2.0;
            const double y6 = -1;
            const double y7 = -MathHelper.SQRT_2 / 2.0;
            const double y8 = 0;

            Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Line,
                FollowingModeEnum.LookAhead,
                rate: 4.0 / Math.PI,
                new[]
                {
                    (Math.PI * 12 / 12, y0),
                    (Math.PI * 11 / 12, (2 * y0 + y1) / 3),
                    (Math.PI * 10 / 12, (y0 + 2 * y1) / 3),
                    (Math.PI * 09 / 12, y1),
                    (Math.PI * 08 / 12, (2 * y1 + y2) / 3),
                    (Math.PI * 07 / 12, (y1 + 2 * y2) / 3),
                    (Math.PI * 06 / 12, y2),
                    (Math.PI * 05 / 12, (2 * y2 + y3) / 3),
                    (Math.PI * 04 / 12, (y2 + 2 * y3) / 3),
                    (Math.PI * 03 / 12, y3),
                    (Math.PI * 02 / 12, (2 * y3 + y4) / 3),
                    (Math.PI * 01 / 12, (y3 + 2 * y4) / 3),
                    (Math.PI * 00 / 12, y4),
                    (Math.PI * -01 / 12, (2 * y4 + y5) / 3),
                    (Math.PI * -02 / 12, (y4 + 2 * y5) / 3),
                    (Math.PI * -03 / 12, y5),
                    (Math.PI * -04 / 12, (2 * y5 + y6) / 3),
                    (Math.PI * -05 / 12, (y5 + 2 * y6) / 3),
                    (Math.PI * -06 / 12, y6),
                    (Math.PI * -07 / 12, (2 * y6 + y7) / 3),
                    (Math.PI * -08 / 12, (y6 + 2 * y7) / 3),
                    (Math.PI * -09 / 12, y7),
                    (Math.PI * -10 / 12, (2 * y7 + y8) / 3),
                    (Math.PI * -11 / 12, (y7 + 2 * y8) / 3),
                    (Math.PI * -12 / 12, y8)
                });
        }

        // LagBehind

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Line_LagBehind_Forward_StartPosition0_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Line_LagBehind_Forward_StartPosition0(
                CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Line_LagBehind_Forward_StartPosition0(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Line,
                FollowingModeEnum.LagBehind,
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
        public void Test_Synthesizer_Interpolate_Line_LagBehind_Forward_StartPositionNegative_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Line_LagBehind_Forward_StartPositionNegative(
                CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Line_LagBehind_Forward_StartPositionNegative(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Line,
                FollowingModeEnum.LagBehind,
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
        public void Test_Synthesizer_Interpolate_Line_LagBehind_Backward_StartPositionPositive_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Line_LagBehind_Backward_StartPositionPositive(
                CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Line_LagBehind_Backward_StartPositionPositive(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Line,
                FollowingModeEnum.LagBehind,
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
    }
}