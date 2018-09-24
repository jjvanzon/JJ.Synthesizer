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
    public class Synthesizer_Interpolate_Line_Tests : Synthesizer_Interpolate_Tests_Base
    {
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