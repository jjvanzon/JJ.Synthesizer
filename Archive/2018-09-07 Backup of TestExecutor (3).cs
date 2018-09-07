//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using JJ.Business.Synthesizer.Calculation;
//using JJ.Business.Synthesizer.Calculation.Patches;
//using JJ.Business.Synthesizer.Configuration;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Collections;
//using JJ.Framework.Data;
//using JJ.Framework.Exceptions.Basic;
//using JJ.Framework.Exceptions.Comparative;
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

//        private static readonly string _note =
//            $"(Note: Values are tested for {DEFAULT_SIGNIFICANT_DIGITS} significant digits and NaN is converted to 0.)";

//        private IContext _context;
//        private readonly IPatchCalculator _calculator;

//        public const DimensionEnum DEFAULT_DIMENSION_ENUM = DimensionEnum.Number;

//        private TestExecutor(CalculationMethodEnum calculationMethodEnum, Func<OperatorFactory, Outlet> operatorFactoryDelegate)
//        {
//            if (operatorFactoryDelegate == null) throw new ArgumentNullException(nameof(operatorFactoryDelegate));

//            AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(() => _context = PersistenceHelper.CreateContext());

//            RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(_context);
//            var patchFacade = new PatchFacade(repositories, calculationMethodEnum);
//            Patch patch = patchFacade.CreatePatch();
//            var operatorFactory = new OperatorFactory(patch, repositories);
//            Outlet outlet = operatorFactoryDelegate(operatorFactory);

//            _calculator = patchFacade.CreateCalculator(outlet, 2, 1, 0, new CalculatorCache());
//        }

//        ~TestExecutor() => Dispose();

//        public void Dispose() => _context?.Dispose();

//        // Public Static Methods

//        public static double CalculateOneValue(IPatchCalculator patchCalculator, double time = 0.0)
//        {
//            const int frameCount = 1;
//            var buffer = new float[1];
//            patchCalculator.Calculate(buffer, frameCount, time);
//            return buffer[0];
//        }

//        public static void TestWithoutInputs(
//            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
//            double expectedY,
//            CalculationMethodEnum calculationMethodEnum)
//        {
//            using (var testExecutor = new TestExecutor(calculationMethodEnum, operatorFactoryDelegate))
//            {
//                testExecutor.TestWith1Input(DEFAULT_DIMENSION_ENUM, ((double)default, expectedY).AsArray());
//            }
//        }

//        public static void TestWith1Input(
//            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
//            Func<double, double> func,
//            IList<double> xValues,
//            CalculationMethodEnum calculationMethodEnum)
//            => TestWith1Input(operatorFactoryDelegate, func, DEFAULT_DIMENSION_ENUM, xValues, calculationMethodEnum);

//        public static void TestWith1Input(
//            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
//            Func<double, double> func,
//            DimensionEnum dimensionEnum,
//            IList<double> xValues,
//            CalculationMethodEnum calculationMethodEnum)
//        {
//            IList<(double x, double y)> expectedOutputPoints = xValues.Select(x => (x, func(x))).ToArray();

//            using (var testExecutor = new TestExecutor(calculationMethodEnum, operatorFactoryDelegate))
//            {
//                testExecutor.TestWith1Input(dimensionEnum, expectedOutputPoints);
//            }
//        }

//        public static void TestWith2Inputs(
//            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
//            Func<double, double, double> func,
//            DimensionEnum xDimensionEnum,
//            IList<double> xValues,
//            DimensionEnum yDimensionEnum,
//            IList<double> yValues,
//            CalculationMethodEnum calculationMethodEnum)
//        {
//            IList<(double x, double y, double z)> expectedOutputPoints = xValues.CrossJoin(yValues, (x, y) => (x, y, func(x, y))).ToArray();

//            using (var testExecutor = new TestExecutor(calculationMethodEnum, operatorFactoryDelegate))
//            {
//                testExecutor.TestWith2Inputs(xDimensionEnum, yDimensionEnum, expectedOutputPoints);
//            }
//        }

