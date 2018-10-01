using System;
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

        private void Test_Synthesizer_Interpolate_Stripe_LookAhead_Forward(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LookAhead,
                slowRate: 4.0 / Math.PI,
                new[]
                {
                    (Math.PI * -12 / 12, Math.Sin(Math.PI * -12 / 12)),
                    (Math.PI * -11 / 12, Math.Sin(Math.PI * -12 / 12)),
                    (Math.PI * -10 / 12, Math.Sin(Math.PI * -9 / 12)),
                    (Math.PI * -09 / 12, Math.Sin(Math.PI * -9 / 12)),
                    (Math.PI * -08 / 12, Math.Sin(Math.PI * -9 / 12)),
                    (Math.PI * -07 / 12, Math.Sin(Math.PI * -6 / 12)),
                    (Math.PI * -06 / 12, Math.Sin(Math.PI * -6 / 12)),
                    (Math.PI * -05 / 12, Math.Sin(Math.PI * -6 / 12)),
                    (Math.PI * -04 / 12, Math.Sin(Math.PI * -3 / 12)),
                    (Math.PI * -03 / 12, Math.Sin(Math.PI * -3 / 12)),
                    (Math.PI * -02 / 12, Math.Sin(Math.PI * -3 / 12)),
                    (Math.PI * -01 / 12, Math.Sin(Math.PI * 0 / 12)),
                    (Math.PI * 00 / 12, Math.Sin(Math.PI * 0 / 12)),
                    (Math.PI * 01 / 12, Math.Sin(Math.PI * 0 / 12)),
                    (Math.PI * 02 / 12, Math.Sin(Math.PI * 3 / 12)),
                    (Math.PI * 03 / 12, Math.Sin(Math.PI * 3 / 12)),
                    (Math.PI * 04 / 12, Math.Sin(Math.PI * 3 / 12)),
                    (Math.PI * 05 / 12, Math.Sin(Math.PI * 6 / 12)),
                    (Math.PI * 06 / 12, Math.Sin(Math.PI * 6 / 12)),
                    (Math.PI * 07 / 12, Math.Sin(Math.PI * 6 / 12)),
                    (Math.PI * 08 / 12, Math.Sin(Math.PI * 9 / 12)),
                    (Math.PI * 09 / 12, Math.Sin(Math.PI * 9 / 12)),
                    (Math.PI * 10 / 12, Math.Sin(Math.PI * 9 / 12)),
                    (Math.PI * 11 / 12, Math.Sin(Math.PI * 12 / 12)),
                    (Math.PI * 12 / 12, Math.Sin(Math.PI * 12 / 12))
                });

        // LagBehind Forward

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LagBehind_Forward_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Stripe_LagBehind_Forward(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LagBehind_Forward(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LagBehind,
                4.0 / Math.PI,
                new[]
                {
                    (Math.PI * -12 / 12, Math.Sin(Math.PI * -12 / 12)),
                    (Math.PI * -11 / 12, Math.Sin(Math.PI * -12 / 12)),
                    (Math.PI * -10 / 12, Math.Sin(Math.PI * -10 / 12)),
                    (Math.PI * -09 / 12, Math.Sin(Math.PI * -10 / 12)),
                    (Math.PI * -08 / 12, Math.Sin(Math.PI * -10 / 12)),
                    (Math.PI * -07 / 12, Math.Sin(Math.PI * -07 / 12)),
                    (Math.PI * -06 / 12, Math.Sin(Math.PI * -07 / 12)),
                    (Math.PI * -05 / 12, Math.Sin(Math.PI * -07 / 12)),
                    (Math.PI * -04 / 12, Math.Sin(Math.PI * -04 / 12)),
                    (Math.PI * -03 / 12, Math.Sin(Math.PI * -04 / 12)),
                    (Math.PI * -02 / 12, Math.Sin(Math.PI * -04 / 12)),
                    (Math.PI * -01 / 12, Math.Sin(Math.PI * -01 / 12)),
                    (Math.PI * 00 / 12, Math.Sin(Math.PI * -01 / 12)),
                    (Math.PI * 01 / 12, Math.Sin(Math.PI * -01 / 12)),
                    (Math.PI * 02 / 12, Math.Sin(Math.PI * 02 / 12)),
                    (Math.PI * 03 / 12, Math.Sin(Math.PI * 02 / 12)),
                    (Math.PI * 04 / 12, Math.Sin(Math.PI * 02 / 12)),
                    (Math.PI * 05 / 12, Math.Sin(Math.PI * 05 / 12)),
                    (Math.PI * 06 / 12, Math.Sin(Math.PI * 05 / 12)),
                    (Math.PI * 07 / 12, Math.Sin(Math.PI * 05 / 12)),
                    (Math.PI * 08 / 12, Math.Sin(Math.PI * 08 / 12)),
                    (Math.PI * 09 / 12, Math.Sin(Math.PI * 08 / 12)),
                    (Math.PI * 10 / 12, Math.Sin(Math.PI * 08 / 12)),
                    (Math.PI * 11 / 12, Math.Sin(Math.PI * 11 / 12)),
                    (Math.PI * 12 / 12, Math.Sin(Math.PI * 11 / 12))
                });

        // LookAhead Backward

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LookAhead_Backward_StartPositionPositive()
            => Test_Synthesizer_Interpolate_Stripe_LookAhead_Backward(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LookAhead_Backward(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LookAhead,
                slowRate: 4.0 / Math.PI,
                new[]
                {
                    (Math.PI * 06 / 12, Math.Sin(Math.PI * 6 / 12)),
                    (Math.PI * 05 / 12, Math.Sin(Math.PI * 6 / 12)),
                    (Math.PI * 04 / 12, Math.Sin(Math.PI * 3 / 12)),
                    (Math.PI * 03 / 12, Math.Sin(Math.PI * 3 / 12)),
                    (Math.PI * 02 / 12, Math.Sin(Math.PI * 3 / 12)),
                    (Math.PI * 01 / 12, Math.Sin(Math.PI * 0 / 12)),
                    (Math.PI * 00 / 12, Math.Sin(Math.PI * 0 / 12)),
                    (Math.PI * -01 / 12, Math.Sin(Math.PI * 0 / 12)),
                    (Math.PI * -02 / 12, Math.Sin(Math.PI * -3 / 12)),
                    (Math.PI * -03 / 12, Math.Sin(Math.PI * -3 / 12)),
                    (Math.PI * -04 / 12, Math.Sin(Math.PI * -3 / 12)),
                    (Math.PI * -05 / 12, Math.Sin(Math.PI * -6 / 12)),
                    (Math.PI * -06 / 12, Math.Sin(Math.PI * -6 / 12)),
                    (Math.PI * -07 / 12, Math.Sin(Math.PI * -6 / 12)),
                    (Math.PI * -08 / 12, Math.Sin(Math.PI * -9 / 12)),
                    (Math.PI * -09 / 12, Math.Sin(Math.PI * -9 / 12)),
                    (Math.PI * -10 / 12, Math.Sin(Math.PI * -9 / 12)),
                    (Math.PI * -11 / 12, Math.Sin(Math.PI * -12 / 12)),
                    (Math.PI * -12 / 12, Math.Sin(Math.PI * -12 / 12)),
                    (Math.PI * -13 / 12, Math.Sin(Math.PI * -12 / 12)),
                    (Math.PI * -14 / 12, Math.Sin(Math.PI * -15 / 12)),
                    (Math.PI * -15 / 12, Math.Sin(Math.PI * -15 / 12)),
                    (Math.PI * -16 / 12, Math.Sin(Math.PI * -15 / 12)),
                    (Math.PI * -17 / 12, Math.Sin(Math.PI * -18 / 12)),
                    (Math.PI * -18 / 12, Math.Sin(Math.PI * -18 / 12))
                });

        // LagBehind Backward

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LagBehind_Backward_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Stripe_LagBehind_Backward(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LagBehind_Backward(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LagBehind,
                slowRate: 4.0 / Math.PI,
                new[]
                {
                    (Math.PI * 06 / 12, Math.Sin(Math.PI * 06 / 12)),
                    (Math.PI * 05 / 12, Math.Sin(Math.PI * 06 / 12)),
                    (Math.PI * 04 / 12, Math.Sin(Math.PI * 04 / 12)),
                    (Math.PI * 03 / 12, Math.Sin(Math.PI * 04 / 12)),
                    (Math.PI * 02 / 12, Math.Sin(Math.PI * 04 / 12)),
                    (Math.PI * 01 / 12, Math.Sin(Math.PI * 01 / 12)),
                    (Math.PI * 00 / 12, Math.Sin(Math.PI * 01 / 12)),
                    (Math.PI * -01 / 12, Math.Sin(Math.PI * 01 / 12)),
                    (Math.PI * -02 / 12, Math.Sin(Math.PI * -02 / 12)),
                    (Math.PI * -03 / 12, Math.Sin(Math.PI * -02 / 12)),
                    (Math.PI * -04 / 12, Math.Sin(Math.PI * -02 / 12)),
                    (Math.PI * -05 / 12, Math.Sin(Math.PI * -05 / 12)),
                    (Math.PI * -06 / 12, Math.Sin(Math.PI * -05 / 12)),
                    (Math.PI * -07 / 12, Math.Sin(Math.PI * -05 / 12)),
                    (Math.PI * -08 / 12, Math.Sin(Math.PI * -08 / 12)),
                    (Math.PI * -09 / 12, Math.Sin(Math.PI * -08 / 12)),
                    (Math.PI * -10 / 12, Math.Sin(Math.PI * -08 / 12)),
                    (Math.PI * -11 / 12, Math.Sin(Math.PI * -11 / 12)),
                    (Math.PI * -12 / 12, Math.Sin(Math.PI * -11 / 12)),
                    (Math.PI * -13 / 12, Math.Sin(Math.PI * -11 / 12)),
                    (Math.PI * -14 / 12, Math.Sin(Math.PI * -14 / 12)),
                    (Math.PI * -15 / 12, Math.Sin(Math.PI * -14 / 12)),
                    (Math.PI * -16 / 12, Math.Sin(Math.PI * -14 / 12)),
                    (Math.PI * -17 / 12, Math.Sin(Math.PI * -17 / 12)),
                    (Math.PI * -18 / 12, Math.Sin(Math.PI * -17 / 12))
                });
    }
}