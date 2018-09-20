using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;
using JJ.Framework.Mathematics;
using JJ.Framework.Testing.Data;
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

        private void Test_Interpolate_Stripe_LookAhead_DimensionNotTime_Forward_StartPosition0(CalculationEngineEnum calculationEngineEnum)
            => Test_Interpolate_Base(
                InterpolationTypeEnum.Stripe,
                FollowingModeEnum.LookAhead,
                DimensionEnum.Number,
                rate: 1.0 / MathHelper.TWO_PI,
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
                rate: 1.0 / MathHelper.TWO_PI,
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

        private void Test_Interpolate_Line_LagBehind_DimensionTime_Forward_StartPositionNegative(CalculationEngineEnum calculationEngineEnum)
            => Test_Interpolate_Base(
                InterpolationTypeEnum.Line,
                FollowingModeEnum.LagBehind,
                DimensionEnum.Time,
                rate: 1.0 / MathHelper.TWO_PI,
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

        private void Test_Interpolate_Line_LagBehind_DimensionNotTime_Backward_StartPositionPositive(CalculationEngineEnum calculationEngineEnum)
            => Test_Interpolate_Base(
                InterpolationTypeEnum.Line,
                FollowingModeEnum.LagBehind,
                DimensionEnum.Number,
                rate: 1.0 / MathHelper.TWO_PI,
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
            IList<(double x, double y)> expectedOutputPoints,
            CalculationEngineEnum calculationEngineEnum)
            =>
                AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(
                () =>
                {
                    using (IContext context = PersistenceHelper.CreateContext())
                    {
                        // Arrange
                        RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);
                        var patchFacade = new PatchFacade(repositories);
                        Patch patch = patchFacade.CreatePatch();
                        var o = new OperatorFactory(patch, repositories);

                        var interpolate = o.Interpolate(
                            o.New(nameof(SystemPatchNames.Sin), o.PatchInlet(dimensionEnum)),
                            o.PatchInlet(DimensionEnum.Rate),
                            interpolationTypeEnum,
                            dimensionEnum,
                            "",
                            followingModeEnum);

                        var buffer = new float[1];
                        IPatchCalculator calculator = patchFacade.CreateCalculator(interpolate, 2, 1, 0, new CalculatorCache(), calculationEngineEnum);

                        // Set Rate
                        calculator.SetValue(DimensionEnum.Rate, rate);

                        // Execute
                        var actualYs = new double[expectedOutputPoints.Count];
                        double firstX = expectedOutputPoints.First().x;
                        calculator.Reset(firstX);

                        for (var i = 0; i < expectedOutputPoints.Count; i++)
                        {
                            (double expectedX, double expectedY) = expectedOutputPoints[i];

                            Array.Clear(buffer, 0, buffer.Length);
                            calculator.SetValue(dimensionEnum, expectedX);
                            calculator.Calculate(buffer, buffer.Length, expectedX);
                            double actualY = buffer[0];
                            actualYs[i] = actualY;
                        }

                        // Assert
                        for (var i = 0; i < expectedOutputPoints.Count; i++)
                        {
                            (double expectedX, double expectedY) = expectedOutputPoints[i];
                            double actualY = actualYs[i];

                            if (Math.Abs(expectedY - actualY) > 0.00000001)
                            {
                                string message =
                                    $"Point [{i}] is expected to be ({expectedX}, {expectedY}), but it is ({expectedX}, {actualY}) instead.";

                                Assert.Fail(message);
                            }
                        }
                    }
                });
    }
}