//        public static void TestWith3Inputs(
//            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
//            Func<double, double, double, double> func,
//            CalculationMethodEnum calculationMethodEnum,
//            params (DimensionEnum, IList<double>)[] inputValues)
//        {
//            IList<(double x, double y, double z, double w)> expectedOutputPoints =
//                xValues.CrossJoin(yValues, (x, y) => (x, y))
//                       .CrossJoin(yValues, (xy, z) => (xy.x, xy.y, z))
//                       .CrossJoin(zValues, (xyz, w) => (xyz.x, xyz.y, xyz.z, func(xyz.x, xyz.y, xyz.z)))
//                       .ToArray();

//            using (var testExecutor = new TestExecutor(calculationMethodEnum, operatorFactoryDelegate))
//            {
//                testExecutor.TestWith3Inputs(xDimensionEnum, yDimensionEnum, zDimensionEnum, expectedOutputPoints);
//            }
//        }

//        // Private Instance Methods

//        private void TestWith1Input(DimensionEnum dimensionEnum, IList<(double x, double y)> expectedOutputPoints)
//        {
//            if (expectedOutputPoints == null) throw new ArgumentNullException(nameof(expectedOutputPoints));

//            // Arrange
//            var buffer = new float[1];

//            // Execute
//            var actualYs = new double[expectedOutputPoints.Count];

//            // HACK: This is assuming X is the time dimension.
//            double firstX = expectedOutputPoints.First().x;
//            _calculator.Reset(firstX);

//            for (var i = 0; i < expectedOutputPoints.Count; i++)
//            {
//                (double expectedX, double expectedY) = expectedOutputPoints[i];

//                Array.Clear(buffer, 0, buffer.Length);
//                _calculator.SetValue(dimensionEnum, expectedX);
//                _calculator.Calculate(buffer, buffer.Length, expectedX);
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
//                    Assert.Fail($"Point [{i}] on x = {expectedX} " +
//                                $"should have y = {canonicalExpectedY}, " +
//                                $"but has y = {canonicalActualY} instead. {_note}");
//                }
//                else
//                {
//                    Console.WriteLine($"Tested point [{i}] = ({expectedX}, {canonicalActualY})");
//                }
//            }

//            Console.WriteLine(_note);
//        }

//        private void TestWith2Inputs(
//            DimensionEnum xDimensionEnum,
//            DimensionEnum yDimensionEnum,
//            IList<(double x, double y, double z)> expectedOutputPoints)
//        {
//            if (expectedOutputPoints == null) throw new ArgumentNullException(nameof(expectedOutputPoints));

//            // Arrange
//            var buffer = new float[1];

//            // Execute
//            var actualZs = new double[expectedOutputPoints.Count];
            
//            // HACK: This is assuming X is the time dimension.
//            double firstX = expectedOutputPoints.First().x;
//            _calculator.Reset(firstX);

//            for (var i = 0; i < expectedOutputPoints.Count; i++)
//            {
//                (double expectedX, double expectedY, double expectedZ) = expectedOutputPoints[i];

//                Array.Clear(buffer, 0, buffer.Length);
//                _calculator.SetValue(xDimensionEnum, expectedX);
//                _calculator.SetValue(yDimensionEnum, expectedY);
//                _calculator.Calculate(buffer, buffer.Length, expectedX);
//                double actualZ = buffer[0];
//                actualZs[i] = actualZ;
//            }

//            // Assert
//            for (var i = 0; i < expectedOutputPoints.Count; i++)
//            {
//                (double expectedX, double expectedY, double expectedZ) = expectedOutputPoints[i];
//                double actualZ = actualZs[i];

//                float canonicalExpectedZ = ToCanonical(expectedZ);
//                float canonicalActualZ = ToCanonical(actualZ);

