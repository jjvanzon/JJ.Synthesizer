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
    internal class TestExecutor : IDisposable
    {
        private const int DEFAULT_SIGNIFICANT_DIGITS = 6;

        private static readonly string _note =
            $"(Note: Values are tested for {DEFAULT_SIGNIFICANT_DIGITS} significant digits and NaN is converted to 0.)";

        private IContext _context;
        private readonly IPatchCalculator _calculator;

        public const DimensionEnum DEFAULT_DIMENSION_ENUM = DimensionEnum.Number;

        private TestExecutor(CalculationMethodEnum calculationMethodEnum, Func<OperatorFactory, Outlet> operatorFactoryDelegate)
        {
            if (operatorFactoryDelegate == null) throw new ArgumentNullException(nameof(operatorFactoryDelegate));

            AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(() => _context = PersistenceHelper.CreateContext());

            RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(_context);
            var patchFacade = new PatchFacade(repositories, calculationMethodEnum);
            Patch patch = patchFacade.CreatePatch();
            var operatorFactory = new OperatorFactory(patch, repositories);
            Outlet outlet = operatorFactoryDelegate(operatorFactory);

            _calculator = patchFacade.CreateCalculator(outlet, 2, 1, 0, new CalculatorCache());
        }

        ~TestExecutor() => Dispose();

        public void Dispose() => _context?.Dispose();

        public static double CalculateOneValue(IPatchCalculator patchCalculator, double time = 0.0)
        {
            const int frameCount = 1;
            var buffer = new float[1];
            patchCalculator.Calculate(buffer, frameCount, time);
            return buffer[0];
        }

        public static void Test0In1Out(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            double expectedY,
            CalculationMethodEnum calculationMethodEnum)
        {
            using (var testExecutor = new TestExecutor(calculationMethodEnum, operatorFactoryDelegate))
            {
                testExecutor.ExecuteTest(DEFAULT_DIMENSION_ENUM, ((double)default, expectedY).AsArray());
            }
        }

        public static void Test1In1Out(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double> func,
            IList<double> xValues,
            CalculationMethodEnum calculationMethodEnum)
            => Test1In1Out(operatorFactoryDelegate, func, DEFAULT_DIMENSION_ENUM, xValues, calculationMethodEnum);

        public static void Test1In1Out(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double> func,
            DimensionEnum dimensionEnum,
            IList<double> xValues,
            CalculationMethodEnum calculationMethodEnum)
        {
            IList<(double x, double y)> expectedOutputPoints = xValues.Select(x => (x, func(x))).ToArray();

            using (var testExecutor = new TestExecutor(calculationMethodEnum, operatorFactoryDelegate))
            {
                testExecutor.ExecuteTest(dimensionEnum, expectedOutputPoints);
            }
        }

        public static void Test2In1Out(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double, double> func,
            DimensionEnum xDimensionEnum,
            IList<double> xValues,
            DimensionEnum yDimensionEnum,
            IList<double> yValues,
            CalculationMethodEnum calculationMethodEnum)
        {
            IList<(double x, double y, double z)> expectedOutputPoints = xValues.CrossJoin(yValues, (x, y) => (x, y, func(x, y))).ToArray();

            using (var testExecutor = new TestExecutor(calculationMethodEnum, operatorFactoryDelegate))
            {
                testExecutor.ExecuteTest(xDimensionEnum, yDimensionEnum, expectedOutputPoints);
            }
        }

        private void ExecuteTest(DimensionEnum dimensionEnum, IList<(double x, double y)> expectedOutputPoints)
        {
            if (expectedOutputPoints == null) throw new ArgumentNullException(nameof(expectedOutputPoints));

            // Arrange
            var buffer = new float[1];

            // Execute
            var actualYs = new double[expectedOutputPoints.Count];
            double firstX = expectedOutputPoints.First().x;
            _calculator.Reset(firstX);

            for (var i = 0; i < expectedOutputPoints.Count; i++)
            {
                (double expectedX, double expectedY) = expectedOutputPoints[i];

                Array.Clear(buffer, 0, buffer.Length);
                _calculator.SetValue(dimensionEnum, expectedX);
                _calculator.Calculate(buffer, buffer.Length, expectedX);
                double actualY = buffer[0];
                actualYs[i] = actualY;
            }

            // Assert
            for (var i = 0; i < expectedOutputPoints.Count; i++)
            {
                (double expectedX, double expectedY) = expectedOutputPoints[i];
                double actualY = actualYs[i];

                float canonicalExpectedY = ToCanonical(expectedY);
                float canonicalActualY = ToCanonical(actualY);

                if (canonicalExpectedY != canonicalActualY)
                {
                    Assert.Fail(
                        $"Point [{i}] on x = {expectedX} should have y = {canonicalExpectedY}, but has y = {canonicalActualY} instead. {_note}");
                }
                else
                {
                    Console.WriteLine($"Tested point [{i}] = ({expectedX}, {canonicalActualY})");
                }
            }

            Console.WriteLine(_note);
        }

        private void ExecuteTest(
            DimensionEnum xDimensionEnum,
            DimensionEnum yDimensionEnum,
            IList<(double x, double y, double z)> expectedOutputPoints)
        {
            if (expectedOutputPoints == null) throw new ArgumentNullException(nameof(expectedOutputPoints));

            // Arrange
            var buffer = new float[1];

            // Execute
            var actualZs = new double[expectedOutputPoints.Count];
            double firstX = expectedOutputPoints.First().x;
            _calculator.Reset(firstX);

            for (var i = 0; i < expectedOutputPoints.Count; i++)
            {
                (double expectedX, double expectedY, double expectedZ) = expectedOutputPoints[i];

                Array.Clear(buffer, 0, buffer.Length);
                _calculator.SetValue(xDimensionEnum, expectedX);
                _calculator.SetValue(yDimensionEnum, expectedY);
                _calculator.Calculate(buffer, buffer.Length, expectedX);
                double actualZ = buffer[0];
                actualZs[i] = actualZ;
            }

            // Assert
            for (var i = 0; i < expectedOutputPoints.Count; i++)
            {
                (double expectedX, double expectedY, double expectedZ) = expectedOutputPoints[i];
                double actualZ = actualZs[i];

                float canonicalExpectedZ = ToCanonical(expectedZ);
                float canonicalActualZ = ToCanonical(actualZ);

                if (canonicalExpectedZ != canonicalActualZ)
                {
                    Assert.Fail(
                        $"Point [{i}] with {xDimensionEnum}={expectedX}, {yDimensionEnum}={expectedY}) " +
                        $"should have result = {canonicalExpectedZ}, " +
                        $"but has result = {canonicalActualZ} instead. {_note}");
                }
                else
                {
                    Console.WriteLine($"Tested point [{i}] = ({xDimensionEnum}={expectedX}, {yDimensionEnum}={expectedY}) => {canonicalActualZ}");
                }
            }

            Console.WriteLine(_note);
        }

        /// <summary> Converts to float, rounds to significant digits and converts NaN to 0 which winmm would trip over. </summary>
        private static float ToCanonical(double input)
        {
            var output = (float)input;

            output = MathHelper.RoundToSignificantDigits(output, DEFAULT_SIGNIFICANT_DIGITS);

            // Calculation engine will not output NaN.
            if (float.IsNaN(output))
            {
                output = 0;
            }

            return output;
        }
    }
}