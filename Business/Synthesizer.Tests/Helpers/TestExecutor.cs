using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Data;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.Comparative;
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
        public const DimensionEnum DEFAULT_DIMENSION_ENUM = DimensionEnum.Number;

        private static readonly double?[] _specialConstsToCheck = { null, 0, 1, 2 };
        private static readonly string _note =
            $"(Note: Values are tested for {DEFAULT_SIGNIFICANT_DIGITS} significant digits and NaN is converted to 0.)";

        private IContext _context;
        private readonly IPatchCalculator _calculator;
        private readonly SystemFacade _systemFacade;
        private readonly PatchFacade _patchFacade;

        private TestExecutor(
            CalculationMethodEnum calculationMethodEnum,
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            params double?[] constsToReplaceVariables)
        {
            if (operatorFactoryDelegate == null) throw new ArgumentNullException(nameof(operatorFactoryDelegate));
            if (constsToReplaceVariables == null) throw new ArgumentNullException(nameof(constsToReplaceVariables));

            AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(() => _context = PersistenceHelper.CreateContext());

            RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(_context);

            _systemFacade = new SystemFacade(repositories.DocumentRepository);
            _patchFacade = new PatchFacade(repositories, calculationMethodEnum);

            Patch patch = _patchFacade.CreatePatch();
            var operatorFactory = new OperatorFactory(patch, repositories);
            Outlet outlet = operatorFactoryDelegate(operatorFactory);

            ReplaceVarsWithConstsIfNeeded(patch, constsToReplaceVariables);

            _calculator = _patchFacade.CreateCalculator(outlet, 2, 1, 0, new CalculatorCache());
        }

        private void ReplaceVarsWithConstsIfNeeded(Patch patch, double?[] constsToReplaceVariables)
        {
            IList<Operator> patchInlets = patch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet).ToArray();

            if (constsToReplaceVariables.Length > patchInlets.Count)
            {
                throw new GreaterThanException(() => constsToReplaceVariables.Length, () => patchInlets.Count);
            }

            for (var i = 0; i < constsToReplaceVariables.Length; i++)
            {
                double? constToReplaceVariable = constsToReplaceVariables[i];

                if (!constToReplaceVariable.HasValue)
                {
                    continue;
                }

                Operator op = patchInlets[i];

                Patch numberPatch = _systemFacade.GetSystemPatch(OperatorTypeEnum.Number);
                op.LinkToUnderlyingPatch(numberPatch);
                new Number_OperatorWrapper(op) { Number = constToReplaceVariable.Value };
                VoidResult result = _patchFacade.SaveOperator(op);
                result.Assert();
            }
        }

        ~TestExecutor() => Dispose();

        public void Dispose() => _context?.Dispose();

        // Public Static Methods

        public static double CalculateOneValue(IPatchCalculator patchCalculator, double time = 0.0)
        {
            const int frameCount = 1;
            var buffer = new float[1];
            patchCalculator.Calculate(buffer, frameCount, time);
            return buffer[0];
        }

        public static void TestWithoutInputs(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            double expectedY,
            CalculationMethodEnum calculationMethodEnum)
        {
            using (var testExecutor = new TestExecutor(calculationMethodEnum, operatorFactoryDelegate))
            {
                testExecutor.TestWithoutInputs(new[] { expectedY });
            }
        }

        public static void TestWith1Input(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double> func,
            IList<double> xValues,
            CalculationMethodEnum calculationMethodEnum)
            => TestWith1Input(operatorFactoryDelegate, func, DEFAULT_DIMENSION_ENUM, xValues, calculationMethodEnum);

        public static void TestWith1Input(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double> func,
            DimensionEnum dimensionEnum,
            IList<double> inputValues,
            CalculationMethodEnum calculationMethodEnum)
        {
            IList<double> expectedOutputValues = inputValues.Select(func).ToArray();

            using (var testExecutor = new TestExecutor(calculationMethodEnum, operatorFactoryDelegate))
            {
                testExecutor.TestWithNInputs(
                    new[] { dimensionEnum },
                    inputValues.Select(x => new[] { x }).ToArray(),
                    expectedOutputValues);
            }
        }

        public static void TestWith2Inputs(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double, double> func,
            DimensionEnum xDimensionEnum,
            IList<double> xValues,
            DimensionEnum yDimensionEnum,
            IList<double> yValues,
            CalculationMethodEnum calculationMethodEnum)
        {
            // To nested arrays
            IList<DimensionEnum> inputDimensionEnums = new[] { xDimensionEnum, yDimensionEnum };

            IList<double[]> inputPoints = xValues.CrossJoin(yValues, (x, y) => new[] {x, y}).ToArray();

            // Loop through special constants
            foreach (double? constX in _specialConstsToCheck)
            {
                foreach (double? constY in _specialConstsToCheck)
                {
                    Console.WriteLine(FormatVarConstMessage(inputDimensionEnums, new[] { constX, constY }));

                    // Replace input with constants
                    IList<double[]> inputPointsWithConsts = inputPoints
                                                            .Select(point => new[] { constX ?? point[0], constY ?? point[1] })
                                                            .Distinct(point => (point[0], point[1]))
                                                            .ToArray();

                    IList<double> expectedOutputValues = inputPointsWithConsts.Select(point => func(point[0], point[1])).ToArray();

                    // Execute test
                    using (var testExecutor = new TestExecutor(calculationMethodEnum, operatorFactoryDelegate, constX, constY))
                    {
                        testExecutor.TestWithNInputs(inputDimensionEnums, inputPointsWithConsts, expectedOutputValues);
                    }

                    Console.WriteLine();
                }
            }
        }

        public static void TestWith3Inputs(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double, double, double> func,
            DimensionEnum xDimensionEnum,
            IList<double> xValues,
            DimensionEnum yDimensionEnum,
            IList<double> yValues,
            DimensionEnum zDimensionEnum,
            IList<double> zValues,
            CalculationMethodEnum calculationMethodEnum)
        {
            // To nested arrays
            IList<DimensionEnum> inputDimensionEnums = new[] { xDimensionEnum, yDimensionEnum, zDimensionEnum };

            IList<double[]> inputPoints =
                xValues.CrossJoin(yValues, (x, y) => (x, y))
                       .CrossJoin(zValues, (xy, z) => new[] { xy.x, xy.y, z })
                       .ToArray();

            // Loop through special constants
            foreach (double? constX in _specialConstsToCheck)
            {
                foreach (double? constY in _specialConstsToCheck)
                {
                    foreach (double? constZ in _specialConstsToCheck)
                    {
                        Console.WriteLine(FormatVarConstMessage(inputDimensionEnums, new[] { constX, constY, constZ }));

                        // Replace input with constants
                        IList<double[]> inputPointsWithConsts = inputPoints
                                                                .Select(point => new[] { constX ?? point[0], constY ?? point[1], constZ ?? point[2] })
                                                                .Distinct(point => (point[0], point[1], point[2]))
                                                                .ToArray();

                        IList<double> expectedOutputValues = inputPointsWithConsts.Select(point => func(point[0], point[1], point[2])).ToArray();

                        // Execute test
                        using (var testExecutor = new TestExecutor(calculationMethodEnum, operatorFactoryDelegate))
                        {
                            testExecutor.TestWithNInputs(inputDimensionEnums, inputPointsWithConsts, expectedOutputValues);
                        }

                        Console.WriteLine();
                    }
                }
            }
        }

        // Private Instance Methods

        private void TestWithoutInputs(IList<double> expectedOutputValues)
        {
            // Pre-Conditions
            if (expectedOutputValues == null) throw new ArgumentNullException(nameof(expectedOutputValues));
            if (expectedOutputValues.Count == 0) throw new CollectionEmptyException(nameof(expectedOutputValues));

            // Execute
            IList<double> actualOutputValues = new double[expectedOutputValues.Count];

            for (var i = 0; i < actualOutputValues.Count; i++)
            {
                // Reset
                _calculator.Reset(0);

                // Calculate Value
                var buffer = new float[1];
                _calculator.Calculate(buffer, buffer.Length, 0);
                double actualOutputValue = buffer[0];

                actualOutputValues[i] = actualOutputValue;
            }

            // Assert
            for (var i = 0; i < expectedOutputValues.Count; i++)
            {
                double expectedOutputValue = expectedOutputValues[i];
                double actualOutputValue = actualOutputValues[i];

                float canonicalExpectedOutputValue = ToCanonical(expectedOutputValue);
                float canonicalActualOutputValue = ToCanonical(actualOutputValue);

                if (canonicalExpectedOutputValue != canonicalActualOutputValue)
                {
                    Assert.AreEqual(canonicalExpectedOutputValue, canonicalActualOutputValue);
                }
                else
                {
                    Console.WriteLine($"Result = {canonicalActualOutputValue}");
                }
            }

            Console.WriteLine(_note);
        }

        private void TestWithNInputs(
            IList<DimensionEnum> inputDimensionEnums,
            IList<double[]> inputPoints,
            IList<double> expectedOutputValues)
        {
            // Pre-Conditions
            if (inputDimensionEnums == null) throw new ArgumentNullException(nameof(inputDimensionEnums));
            if (inputDimensionEnums.Count == 0) throw new CollectionEmptyException(nameof(inputDimensionEnums));
            if (inputPoints == null) throw new ArgumentNullException(nameof(inputPoints));
            if (inputPoints.Count == 0) throw new CollectionEmptyException(nameof(inputPoints));
            if (expectedOutputValues == null) throw new ArgumentNullException(nameof(expectedOutputValues));
            if (expectedOutputValues.Count == 0) throw new CollectionEmptyException(nameof(expectedOutputValues));

            if (inputPoints.Count != expectedOutputValues.Count)
            {
                throw new NotEqualException(() => inputPoints.Count, () => expectedOutputValues.Count);
            }

            for (var i = 0; i < inputPoints.Count; i++)
            {
                if (inputPoints[i].Length != inputDimensionEnums.Count)
                {
                    throw new NotEqualException(() => inputPoints[i].Length, () => inputDimensionEnums.Count);
                }
            }

            // Reset Time
            int? timeDimensionIndex = inputDimensionEnums.TryGetIndexOf(x => x == DimensionEnum.Time);

            if (timeDimensionIndex.HasValue)
            {
                double firstTimeValue = inputPoints[0][timeDimensionIndex.Value];
                _calculator.Reset(firstTimeValue);
            }

            IList<double> actualOutputValues = new double[inputPoints.Count];

            for (var pointIndex = 0; pointIndex < actualOutputValues.Count; pointIndex++)
            {
                IList<double> inputValues = inputPoints[pointIndex];

                // Set Input Values
                for (var dimensionIndex = 0; dimensionIndex < inputDimensionEnums.Count; dimensionIndex++)
                {
                    DimensionEnum inputDimensionEnum = inputDimensionEnums[dimensionIndex];
                    double inputValue = inputValues[dimensionIndex];
                    _calculator.SetValue(inputDimensionEnum, inputValue);
                }

                // Determine Time
                double time = 0;

                if (timeDimensionIndex.HasValue)
                {
                    time = inputValues[timeDimensionIndex.Value];
                }

                // Calculate Value
                var buffer = new float[1];
                _calculator.Calculate(buffer, buffer.Length, time);
                double actualOutputValue = buffer[0];

                actualOutputValues[pointIndex] = actualOutputValue;
            }

            // Assert
            for (var i = 0; i < inputPoints.Count; i++)
            {
                IList<double> inputValues = inputPoints[i];
                double expectedOutputValue = expectedOutputValues[i];
                double actualOutputValue = actualOutputValues[i];

                float canonicalExpectedOutputValue = ToCanonical(expectedOutputValue);
                float canonicalActualOutputValue = ToCanonical(actualOutputValue);

                string pointDescriptor = GetPointDescriptor(inputDimensionEnums, inputValues, i);

                if (canonicalExpectedOutputValue != canonicalActualOutputValue)
                {
                    Assert.Fail(
                        $"{pointDescriptor} " +
                        $"should have result = {canonicalExpectedOutputValue}, " +
                        $"but has result = {canonicalActualOutputValue} instead. {_note}");
                }
                else
                {
                    Console.WriteLine($"{pointDescriptor} => {canonicalActualOutputValue}");
                }
            }

            Console.WriteLine(_note);
        }

        private string GetPointDescriptor(IList<DimensionEnum> inputDimensionEnums, IList<double> inputValues, int i)
        {
            if (inputValues.Count != inputDimensionEnums.Count)
            {
                throw new NotEqualException(() => inputValues.Count, () => inputDimensionEnums.Count);
            }

            string concatenatedInputValues = string.Join(", ", inputDimensionEnums.Zip(inputValues).Select(x => $"{x.Item1}={x.Item2}"));
            string pointDescriptor = $"Tested point [{i}] = ({concatenatedInputValues})";
            return pointDescriptor;
        }

        private static string FormatVarConstMessage(IList<DimensionEnum> inputDimensionEnums, double?[] consts)
        {
            var sb = new StringBuilder();

            sb.Append("Testing for (");

            string concatenatedVarConstDescriptors =
                string.Join(", ", inputDimensionEnums.Zip(consts).Select(x => FormatVarConstDescriptor(x.Item1, x.Item2)));

            sb.Append(concatenatedVarConstDescriptors);

            sb.Append(").");

            return sb.ToString();
        }

        private static string FormatVarConstDescriptor(DimensionEnum inputDimensionEnum, double? @const) 
            => @const.HasValue ? $"const {@const}" : $"var {inputDimensionEnum}";

        /// <summary> Converts to float, rounds to significant digits and converts NaN to 0 which 'winmm' would trip over. </summary>
        private float ToCanonical(double input)
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