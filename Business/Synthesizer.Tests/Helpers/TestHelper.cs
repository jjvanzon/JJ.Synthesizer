using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;
using JJ.Framework.Testing.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable UnusedVariable
// ReSharper disable InvertIf

namespace JJ.Business.Synthesizer.Tests.Helpers
{
	internal static class TestHelper
	{
        private const double DEFAULT_PRECISION = 0.00001;

        public static double CalculateOneValue(IPatchCalculator patchCalculator, double time = 0.0)
		{
			const int frameCount = 1;
			var buffer = new float[1];
			patchCalculator.Calculate(buffer, frameCount, time);
			return buffer[0];
		}

        public static void ExecuteTest(
            DimensionEnum dimensionEnum,
            Func<double, double> func,
            Func<OperatorFactory, Outlet> operatorCreationDelegate,
            IList<double> xValues,
            double precision = DEFAULT_PRECISION)
        {
            IList<(double x, double y)> expectedOutputPoints = xValues.Select(x => (x, func(x))).ToArray();
            ExecuteTest(dimensionEnum, operatorCreationDelegate, expectedOutputPoints, precision);
        }

	    public static void ExecuteTest(
            DimensionEnum dimensionEnum,
            Func<OperatorFactory, Outlet> operatorCreationDelegate,
            IList<(double x, double y)> expectedOutputPoints,
            double precision = DEFAULT_PRECISION)
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

                        Outlet outlet = operatorCreationDelegate(o);

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
                                //string message =
                                //    $"Point [{i}] is expected to be ({expectedX}, {expectedY}), but it is ({expectedX}, {actualY}) instead.";

                                string message =
                                    $"Point [{i}] on x = {expectedX} should have y = {expectedY}, but has y = {actualY} instead.";

                                Assert.Fail(message);
                            }
                        }
                    }
                });
    }
}