//                if (canonicalExpectedZ != canonicalActualZ)
//                {
//                    Assert.Fail(
//                        $"Point [{i}] with {xDimensionEnum}={expectedX}, {yDimensionEnum}={expectedY}) " +
//                        $"should have result = {canonicalExpectedZ}, " +
//                        $"but has result = {canonicalActualZ} instead. {_note}");
//                }
//                else
//                {
//                    Console.WriteLine($"Tested point [{i}] = (" +
//                                      $"{xDimensionEnum}={expectedX}, " +
//                                      $"{yDimensionEnum}={expectedY}) " +
//                                      $"=> {canonicalActualZ}");
//                }
//            }

//            Console.WriteLine(_note);
//        }

//        private void TestWith3Inputs(
//            DimensionEnum xDimensionEnum,
//            DimensionEnum yDimensionEnum,
//            DimensionEnum zDimensionEnum,
//            IList<(double x, double y, double z, double w)> expectedOutputPoints)
//        {
//            TestWithNInputs(
//                new[] { xDimensionEnum, yDimensionEnum, zDimensionEnum },
//                expectedOutputPoints.Select(x => new[] { x.x, x.y, x.z, x.w }).Cast<IList<double>>().ToArray());

//            return;

//            if (expectedOutputPoints == null) throw new ArgumentNullException(nameof(expectedOutputPoints));

//            // Arrange
//            var buffer = new float[1];

//            // Execute
//            var actualWs = new double[expectedOutputPoints.Count];
            
//            // HACK: This is assuming X is the time dimension.
//            double firstX = expectedOutputPoints.First().x;
//            _calculator.Reset(firstX);

//            for (var i = 0; i < expectedOutputPoints.Count; i++)
//            {
//                (double expectedX, double expectedY, double expectedZ, double expectedW) = expectedOutputPoints[i];

//                Array.Clear(buffer, 0, buffer.Length);
//                _calculator.SetValue(xDimensionEnum, expectedX);
//                _calculator.SetValue(yDimensionEnum, expectedY);
//                _calculator.SetValue(zDimensionEnum, expectedZ);
//                _calculator.Calculate(buffer, buffer.Length, expectedX);
//                double actualW = buffer[0];
//                actualWs[i] = actualW;
//            }

//            // Assert
//            for (var i = 0; i < expectedOutputPoints.Count; i++)
//            {
//                (double expectedX, double expectedY, double expectedZ, double expectedW) = expectedOutputPoints[i];
//                double actualW = actualWs[i];

//                float canonicalExpectedW = ToCanonical(expectedW);
//                float canonicalActualW = ToCanonical(actualW);

//                if (canonicalExpectedW != canonicalActualW)
//                {
//                    Assert.Fail(
//                        $"Point [{i}] with {xDimensionEnum}={expectedX}, {yDimensionEnum}={expectedY}, {zDimensionEnum}={expectedZ}) " +
//                        $"should have result = {canonicalExpectedW}, " +
//                        $"but has result = {canonicalActualW} instead. {_note}");
//                }
//                else
//                {
//                    Console.WriteLine($"Tested point [{i}] = (" +
//                                      $"{xDimensionEnum}={expectedX}, " +
//                                      $"{yDimensionEnum}={expectedY}, " +
//                                      $"{zDimensionEnum}={expectedZ}) " +
//                                      $"=> {canonicalActualW}");
//                }
//            }

//            Console.WriteLine(_note);
//        }

//        // TODO: Use tuples instead of list counts to check for equality.
//        private void TestWithNInputs(
//            IList<(DimensionEnum, IList<double>> inputDimensionValues,
//            IList<double> expectedOutputValues)
//        {
//            // Pre-Conditions
//            if (inputDimensionValues == null) throw new ArgumentNullException(nameof(inputDimensionValues));
//            if (inputDimensionValues.Count == 0) throw new CollectionEmptyException(nameof(inputDimensionValues));
//            if (expectedOutputValues == null) throw new ArgumentNullException(nameof(inputDimensionValues));
//            if (expectedOutputValues.Count == 0) throw new CollectionEmptyException(nameof(expectedOutputValues));

