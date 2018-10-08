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
                slowRate: 4.0 / Math.PI,
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
                slowRate: 4.0 / Math.PI,
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
        public void Test_Synthesizer_Interpolate_Line_LagBehind_Forward_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Line_LagBehind_Forward(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Line_LagBehind_Forward(
            CalculationEngineEnum calculationEngineEnum)
        {
            double y0 = Math.Sin(MathHelper.TWO_PI * -12 / 24);
            double y1 = Math.Sin(MathHelper.TWO_PI * -11 / 24);
            double y2 = Math.Sin(MathHelper.TWO_PI * -8 / 24);
            double y3 = Math.Sin(MathHelper.TWO_PI * -5 / 24);
            double y4 = Math.Sin(MathHelper.TWO_PI * -2 / 24);
            double y5 = Math.Sin(MathHelper.TWO_PI * 1 / 24);
            double y6 = Math.Sin(MathHelper.TWO_PI * 4 / 24);
            double y7 = Math.Sin(MathHelper.TWO_PI * 7 / 24);
            double y8 = Math.Sin(MathHelper.TWO_PI * 10 / 24);

            Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Line,
                FollowingModeEnum.LagBehind,
                new[]
                {
                    (-4.0, y0),
                    (-3.6666667, (2 * y0 + y1) / 3),
                    (-3.3333333, (y0 + 2 * y1) / 3),
                    (-3, y1),
                    (-2.6666667, (2 * y1 + y2) / 3),
                    (-2.3333333, (y1 + 2 * y2) / 3),
                    (-2, y2),
                    (-1.6666667, (2 * y2 + y3) / 3),
                    (-1.3333333, (y2 + 2 * y3) / 3),
                    (-1, y3),
                    (-0.6666667, (2 * y3 + y4) / 3),
                    (-0.3333333, (y3 + 2 * y4) / 3),
                    (0, y4),
                    (0.3333333, (2 * y4 + y5) / 3),
                    (0.6666667, (y4 + 2 * y5) / 3),
                    (1, y5),
                    (1.3333333, (2 * y5 + y6) / 3),
                    (1.6666667, (y5 + 2 * y6) / 3),
                    (2, y6),
                    (2.3333333, (2 * y6 + y7) / 3),
                    (2.6666667, (y6 + 2 * y7) / 3),
                    (3, y7),
                    (3.3333333, (2 * y7 + y8) / 3),
                    (3.6666667, (y7 + 2 * y8) / 3),
                    (4, y8)
                }, plotLineCount: 25);
        }
    }
}