using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;
using JJ.Framework.Mathematics;
using JJ.Framework.Testing.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable UnusedVariable
// ReSharper disable InvertIf
// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable LocalizableElement

namespace JJ.Business.Synthesizer.Tests.Helpers
{
	internal static class TestHelper
	{
        private const int DEFAULT_SIGNIFICANT_DIGITS = 6;

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
            int significantDigits = DEFAULT_SIGNIFICANT_DIGITS)
        {
            IList<(double x, double y)> expectedOutputPoints = xValues.Select(x => (x, func(x))).ToArray();
            ExecuteTest(dimensionEnum, operatorCreationDelegate, expectedOutputPoints, significantDigits);
        }

	    public static void ExecuteTest(
            DimensionEnum dimensionEnum,
            Func<OperatorFactory, Outlet> operatorCreationDelegate,
            IList<(double x, double y)> expectedOutputPoints,
            int significantDigits = DEFAULT_SIGNIFICANT_DIGITS)
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

                            double roundedExpectedY = MathHelper.RoundToSignificantDigits(expectedY, significantDigits);
                            double roundedActualY = MathHelper.RoundToSignificantDigits(actualY, significantDigits);

                            if (roundedExpectedY != roundedActualY)
                            {
                                Assert.Fail(
                                    $"Point [{i}] on x = {expectedX} should have y = {roundedExpectedY}, but has y = {roundedActualY} instead. " +
                                    $"(y's are rounded to {significantDigits} significant digits.)");
                            }
                            else
                            {
                                Console.WriteLine($"Tested point [{i}] = ({expectedX}, {roundedActualY})");
                            }
                        }
                    }
                });
    }
}