//            if (inputDimensionValues.Count != expectedOutputValues.Count)
//            {
//                throw new NotEqualException(() => inputDimensionValues.Count, () => expectedOutputValues.Count);
//            }

//            for (var i = 0; i < inputPoints.Count; i++)
//            {
//                if (inputPoints[i].Count != inputDimensionEnums.Count)
//                {
//                    throw new NotEqualException(() => inputPoints[i].Count, () => inputDimensionEnums.Count);
//                }
//            }

//            // Arrange
//            var buffer = new float[1];

//            // Execute
//            int? timeDimensionIndex = inputDimensionEnums.TryGetIndexOf(x => x == DimensionEnum.Time);
//            if (timeDimensionIndex.HasValue)
//            {
//                double firstTimeValue = inputPoints[0][timeDimensionIndex.Value];
//                _calculator.Reset(firstTimeValue);
//            }

//            var actualOutputValues = new double[inputPoints.Count];

//            for (var pointIndex = 0; pointIndex < inputPoints.Count; pointIndex++)
//            {
//                IList<double> inputPointValues = inputPoints[pointIndex];

//                Array.Clear(buffer, 0, buffer.Length);

//                // Set Values
//                for (var dimensionIndex = 0; dimensionIndex < inputDimensionEnums.Count; dimensionIndex++)
//                {
//                    DimensionEnum inputDimensionEnum = inputDimensionEnums[dimensionIndex];
//                    double inputValue = inputPointValues[dimensionIndex];
//                    _calculator.SetValue(inputDimensionEnum, inputValue);
//                }

//                // Determine Time
//                double time = 0;
//                if (timeDimensionIndex.HasValue)
//                {
//                    time = inputPointValues[timeDimensionIndex.Value];
//                }

//                // Calculate Value
//                _calculator.Calculate(buffer, buffer.Length, time);
//                double actualOutputValue = buffer[0];

//                actualOutputValues[pointIndex] = actualOutputValue;
//            }

//            // Assert
//            for (var i = 0; i < inputPoints.Count; i++)
//            {
//                IList<double> inputPointValues = inputPoints[i];

//                string pointDescriptor = GetPointDescriptor(inputDimensionEnums, inputPointValues, i);

//                double expectedOutputValue = inputPointValues.Last();
//                double actualOutputValue = actualOutputValues[i];

//                float canonicalExpectedOutputValue = ToCanonical(expectedOutputValue);
//                float canonicalActualOutputValue = ToCanonical(actualOutputValue);

//                if (canonicalExpectedOutputValue != canonicalActualOutputValue)
//                {
//                    Assert.Fail(
//                        $"{pointDescriptor} " +
//                        $"should have result = {canonicalExpectedOutputValue}, " +
//                        $"but has result = {canonicalActualOutputValue} instead. {_note}");
//                }
//                else
//                {
//                    Console.WriteLine($"{pointDescriptor} => {canonicalActualOutputValue}");
//                }
//            }

//            Console.WriteLine(_note);
//        }

//        private string GetPointDescriptor(IList<DimensionEnum> inputDimensionEnums, IList<double> inputPointValues, int i)
//        {
//            if (inputPointValues.Count != inputDimensionEnums.Count)
//            {
//                throw new NotEqualException(() => inputPointValues.Count, () => inputDimensionEnums.Count);
//            }

//            var sb = new StringBuilder();

//            sb.Append($"Tested point [{i}] = (");

//            for (var j = 0; j < inputDimensionEnums.Count; j++)
//            {
//                DimensionEnum inputDimensionEnum = inputDimensionEnums[j];
//                double inputValue = inputPointValues[j];

//                sb.Append($"{inputDimensionEnum}={inputValue}");

//                if (j != inputDimensionEnums.Count - 1)
//                {
//                    sb.Append(", ");
//                }
//            }

//            sb.Append(")");

//            return sb.ToString();
//        }

//        /// <summary> Converts to float, rounds to significant digits and converts NaN to 0 which winmm would trip over. </summary>
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