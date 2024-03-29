﻿using System;
using JJ.Business.Synthesizer.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable UnusedVariable
// ReSharper disable AccessToModifiedClosure
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_Interpolate_Stripe_Tests : Synthesizer_Interpolate_Tests_Base
    {
        // LookAhead Forward

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LookAhead_Forward_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Stripe_LookAhead_Forward(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LookAhead_Forward(CalculationEngineEnum calculationEngineEnum)
        {
            double y0 = Math.Sin(Math.PI * -09 / 12);
            double y1 = Math.Sin(Math.PI * -06 / 12);
            double y2 = Math.Sin(Math.PI * -03 / 12);
            double y3 = Math.Sin(Math.PI * 00 / 12);
            double y4 = Math.Sin(Math.PI * 03 / 12);
            double y5 = Math.Sin(Math.PI * 06 / 12);
            double y6 = Math.Sin(Math.PI * 09 / 12);
            double y7 = Math.Sin(Math.PI * 12 / 12);
            double y8 = Math.Sin(Math.PI * 15 / 12);

            Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LookAhead,
                slowRate: 4.0 / Math.PI,
                new[]
                {
                    (Math.PI * -09 / 12, y0),
                    (Math.PI * -08 / 12, y0),
                    (Math.PI * -07 / 12, y1),
                    (Math.PI * -06 / 12, y1),
                    (Math.PI * -05 / 12, y1),
                    (Math.PI * -04 / 12, y2),
                    (Math.PI * -03 / 12, y2),
                    (Math.PI * -02 / 12, y2),
                    (Math.PI * -01 / 12, y3),
                    (Math.PI * 00 / 12, y3),
                    (Math.PI * 01 / 12, y3),
                    (Math.PI * 02 / 12, y4),
                    (Math.PI * 03 / 12, y4),
                    (Math.PI * 04 / 12, y4),
                    (Math.PI * 05 / 12, y5),
                    (Math.PI * 06 / 12, y5),
                    (Math.PI * 07 / 12, y5),
                    (Math.PI * 08 / 12, y6),
                    (Math.PI * 09 / 12, y6),
                    (Math.PI * 10 / 12, y6),
                    (Math.PI * 11 / 12, y7),
                    (Math.PI * 12 / 12, y7),
                    (Math.PI * 13 / 12, y7),
                    (Math.PI * 14 / 12, y8),
                    (Math.PI * 15 / 12, y8)
                });
        }

        // LookAhead Backward

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LookAhead_Backward_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Stripe_LookAhead_Backward(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LookAhead_Backward(CalculationEngineEnum calculationEngineEnum)
        {
            double y0 = Math.Sin(Math.PI * 09 / 12);
            double y1 = Math.Sin(Math.PI * 06 / 12);
            double y2 = Math.Sin(Math.PI * 03 / 12);
            double y3 = Math.Sin(Math.PI * 00 / 12);
            double y4 = Math.Sin(Math.PI * -03 / 12);
            double y5 = Math.Sin(Math.PI * -06 / 12);
            double y6 = Math.Sin(Math.PI * -09 / 12);
            double y7 = Math.Sin(Math.PI * -12 / 12);
            double y8 = Math.Sin(Math.PI * -15 / 12);

            Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LookAhead,
                slowRate: 4.0 / Math.PI,
                new[]
                {
                    (Math.PI * 09 / 12, y0),
                    (Math.PI * 08 / 12, y0),
                    (Math.PI * 07 / 12, y1),
                    (Math.PI * 06 / 12, y1),
                    (Math.PI * 05 / 12, y1),
                    (Math.PI * 04 / 12, y2),
                    (Math.PI * 03 / 12, y2),
                    (Math.PI * 02 / 12, y2),
                    (Math.PI * 01 / 12, y3),
                    (Math.PI * 00 / 12, y3),
                    (Math.PI * -01 / 12, y3),
                    (Math.PI * -02 / 12, y4),
                    (Math.PI * -03 / 12, y4),
                    (Math.PI * -04 / 12, y4),
                    (Math.PI * -05 / 12, y5),
                    (Math.PI * -06 / 12, y5),
                    (Math.PI * -07 / 12, y5),
                    (Math.PI * -08 / 12, y6),
                    (Math.PI * -09 / 12, y6),
                    (Math.PI * -10 / 12, y6),
                    (Math.PI * -11 / 12, y7),
                    (Math.PI * -12 / 12, y7),
                    (Math.PI * -13 / 12, y7),
                    (Math.PI * -14 / 12, y8),
                    (Math.PI * -15 / 12, y8)
                });
        }

        // LagBehind Forward

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LagBehind_Forward_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Stripe_LagBehind_Forward(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LagBehind_Forward(CalculationEngineEnum calculationEngineEnum)
        {
            double y0 = Math.Sin(Math.PI * -09 / 12);
            double y1 = Math.Sin(Math.PI * -07 / 12);
            double y2 = Math.Sin(Math.PI * -04 / 12);
            double y3 = Math.Sin(Math.PI * -01 / 12);
            double y4 = Math.Sin(Math.PI * 02 / 12);
            double y5 = Math.Sin(Math.PI * 05 / 12);
            double y6 = Math.Sin(Math.PI * 08 / 12);
            double y7 = Math.Sin(Math.PI * 11 / 12);
            double y8 = Math.Sin(Math.PI * 14 / 12);

            Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LagBehind,
                4.0 / Math.PI,
                new[]
                {
                    (Math.PI * -09 / 12, y0),
                    (Math.PI * -08 / 12, y0),
                    (Math.PI * -07 / 12, y1),
                    (Math.PI * -06 / 12, y1),
                    (Math.PI * -05 / 12, y1),
                    (Math.PI * -04 / 12, y2),
                    (Math.PI * -03 / 12, y2),
                    (Math.PI * -02 / 12, y2),
                    (Math.PI * -01 / 12, y3),
                    (Math.PI * 00 / 12, y3),
                    (Math.PI * 01 / 12, y3),
                    (Math.PI * 02 / 12, y4),
                    (Math.PI * 03 / 12, y4),
                    (Math.PI * 04 / 12, y4),
                    (Math.PI * 05 / 12, y5),
                    (Math.PI * 06 / 12, y5),
                    (Math.PI * 07 / 12, y5),
                    (Math.PI * 08 / 12, y6),
                    (Math.PI * 09 / 12, y6),
                    (Math.PI * 10 / 12, y6),
                    (Math.PI * 11 / 12, y7),
                    (Math.PI * 12 / 12, y7),
                    (Math.PI * 13 / 12, y7),
                    (Math.PI * 14 / 12, y8),
                    (Math.PI * 15 / 12, y8)
                });
        }

        // LagBehind Backward

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LagBehind_Backward_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Stripe_LagBehind_Backward(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LagBehind_Backward(CalculationEngineEnum calculationEngineEnum)
        {
            double y0 = Math.Sin(Math.PI * 09 / 12);
            double y1 = Math.Sin(Math.PI * 07 / 12);
            double y2 = Math.Sin(Math.PI * 04 / 12);
            double y3 = Math.Sin(Math.PI * 01 / 12);
            double y4 = Math.Sin(Math.PI * -02 / 12);
            double y5 = Math.Sin(Math.PI * -05 / 12);
            double y6 = Math.Sin(Math.PI * -08 / 12);
            double y7 = Math.Sin(Math.PI * -11 / 12);
            double y8 = Math.Sin(Math.PI * -14 / 12);

            Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LagBehind,
                slowRate: 4.0 / Math.PI,
                new[]
                {
                    (Math.PI * 09 / 12, y0),
                    (Math.PI * 08 / 12, y0),
                    (Math.PI * 07 / 12, y1),
                    (Math.PI * 06 / 12, y1),
                    (Math.PI * 05 / 12, y1),
                    (Math.PI * 04 / 12, y2),
                    (Math.PI * 03 / 12, y2),
                    (Math.PI * 02 / 12, y2),
                    (Math.PI * 01 / 12, y3),
                    (Math.PI * 00 / 12, y3),
                    (Math.PI * -01 / 12, y3),
                    (Math.PI * -02 / 12, y4),
                    (Math.PI * -03 / 12, y4),
                    (Math.PI * -04 / 12, y4),
                    (Math.PI * -05 / 12, y5),
                    (Math.PI * -06 / 12, y5),
                    (Math.PI * -07 / 12, y5),
                    (Math.PI * -08 / 12, y6),
                    (Math.PI * -09 / 12, y6),
                    (Math.PI * -10 / 12, y6),
                    (Math.PI * -11 / 12, y7),
                    (Math.PI * -12 / 12, y7),
                    (Math.PI * -13 / 12, y7),
                    (Math.PI * -14 / 12, y8),
                    (Math.PI * -15 / 12, y8)
                });
        }
    }
}