using System;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;
using JJ.Framework.Testing;
using JJ.Framework.Testing.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable SuggestVarOrType_SimpleTypes

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class PatchCalculator_Interpolate_Tests
    {
        //Assert.Inconclusive("The assertions are known to not be correct yet.");

        [TestMethod]
        public void Test_PatchCalculator_Interpolate_Line_LagBehind_DimensionTime_Forward_StartPosition0()
        {
            Assert.Inconclusive("The assertions are known to not be correct yet.");

            AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(
                () =>
                {
                    using (IContext context = PersistenceHelper.CreateContext())
                    {
                        // Arrange
                        RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);
                        PatchFacade patchFacade = new PatchFacade(repositories);
                        Patch patch = patchFacade.CreatePatch();
                        OperatorFactory o = new OperatorFactory(patch, repositories);

                        const DimensionEnum dimensionEnum = DimensionEnum.Time;

                        var curve = o.Curve(
                            dimensionEnum,
                            "",
                            (0.5, 1, InterpolationTypeEnum.Block),
                            (1.5, -1, InterpolationTypeEnum.Block),
                            (2.5, 2, InterpolationTypeEnum.Block),
                            (3.5, -2, InterpolationTypeEnum.Block));

                        var interpolate = o.Interpolate(
                            curve,
                            o.Number(1),
                            InterpolationTypeEnum.Line,
                            dimensionEnum,
                            "",
                            FollowingModeEnum.LagBehind);

                        var buffer = new float[1];
                        IPatchCalculator calculator = patchFacade.CreateCalculator(interpolate, 10, 1, 0, new CalculatorCache());

                        // Execute
                        var values = new float[9];
                        double x = 0;
                        calculator.Reset(x);

                        const double dx = 0.5;

                        for (var i = 0; i < values.Length; i++)
                        {
                            Array.Clear(buffer, 0, buffer.Length);
                            calculator.SetValue(dimensionEnum, x);
                            calculator.Calculate(buffer, buffer.Length, x);
                            values[i] = buffer[0];
                            x += dx;
                        }

                        // Assert
                        // x = 0.0
                        AssertHelper.AreEqual(1, () => values[0]);
                        // x = 0.5
                        AssertHelper.AreEqual(1, () => values[1]);
                        // x = 1.0
                        AssertHelper.AreEqual(0, () => values[2]);
                        // x = 1.5
                        AssertHelper.AreEqual(-1, () => values[3]);
                        // x = 2.0
                        AssertHelper.AreEqual(0.5, () => values[4]);
                        // x = 2.5
                        AssertHelper.AreEqual(2, () => values[5]);
                        // x = 3.0
                        AssertHelper.AreEqual(0, () => values[6]);
                        // x = 3.5
                        AssertHelper.AreEqual(-2, () => values[7]);
                        // x = 4.0
                        AssertHelper.AreEqual(-2, () => values[8]);
                    }
                });
        }

        [TestMethod]
        public void Test_PatchCalculator_Interpolate_Line_LagBehind_DimensionTime_Forward_StartPositionNegative()
        {
            Assert.Inconclusive("The assertions are known to not be correct yet.");

            AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(
                () =>
                {
                    using (IContext context = PersistenceHelper.CreateContext())
                    {
                        // Arrange
                        RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);
                        PatchFacade patchFacade = new PatchFacade(repositories);
                        Patch patch = patchFacade.CreatePatch();
                        OperatorFactory o = new OperatorFactory(patch, repositories);

                        const DimensionEnum dimensionEnum = DimensionEnum.Time;

                        var curve = o.Curve(
                            dimensionEnum,
                            "",
                            (-1.5, 1, InterpolationTypeEnum.Block),
                            (-0.5, -1, InterpolationTypeEnum.Block),
                            (0.5, 2, InterpolationTypeEnum.Block),
                            (1.5, -2, InterpolationTypeEnum.Block));

                        var interpolate = o.Interpolate(
                            curve,
                            o.Number(1),
                            InterpolationTypeEnum.Line,
                            dimensionEnum,
                            "",
                            FollowingModeEnum.LagBehind);

                        var buffer = new float[1];
                        IPatchCalculator calculator = patchFacade.CreateCalculator(interpolate, 10, 1, 0, new CalculatorCache());

                        // Execute
                        var values = new float[9];
                        double x = -2;
                        calculator.Reset(x);

                        const double dx = 0.5;

                        for (var i = 0; i < values.Length; i++)
                        {
                            Array.Clear(buffer, 0, buffer.Length);
                            calculator.SetValue(dimensionEnum, x);
                            calculator.Calculate(buffer, buffer.Length, x);
                            values[i] = buffer[0];
                            x += dx;
                        }

                        // Assert
                        AssertHelper.AreEqual(1, () => values[0]);
                        AssertHelper.AreEqual(1, () => values[1]);
                        AssertHelper.AreEqual(0, () => values[2]);
                        AssertHelper.AreEqual(-1, () => values[3]);
                        AssertHelper.AreEqual(0.5, () => values[4]);
                        AssertHelper.AreEqual(2, () => values[5]);
                        AssertHelper.AreEqual(0, () => values[6]);
                        AssertHelper.AreEqual(-2, () => values[7]);
                        AssertHelper.AreEqual(-2, () => values[8]);
                    }
                });
        }

        [TestMethod]
        public void Test_PatchCalculator_Interpolate_Line_Backward_StartPositionNegative()
        {
            Assert.Inconclusive("The assertions are known to not be correct yet.");

            AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(
                () =>
                {
                    using (IContext context = PersistenceHelper.CreateContext())
                    {
                        // Arrange
                        RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);
                        PatchFacade patchFacade = new PatchFacade(repositories);
                        Patch patch = patchFacade.CreatePatch();
                        OperatorFactory o = new OperatorFactory(patch, repositories);

                        const DimensionEnum dimensionEnum = DimensionEnum.Number;

                        var curve = o.Curve(
                            dimensionEnum,
                            "",
                            (-1.5, 1, InterpolationTypeEnum.Block),
                            (-0.5, -1, InterpolationTypeEnum.Block),
                            (0.5, 2, InterpolationTypeEnum.Block),
                            (1.5, -2, InterpolationTypeEnum.Block));

                        var interpolate = o.Interpolate(curve, o.Number(1), InterpolationTypeEnum.Line, dimensionEnum);

                        var buffer = new float[1];

                        // Execute
                        IPatchCalculator calculator = patchFacade.CreateCalculator(interpolate, 44800, 1, 0, new CalculatorCache());

                        var values = new float[9];
                        double x = 3;
                        calculator.Reset(x);

                        for (var i = 0; i < values.Length; i++)
                        {
                            Array.Clear(buffer, 0, buffer.Length);
                            calculator.SetValue(dimensionEnum, x);
                            calculator.Calculate(buffer, buffer.Length, x);
                            values[i] = buffer[0];
                            x -= 0.5;
                        }

                        // Assert
                        AssertHelper.AreEqual(-2, () => values[0]);
                        AssertHelper.AreEqual(-2, () => values[1]);
                        AssertHelper.AreEqual(0, () => values[2]);
                        AssertHelper.AreEqual(2, () => values[3]);
                        AssertHelper.AreEqual(0.5, () => values[4]);
                        AssertHelper.AreEqual(-1, () => values[5]);
                        AssertHelper.AreEqual(0, () => values[6]);
                        AssertHelper.AreEqual(1, () => values[7]);
                        AssertHelper.AreEqual(1, () => values[8]);
                    }
                });
        }
    }
}