//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Calculation;
//using JJ.Business.Synthesizer.Calculation.Patches;
//using JJ.Business.Synthesizer.Configuration;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Collections;
//using JJ.Framework.Data;
//using JJ.Framework.Mathematics;
//using JJ.Framework.Testing.Data;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//// ReSharper disable UnusedVariable
//// ReSharper disable InvertIf
//// ReSharper disable CompareOfFloatsByEqualityOperator
//// ReSharper disable LocalizableElement
//// ReSharper disable SuggestVarOrType_Elsewhere

//namespace JJ.Business.Synthesizer.Tests.Helpers
//{
//    internal class TestExecutor : IDisposable
//    {
//        private const int DEFAULT_SIGNIFICANT_DIGITS = 6;

//        private readonly Func<OperatorFactory, Outlet> _operatorFactoryDelegate;
//        private IContext _context;
//        private readonly PatchFacade _patchFacade;
//        private readonly OperatorFactory _operatorFactory;

//        public const DimensionEnum DEFAULT_DIMENSION_ENUM = DimensionEnum.Number;

//        public TestExecutor(Func<OperatorFactory, Outlet> operatorFactoryDelegate, CalculationMethodEnum calculationMethodEnum)
//        {
//            _operatorFactoryDelegate = operatorFactoryDelegate ?? throw new ArgumentNullException(nameof(operatorFactoryDelegate));

//            AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(() => _context = PersistenceHelper.CreateContext());

//            RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(_context);
//            _patchFacade = new PatchFacade(repositories, calculationMethodEnum);

//            Patch patch = _patchFacade.CreatePatch();

//            _operatorFactory = new OperatorFactory(patch, repositories);
//        }

//        ~TestExecutor() => Dispose();

//        public void Dispose() => _context?.Dispose();

//        public static double CalculateOneValue(IPatchCalculator patchCalculator, double time = 0.0)
//        {
//            const int frameCount = 1;
//            var buffer = new float[1];
//            patchCalculator.Calculate(buffer, frameCount, time);
//            return buffer[0];
//        }

//        public void TestOneValue(Func<double, double> func, double x) => TestOneValue(x, func(x), DEFAULT_DIMENSION_ENUM);

//        public void TestOneValue(double expectedY) => TestOneValue(default, expectedY, DEFAULT_DIMENSION_ENUM);

//        public void TestOneValue(double x, double expectedY) => TestOneValue(x, expectedY, DEFAULT_DIMENSION_ENUM);

//        public void TestOneValue(double x, double expectedY, DimensionEnum dimensionEnum) => ExecuteTest(dimensionEnum, (x, expectedY).AsArray());

//        public void TestMultipleValues(Func<double, double> func, IList<double> xValues) => TestMultipleValues(func, DEFAULT_DIMENSION_ENUM, xValues);

//        public void TestMultipleValues(Func<double, double> func, DimensionEnum dimensionEnum, IList<double> xValues)
//        {
//            IList<(double x, double y)> expectedOutputPoints = xValues.Select(x => (x, func(x))).ToArray();
//            ExecuteTest(dimensionEnum, expectedOutputPoints);
//        }

//        public void ExecuteTest(DimensionEnum dimensionEnum, IList<(double x, double y)> expectedOutputPoints)
//        {
//            if (_operatorFactoryDelegate == null) throw new ArgumentNullException(nameof(_operatorFactoryDelegate));
//            if (expectedOutputPoints == null) throw new ArgumentNullException(nameof(expectedOutputPoints));
            
//            // Arrange
//            Outlet outlet = _operatorFactoryDelegate(_operatorFactory);

//            IPatchCalculator calculator = _patchFacade.CreateCalculator(outlet, 2, 1, 0, new CalculatorCache());

//            var buffer = new float[1];

//            // Execute
//            var actualYs = new double[expectedOutputPoints.Count];
//            double firstX = expectedOutputPoints.First().x;
//            calculator.Reset(firstX);

//            for (var i = 0; i < expectedOutputPoints.Count; i++)
//            {
//                (double expectedX, double expectedY) = expectedOutputPoints[i];

//                Array.Clear(buffer, 0, buffer.Length);
//                calculator.SetValue(dimensionEnum, expectedX);
//                calculator.Calculate(buffer, buffer.Length, expectedX);
//                double actualY = buffer[0];
//                actualYs[i] = actualY;
//            }

//            // Assert
//            for (var i = 0; i < expectedOutputPoints.Count; i++)
//            {
//                (double expectedX, double expectedY) = expectedOutputPoints[i];
//                double actualY = actualYs[i];

//                float canonicalExpectedY = ToCanonical(expectedY);
//                float canonicalActualY = ToCanonical(actualY);

//                if (canonicalExpectedY != canonicalActualY)
//                {
//                    Assert.Fail(
//                        $"Point [{i}] on x = {expectedX} should have y = {canonicalExpectedY}, but has y = {canonicalActualY} instead. " +
//                        $"(y's are rounded to {DEFAULT_SIGNIFICANT_DIGITS} significant digits.)");
//                }
//                else
//                {
//                    Console.WriteLine($"Tested point [{i}] = ({expectedX}, {canonicalActualY})");
//                }
//            }

//            Console.WriteLine($"(Note: Values are tested for {DEFAULT_SIGNIFICANT_DIGITS} significant digits and NaN is converted to 0.)");
//        }

//        /// <summary> Rounds to significant digits, and converts NaN to 0 which winmm would trip over. </summary>
//        private static float ToCanonical(double input)
//        {
//            var output = (float)input;

//            output = MathHelper.RoundToSignificantDigits(output, DEFAULT_SIGNIFICANT_DIGITS);

//            // Calculation engine will not output NaN.
//            if (float.IsNaN(output))
//            {
//                output = 0;
//            }

//            return output;
//        }
//    }
//}