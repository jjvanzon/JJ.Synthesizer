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
using JJ.Framework.Testing.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable UnusedVariable
// ReSharper disable InvertIf

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class ArithmeticTests
    {
        [TestMethod]
        public void Test_Synthesizer_Sin()
            => ExecuteTest(
                DimensionEnum.Number,
                x => x.New(nameof(SystemPatchNames.Sin), x.PatchInlet(DimensionEnum.Number)),
                new[]
                {
                    (Math.PI * 0.00, 0),
                    (Math.PI * 0.25, Math.Sqrt(2) / 2.0),
                    (Math.PI * 0.50, 1),
                    (Math.PI * 0.75, Math.Sqrt(2) / 2.0),
                    (Math.PI * 1.00, 0),
                    (Math.PI * 1.25, -Math.Sqrt(2) / 2.0),
                    (Math.PI * 1.50, -1),
                    (Math.PI * 1.75, -Math.Sqrt(2) / 2.0),
                    (Math.PI * 2.00, 0)
                }
            );

        [TestMethod]
        public void Test_Synthesizer_Cos()
            => ExecuteTest(
                DimensionEnum.Number,
                x => x.New(nameof(SystemPatchNames.Cos), x.PatchInlet(DimensionEnum.Number)),
                new[]
                {
                    (Math.PI * 0.00, 1),
                    (Math.PI * 0.25, Math.Sqrt(2) / 2.0),
                    (Math.PI * 0.50, 0),
                    (Math.PI * 0.75, -Math.Sqrt(2) / 2.0),
                    (Math.PI * 1.00, -1),
                    (Math.PI * 1.25, -Math.Sqrt(2) / 2.0),
                    (Math.PI * 1.50, 0),
                    (Math.PI * 1.75, Math.Sqrt(2) / 2.0),
                    (Math.PI * 2.00, 1)
                }
            );

        private void ExecuteTest(
            DimensionEnum dimensionEnum,
            Func<OperatorFactory, Outlet> createOperatorDelegate,
            IList<(double x, double y)> expectedOutputPoints,
            double precision = 0.0000001)
            => AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(
                () =>
                {
                    using (IContext context = PersistenceHelper.CreateContext())
                    {
                        // Arrange
                        RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);
                        var patchFacade = new PatchFacade(repositories);
                        Patch patch = patchFacade.CreatePatch();
                        var o = new OperatorFactory(patch, repositories);

                        Outlet outlet = createOperatorDelegate(o);

                        var buffer = new float[1];
                        IPatchCalculator calculator = patchFacade.CreateCalculator(outlet, 2, 1, 0, new CalculatorCache());

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

                            if (Math.Abs(expectedY - actualY) > precision)
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