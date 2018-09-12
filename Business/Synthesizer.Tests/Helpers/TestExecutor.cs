using System;
using System.Collections.Generic;
using System.Linq;
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

#pragma warning disable IDE0039 // Use local function
// ReSharper disable ConvertToLocalFunction
// ReSharper disable UnusedVariable
// ReSharper disable InvertIf
// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable LocalizableElement
// ReSharper disable SuggestVarOrType_Elsewhere

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class TestExecutor : IDisposable
    {
        public const int DEFAULT_SIGNIFICANT_DIGITS = 6;
        public const DimensionEnum DEFAULT_DIMENSION_ENUM = DimensionEnum.Number;

        private static readonly double?[] _specialConstsToTest = { null, 0, 1, 2 };

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
            IList<Operator> patchInlets = patch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
                                               .Select(x => new PatchInletOrOutlet_OperatorWrapper(x))
                                               .OrderBy(x => x.Inlet.Position)
                                               .Select(x => x.WrappedOperator)
                                               .ToArray();

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

        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            double expectedY,
            CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(operatorFactoryDelegate, arr => expectedY, Array.Empty<DimensionInfo>(), calculationMethodEnum);

        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double> func,
            IList<double> xValues,
            CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(operatorFactoryDelegate, func, DEFAULT_DIMENSION_ENUM, xValues, calculationMethodEnum);

        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double> func,
            DimensionEnum dimensionEnum,
            IList<double> inputValues,
            CalculationMethodEnum calculationMethodEnum)
        {
            var dimensionInfoList = new[] { new DimensionInfo(dimensionEnum, inputValues) };
            ExecuteTest(operatorFactoryDelegate, arr => func(arr[0]), dimensionInfoList, calculationMethodEnum);
        }

        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double, double, double> func,
            DimensionEnum xDimensionEnum,
            IList<double> xValues,
            DimensionEnum yDimensionEnum,
            IList<double> yValues,
            CalculationMethodEnum calculationMethodEnum)
        {
            var dimensionInfoList = new[] { new DimensionInfo(xDimensionEnum, xValues), new DimensionInfo(yDimensionEnum, yValues) };
            ExecuteTest(operatorFactoryDelegate, arr => func(arr[0], arr[1]), dimensionInfoList, calculationMethodEnum);
        }

        public static void ExecuteTest(
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
            var dimensionInfoList = new[]
            {
                new DimensionInfo(xDimensionEnum, xValues), new DimensionInfo(yDimensionEnum, yValues), new DimensionInfo(zDimensionEnum, zValues)
            };

            ExecuteTest(operatorFactoryDelegate, arr => func(arr[0], arr[1], arr[2]), dimensionInfoList, calculationMethodEnum);
        }

        public static void ExecuteTest(
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            Func<double[], double> func,
            IList<DimensionInfo> dimensionInfoList,
            CalculationMethodEnum calculationMethodEnum)
        {
            var failureMessages = new List<string>();

            IList<DimensionEnum> inputDimensionEnums = dimensionInfoList.Select(x => x.DimensionEnum).ToArray();
            IList<double[]> inputPoints = dimensionInfoList.Select(x => x.InputValues).CrossJoin(x => x.ToArray()).ToArray();

            IList<double?[]> constsLists = CollectionHelper.Repeat(dimensionInfoList.Count, () => _specialConstsToTest)
                                                           .CrossJoin(x => x.ToArray())
                                                           .DefaultIfEmpty(Array.Empty<double?>())
                                                           .ToArray();
            // Loop through special constants
            foreach (double?[] consts in constsLists)
            {
                string varConstMessage = TestMessageFormatter.TryGetVarConstMessage(inputDimensionEnums, consts);
                if (!string.IsNullOrEmpty(varConstMessage))
                {
                    Console.WriteLine(varConstMessage);
                }

                // Replace input with constants
                IList<double[]> inputPointsWithConsts = inputPoints
                                                        .Select(point => consts.Zip(point, (x, y) => x ?? y).ToArray())
                                                        .DistinctMany()
                                                        .ToArray();

                IList<double> expectedOutputValues = inputPointsWithConsts.Select(func).ToArray();
                if (expectedOutputValues.Count == 0)
                {
                    expectedOutputValues = new List<double> { func(null) };
                }

                // Execute test
                using (var testExecutor = new TestExecutor(calculationMethodEnum, operatorFactoryDelegate, consts))
                {
                    List<string> failureMessages2 = testExecutor.ExecuteTest(inputDimensionEnums, inputPointsWithConsts, expectedOutputValues);

                    failureMessages.AddRange(failureMessages2);
                }

                Console.WriteLine();
            }

            Console.WriteLine(TestMessageFormatter.Note);

            if (failureMessages.Any())
            {
                Assert.Fail(string.Join(Environment.NewLine, failureMessages) + " " + TestMessageFormatter.Note);
            }
        }

        // Private Instance Methods

        private List<string> ExecuteTest(
            IList<DimensionEnum> inputDimensionEnums,
            IList<double[]> inputPoints,
            IList<double> expectedOutputValues)
        {
            // Pre-Conditions
            if (inputDimensionEnums == null) throw new ArgumentNullException(nameof(inputDimensionEnums));
            if (inputPoints == null) throw new ArgumentNullException(nameof(inputPoints));
            if (expectedOutputValues == null) throw new ArgumentNullException(nameof(expectedOutputValues));
            if (expectedOutputValues.Count == 0) throw new CollectionEmptyException(nameof(expectedOutputValues));

            bool hasInput = inputDimensionEnums.Count != 0 || inputPoints.Count != 0;

            if (hasInput)
            {
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
            }

            // Reset Time
            int? timeDimensionIndex = inputDimensionEnums.TryGetIndexOf(x => x == DimensionEnum.Time);

            if (timeDimensionIndex.HasValue)
            {
                double firstTimeValue = inputPoints[0][timeDimensionIndex.Value];
                _calculator.Reset(firstTimeValue);
            }

            IList<double> actualOutputValues = new double[expectedOutputValues.Count];

            for (var pointIndex = 0; pointIndex < expectedOutputValues.Count; pointIndex++)
            {
                double time = 0;

                if (hasInput)
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
                    if (timeDimensionIndex.HasValue)
                    {
                        time = inputValues[timeDimensionIndex.Value];
                    }
                }

                // Calculate Value
                var buffer = new float[1];
                _calculator.Calculate(buffer, buffer.Length, time);
                double actualOutputValue = buffer[0];

                actualOutputValues[pointIndex] = actualOutputValue;
            }

            // Check
            var failureMessages = new List<string>();

            for (var i = 0; i < expectedOutputValues.Count; i++)
            {
                IList<double> inputValues = hasInput ? inputPoints[i] : Array.Empty<double>();
                double expectedOutputValue = expectedOutputValues[i];
                double actualOutputValue = actualOutputValues[i];

                float canonicalExpectedOutputValue = ToCanonical(expectedOutputValue);
                float canonicalActualOutputValue = ToCanonical(actualOutputValue);

                if (canonicalExpectedOutputValue != canonicalActualOutputValue)
                {
                    string failureMessage = TestMessageFormatter.GetOutputValueMessage_NotValid(
                        i,
                        inputDimensionEnums,
                        inputValues,
                        canonicalExpectedOutputValue,
                        canonicalActualOutputValue);

                    Console.WriteLine(failureMessage);
                    failureMessages.Add(failureMessage);
                }
                else
                {
                    Console.WriteLine(TestMessageFormatter.GetOutputValueMessage(i, inputDimensionEnums, inputValues, canonicalActualOutputValue));
                }
            }

            return failureMessages;
        }

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