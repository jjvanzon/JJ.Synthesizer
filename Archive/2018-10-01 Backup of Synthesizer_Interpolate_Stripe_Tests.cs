﻿using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable UnusedVariable
// ReSharper disable AccessToModifiedClosure
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_Interpolate_Stripe_Tests : Synthesizer_Interpolate_Tests_Base
    {
        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LookAhead_Forward_StartPosition0_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Stripe_LookAhead_Forward_StartPosition0(
                CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LookAhead_Forward_StartPosition0(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LookAhead,
                slowRate: 4.0 / Math.PI,
                new[]
                {
                    (Math.PI * 00 / 12, 0.0),
                    (Math.PI * 01 / 12, 0.0),
                    (Math.PI * 02 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 03 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 04 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 05 / 12, 1.0),
                    (Math.PI * 06 / 12, 1.0),
                    (Math.PI * 07 / 12, 1.0),
                    (Math.PI * 08 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 09 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 10 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 11 / 12, 0.0),
                    (Math.PI * 12 / 12, 0.0),
                    (Math.PI * 13 / 12, 0.0),
                    (Math.PI * 14 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 15 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 16 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 17 / 12, -1.0),
                    (Math.PI * 18 / 12, -1.0),
                    (Math.PI * 19 / 12, -1.0),
                    (Math.PI * 20 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 21 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 22 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 23 / 12, 0.0),
                    (Math.PI * 24 / 12, 0.0),
                    (Math.PI * 25 / 12, 0.0)
                });

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LookAhead_Forward_StartPositionNegative_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Stripe_LookAhead_Forward_StartPositionNegative(
                CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LookAhead_Forward_StartPositionNegative(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LookAhead,
                slowRate: 4.0 / Math.PI,
                new[]
                {
                    (Math.PI * -12 / 12, 0.0),
                    (Math.PI * -11 / 12, 0.0),
                    (Math.PI * -10 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -09 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -08 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -07 / 12, -1.0),
                    (Math.PI * -06 / 12, -1.0),
                    (Math.PI * -05 / 12, -1.0),
                    (Math.PI * -04 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -03 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -02 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -01 / 12, 0.0),
                    (Math.PI * 00 / 12, 0.0),
                    (Math.PI * 01 / 12, 0.0),
                    (Math.PI * 02 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 03 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 04 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 05 / 12, 1.0),
                    (Math.PI * 06 / 12, 1.0),
                    (Math.PI * 07 / 12, 1.0),
                    (Math.PI * 08 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 09 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 10 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 11 / 12, 0.0),
                    (Math.PI * 12 / 12, 0.0),
                    (Math.PI * 13 / 12, 0.0)
                });

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LookAhead_Forward_StartPositionPositive_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Stripe_LookAhead_Forward_StartPositionPositive(
                CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LookAhead_Forward_StartPositionPositive(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LookAhead,
                slowRate: 4.0 / Math.PI,
                new[]
                {
                    (Math.PI * 06 / 12, 1.0),
                    (Math.PI * 07 / 12, 1.0),
                    (Math.PI * 08 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 09 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 10 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 11 / 12, 0.0),
                    (Math.PI * 12 / 12, 0.0),
                    (Math.PI * 13 / 12, 0.0),
                    (Math.PI * 14 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 15 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 16 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 17 / 12, -1.0),
                    (Math.PI * 18 / 12, -1.0),
                    (Math.PI * 19 / 12, -1.0),
                    (Math.PI * 20 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 21 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 22 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 23 / 12, 0.0),
                    (Math.PI * 24 / 12, 0.0),
                    (Math.PI * 25 / 12, 0.0),
                    (Math.PI * 26 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 27 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 28 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 29 / 12, 1.0),
                    (Math.PI * 30 / 12, 1.0),
                    (Math.PI * 31 / 12, 1.0)
                });

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LagBehind_Forward_StartPosition0_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Stripe_LagBehind_Forward_StartPosition0(
                CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LagBehind_Forward_StartPosition0(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LagBehind,
                4.0 / Math.PI,
                new[]
                {
                    (Math.PI * 00 / 12, Math.Sin(Math.PI * 00 / 12)),
                    (Math.PI * 01 / 12, Math.Sin(Math.PI * 00 / 12)),
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
                    (Math.PI * 12 / 12, Math.Sin(Math.PI * 11 / 12)),
                    (Math.PI * 13 / 12, Math.Sin(Math.PI * 11 / 12)),
                    (Math.PI * 14 / 12, Math.Sin(Math.PI * 14 / 12)),
                    (Math.PI * 15 / 12, Math.Sin(Math.PI * 14 / 12)),
                    (Math.PI * 16 / 12, Math.Sin(Math.PI * 14 / 12)),
                    (Math.PI * 17 / 12, Math.Sin(Math.PI * 17 / 12)),
                    (Math.PI * 18 / 12, Math.Sin(Math.PI * 17 / 12)),
                    (Math.PI * 19 / 12, Math.Sin(Math.PI * 17 / 12)),
                    (Math.PI * 20 / 12, Math.Sin(Math.PI * 20 / 12)),
                    (Math.PI * 21 / 12, Math.Sin(Math.PI * 20 / 12)),
                    (Math.PI * 22 / 12, Math.Sin(Math.PI * 20 / 12)),
                    (Math.PI * 23 / 12, Math.Sin(Math.PI * 23 / 12)),
                    (Math.PI * 24 / 12, Math.Sin(Math.PI * 23 / 12)),
                    (Math.PI * 25 / 12, Math.Sin(Math.PI * 23 / 12))
                });

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LagBehind_Forward_StartPositionNegative_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Stripe_LagBehind_Forward_StartPositionNegative(
                CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LagBehind_Forward_StartPositionNegative(
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
                    (Math.PI * 12 / 12, Math.Sin(Math.PI * 11 / 12)),
                    (Math.PI * 13 / 12, Math.Sin(Math.PI * 11 / 12))
                });

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LagBehind_Forward_StartPositionPositive_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Stripe_LagBehind_Forward_StartPositionPositive(
                CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LagBehind_Forward_StartPositionPositive(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LagBehind,
                slowRate: 4.0 / Math.PI,
                new[]
                {
                    (Math.PI * 06 / 12, Math.Sin(Math.PI * 06 / 12)),
                    (Math.PI * 07 / 12, Math.Sin(Math.PI * 06 / 12)),
                    (Math.PI * 08 / 12, Math.Sin(Math.PI * 08 / 12)),
                    (Math.PI * 09 / 12, Math.Sin(Math.PI * 08 / 12)),
                    (Math.PI * 10 / 12, Math.Sin(Math.PI * 08 / 12)),
                    (Math.PI * 11 / 12, Math.Sin(Math.PI * 11 / 12)),
                    (Math.PI * 12 / 12, Math.Sin(Math.PI * 11 / 12)),
                    (Math.PI * 13 / 12, Math.Sin(Math.PI * 11 / 12)),
                    (Math.PI * 14 / 12, Math.Sin(Math.PI * 14 / 12)),
                    (Math.PI * 15 / 12, Math.Sin(Math.PI * 14 / 12)),
                    (Math.PI * 16 / 12, Math.Sin(Math.PI * 14 / 12)),
                    (Math.PI * 17 / 12, Math.Sin(Math.PI * 17 / 12)),
                    (Math.PI * 18 / 12, Math.Sin(Math.PI * 17 / 12)),
                    (Math.PI * 19 / 12, Math.Sin(Math.PI * 17 / 12)),
                    (Math.PI * 20 / 12, Math.Sin(Math.PI * 20 / 12)),
                    (Math.PI * 21 / 12, Math.Sin(Math.PI * 20 / 12)),
                    (Math.PI * 22 / 12, Math.Sin(Math.PI * 20 / 12)),
                    (Math.PI * 23 / 12, Math.Sin(Math.PI * 23 / 12)),
                    (Math.PI * 24 / 12, Math.Sin(Math.PI * 23 / 12)),
                    (Math.PI * 25 / 12, Math.Sin(Math.PI * 23 / 12)),
                    (Math.PI * 26 / 12, Math.Sin(Math.PI * 26 / 12)),
                    (Math.PI * 27 / 12, Math.Sin(Math.PI * 26 / 12)),
                    (Math.PI * 28 / 12, Math.Sin(Math.PI * 26 / 12)),
                    (Math.PI * 29 / 12, Math.Sin(Math.PI * 29 / 12)),
                    (Math.PI * 30 / 12, Math.Sin(Math.PI * 29 / 12)),
                    (Math.PI * 31 / 12, Math.Sin(Math.PI * 29 / 12))
                });

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LookAhead_Backward_StartPosition0_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Stripe_LookAhead_Backward_StartPosition0(
                CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LookAhead_Backward_StartPosition0(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LookAhead,
                slowRate: 4.0 / Math.PI,
                new[]
                {
                    (Math.PI * 00 / 12, 0.0),
                    (Math.PI * -01 / 12, 0.0),
                    (Math.PI * -02 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -03 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -04 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -05 / 12, -1.0),
                    (Math.PI * -06 / 12, -1.0),
                    (Math.PI * -07 / 12, -1.0),
                    (Math.PI * -08 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -09 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -10 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -11 / 12, 0.0),
                    (Math.PI * -12 / 12, 0.0),
                    (Math.PI * -13 / 12, 0.0),
                    (Math.PI * -14 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -15 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -16 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -17 / 12, 1.0),
                    (Math.PI * -18 / 12, 1.0),
                    (Math.PI * -19 / 12, 1.0),
                    (Math.PI * -20 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -21 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -22 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -23 / 12, 0.0),
                    (Math.PI * -24 / 12, 0.0),
                    (Math.PI * -25 / 12, 0.0)
                });

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LookAhead_Backward_StartPositionNegative_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Stripe_LookAhead_Backward_StartPositionNegative(
                CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LookAhead_Backward_StartPositionNegative(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LookAhead,
                slowRate: 4.0 / Math.PI,
                new[]
                {
                    (Math.PI * -06 / 12, -1.0),
                    (Math.PI * -07 / 12, -1.0),
                    (Math.PI * -08 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -09 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -10 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -11 / 12, 0.0),
                    (Math.PI * -12 / 12, 0.0),
                    (Math.PI * -13 / 12, 0.0),
                    (Math.PI * -14 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -15 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -16 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -17 / 12, 1.0),
                    (Math.PI * -18 / 12, 1.0),
                    (Math.PI * -19 / 12, 1.0),
                    (Math.PI * -20 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -21 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -22 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -23 / 12, 0.0),
                    (Math.PI * -24 / 12, 0.0),
                    (Math.PI * -25 / 12, 0.0),
                    (Math.PI * -26 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -27 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -28 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -29 / 12, -1.0),
                    (Math.PI * -30 / 12, -1.0),
                    (Math.PI * -31 / 12, -1.0)
                });

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LookAhead_Backward_StartPositionPositive_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Stripe_LookAhead_Backward_StartPositionPositive(
                CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LookAhead_Backward_StartPositionPositive(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LookAhead,
                slowRate: 4.0 / Math.PI,
                new[]
                {
                    (Math.PI * 06 / 12, 1.0),
                    (Math.PI * 05 / 12, 1.0),
                    (Math.PI * 04 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 03 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 02 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * 01 / 12, 0.0),
                    (Math.PI * 00 / 12, 0.0),
                    (Math.PI * -01 / 12, 0.0),
                    (Math.PI * -02 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -03 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -04 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -05 / 12, -1.0),
                    (Math.PI * -06 / 12, -1.0),
                    (Math.PI * -07 / 12, -1.0),
                    (Math.PI * -08 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -09 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -10 / 12, -MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -11 / 12, 0.0),
                    (Math.PI * -12 / 12, 0.0),
                    (Math.PI * -13 / 12, 0.0),
                    (Math.PI * -14 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -15 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -16 / 12, MathHelper.SQRT_2 / 2.0),
                    (Math.PI * -17 / 12, 1.0),
                    (Math.PI * -18 / 12, 1.0),
                    (Math.PI * -19 / 12, 1.0)
                });

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LagBehind_Backward_StartPosition0_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Stripe_LagBehind_Backward_StartPosition0(
                CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LagBehind_Backward_StartPosition0(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LagBehind,
                4.0 / Math.PI,
                new[]
                {
                    (Math.PI * 00 / 12, Math.Sin(Math.PI * 00 / 12)),
                    (Math.PI * -01 / 12, Math.Sin(Math.PI * 00 / 12)),
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
                    (Math.PI * -18 / 12, Math.Sin(Math.PI * -17 / 12)),
                    (Math.PI * -19 / 12, Math.Sin(Math.PI * -17 / 12)),
                    (Math.PI * -20 / 12, Math.Sin(Math.PI * -20 / 12)),
                    (Math.PI * -21 / 12, Math.Sin(Math.PI * -20 / 12)),
                    (Math.PI * -22 / 12, Math.Sin(Math.PI * -20 / 12)),
                    (Math.PI * -23 / 12, Math.Sin(Math.PI * -23 / 12)),
                    (Math.PI * -24 / 12, Math.Sin(Math.PI * -23 / 12)),
                    (Math.PI * -25 / 12, Math.Sin(Math.PI * -23 / 12))
                });

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LagBehind_Backward_StartPositionNegative_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Stripe_LagBehind_Backward_StartPositionNegative(
                CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LagBehind_Backward_StartPositionNegative(
            CalculationEngineEnum calculationEngineEnum)
            => Test_Synthesizer_Interpolate_Base(
                calculationEngineEnum,
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LagBehind,
                4.0 / Math.PI,
                new[]
                {
                    (Math.PI * -06 / 12, Math.Sin(Math.PI * -06 / 12)),
                    (Math.PI * -07 / 12, Math.Sin(Math.PI * -06 / 12)),
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
                    (Math.PI * -18 / 12, Math.Sin(Math.PI * -17 / 12)),
                    (Math.PI * -19 / 12, Math.Sin(Math.PI * -17 / 12)),
                    (Math.PI * -20 / 12, Math.Sin(Math.PI * -20 / 12)),
                    (Math.PI * -21 / 12, Math.Sin(Math.PI * -20 / 12)),
                    (Math.PI * -22 / 12, Math.Sin(Math.PI * -20 / 12)),
                    (Math.PI * -23 / 12, Math.Sin(Math.PI * -23 / 12)),
                    (Math.PI * -24 / 12, Math.Sin(Math.PI * -23 / 12)),
                    (Math.PI * -25 / 12, Math.Sin(Math.PI * -23 / 12)),
                    (Math.PI * -26 / 12, Math.Sin(Math.PI * -26 / 12)),
                    (Math.PI * -27 / 12, Math.Sin(Math.PI * -26 / 12)),
                    (Math.PI * -28 / 12, Math.Sin(Math.PI * -26 / 12)),
                    (Math.PI * -29 / 12, Math.Sin(Math.PI * -29 / 12)),
                    (Math.PI * -30 / 12, Math.Sin(Math.PI * -29 / 12)),
                    (Math.PI * -31 / 12, Math.Sin(Math.PI * -29 / 12))
                });

        [TestMethod]
        public void Test_Synthesizer_Interpolate_Stripe_LagBehind_Backward_StartPositionPositive_WithCalculatorClasses()
            => Test_Synthesizer_Interpolate_Stripe_LagBehind_Backward_StartPositionPositive(
                CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Interpolate_Stripe_LagBehind_Backward_StartPositionPositive(
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
                    (Math.PI * -18 / 12, Math.Sin(Math.PI * -17 / 12)),
                    (Math.PI * -19 / 12, Math.Sin(Math.PI * -17 / 12))
                });
    }
}