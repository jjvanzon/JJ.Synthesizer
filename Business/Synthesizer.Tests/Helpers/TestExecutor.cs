using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Data;
using JJ.Framework.Mathematics;
using JJ.Framework.Testing.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable UnusedVariable
// ReSharper disable InvertIf
// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable LocalizableElement
// ReSharper disable SuggestVarOrType_Elsewhere

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class TestExecutor
    {
        private const int DEFAULT_SIGNIFICANT_DIGITS = 6;
        public const DimensionEnum DEFAULT_DIMENSION_ENUM = DimensionEnum.Number;

        public static double CalculateOneValue(IPatchCalculator patchCalculator, double time = 0.0)
        {
            const int frameCount = 1;
            var buffer = new float[1];
            patchCalculator.Calculate(buffer, frameCount, time);
            return buffer[0];
        }

        public static void TestOneValue(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double> func,
            double x,
            CalculationMethodEnum calculationMethodEnum)
            => TestOneValue(operatorFactoryDelegate, x, func(x), DEFAULT_DIMENSION_ENUM, calculationMethodEnum);

        public static void TestOneValue(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            double expectedY,
            CalculationMethodEnum calculationMethodEnum)
            => TestOneValue(operatorFactoryDelegate, default, expectedY, DEFAULT_DIMENSION_ENUM, calculationMethodEnum);

        public static void TestOneValue(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            double x,
            double expectedY,
            CalculationMethodEnum calculationMethodEnum)
            => TestOneValue(operatorFactoryDelegate, x, expectedY, DEFAULT_DIMENSION_ENUM, calculationMethodEnum);

        public static void TestOneValue(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            double x,
            double expectedY,
            DimensionEnum dimensionEnum,
            CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(operatorFactoryDelegate, dimensionEnum, (x, expectedY).AsArray(), calculationMethodEnum);

        public static void TestMultipleValues(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double> func,
            IList<double> xValues,
            CalculationMethodEnum calculationMethodEnum)
            => TestMultipleValues(operatorFactoryDelegate, func, DEFAULT_DIMENSION_ENUM, xValues, calculationMethodEnum);

        public static void TestMultipleValues(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double> func,
            DimensionEnum dimensionEnum,
            IList<double> xValues,
            CalculationMethodEnum calculationMethodEnum)
        {
            IList<(double x, double y)> expectedOutputPoints = xValues.Select(x => (x, func(x))).ToArray();
            ExecuteTest(operatorFactoryDelegate, dimensionEnum, expectedOutputPoints, calculationMethodEnum);
        }

        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            DimensionEnum dimensionEnum,
            IList<(double x, double y)> expectedOutputPoints,
            CalculationMethodEnum calculationMethodEnum)
        {
            if (operatorFactoryDelegate == null) throw new ArgumentNullException(nameof(operatorFactoryDelegate));
            if (expectedOutputPoints == null) throw new ArgumentNullException(nameof(expectedOutputPoints));

            AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(
                () =>
                {
                    using (IContext context = PersistenceHelper.CreateContext())
                    {
                        // Arrange
                        RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);
                        var patchFacade = new PatchFacade(repositories, calculationMethodEnum);
                        Patch patch = patchFacade.CreatePatch();
                        var o = new OperatorFactory(patch, repositories);

                        Outlet outlet = operatorFactoryDelegate(o);

                        IPatchCalculator calculator = patchFacade.CreateCalculator(outlet, 2, 1, 0, new CalculatorCache());

                        var buffer = new float[1];

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

                            double canonicalExpectedY = ToCanonical(expectedY);
                            double canonicalActualY = ToCanonical(actualY);

                            if (canonicalExpectedY != canonicalActualY)
                            {
                                Assert.Fail(
                                    $"Point [{i}] on x = {expectedX} should have y = {canonicalExpectedY}, but has y = {canonicalActualY} instead. " +
                                    $"(y's are rounded to {DEFAULT_SIGNIFICANT_DIGITS} significant digits.)");
                            }
                            else
                            {
                                Console.WriteLine($"Tested point [{i}] = ({expectedX}, {canonicalActualY})");
                            }
                        }

                        Console.WriteLine(
                            $"(Note: Values are tested for {DEFAULT_SIGNIFICANT_DIGITS} significant digits and NaN is converted to 0.)");
                    }
                });
        }

        /// <summary> Rounds to significant digits, and converts NaN to 0 which winmm would trip over. </summary>
        private static double ToCanonical(double input)
        {
            double output = MathHelper.RoundToSignificantDigits(input, DEFAULT_SIGNIFICANT_DIGITS);

            // Calculation engine will not output NaN.
            if (double.IsNaN(output))
            {
                output = 0;
            }

            return output;
        }
    }
}