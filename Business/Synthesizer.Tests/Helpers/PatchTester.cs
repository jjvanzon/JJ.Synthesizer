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

#pragma warning disable IDE0039 // Use local function
// ReSharper disable ConvertToLocalFunction
// ReSharper disable UnusedVariable
// ReSharper disable InvertIf
// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable LocalizableElement
// ReSharper disable SuggestVarOrType_Elsewhere

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class PatchTester : IDisposable
    {
        private readonly IList<double?> _consts;
        private readonly bool _mustCompareZeroAndNonZeroOnly;

        private IContext _context;
        private readonly IPatchCalculator _calculator;
        private readonly SystemFacade _systemFacade;
        private readonly PatchFacade _patchFacade;

        public PatchTester(
            CalculationMethodEnum calculationMethodEnum,
            Func<OperatorFactory, Outlet> operatorFactoryDelegate,
            IList<double?> consts,
            bool mustCompareZeroAndNonZeroOnly)
        {
            if (operatorFactoryDelegate == null) throw new ArgumentNullException(nameof(operatorFactoryDelegate));

            _consts = consts ?? throw new ArgumentNullException(nameof(consts));
            _mustCompareZeroAndNonZeroOnly = mustCompareZeroAndNonZeroOnly;

            AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(() => _context = PersistenceHelper.CreateContext());

            RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(_context);

            _systemFacade = new SystemFacade(repositories.DocumentRepository);
            _patchFacade = new PatchFacade(repositories, calculationMethodEnum);

            Patch patch = _patchFacade.CreatePatch();
            var operatorFactory = new OperatorFactory(patch, repositories);
            Outlet outlet = operatorFactoryDelegate(operatorFactory);

            ReplaceVarsWithConstsIfNeeded(patch, consts);

            _calculator = _patchFacade.CreateCalculator(outlet, 2, 1, 0, new CalculatorCache());
        }

        private void ReplaceVarsWithConstsIfNeeded(Patch patch, IList<double?> constsToReplaceVariables)
        {
            IList<Operator> patchInlets = patch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
                                               .Select(x => new PatchInletOrOutlet_OperatorWrapper(x))
                                               .OrderBy(x => x.Inlet.Position)
                                               .Select(x => x.WrappedOperator)
                                               .ToArray();

            if (constsToReplaceVariables.Count > patchInlets.Count)
            {
                throw new GreaterThanException(() => constsToReplaceVariables.Count, () => patchInlets.Count);
            }

            for (var i = 0; i < constsToReplaceVariables.Count; i++)
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

        ~PatchTester() => Dispose();

        public void Dispose() => _context?.Dispose();

        /// <summary> Outputs failure messages. </summary>
        public List<string> ExecuteTest(IList<DimensionEnum> inputDimensionEnums, IList<double[]> inputPoints, IList<double> expectedOutputValues)
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

                    failureMessages.Add(TestMessageFormatter.TryGetVarConstMessage(inputDimensionEnums, _consts));
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

            output = MathHelper.RoundToSignificantDigits(output, TestConstants.DEFAULT_SIGNIFICANT_DIGITS);

            // Calculation engine will not output NaN.
            if (float.IsNaN(output))
            {
                output = 0;
            }

            if (_mustCompareZeroAndNonZeroOnly)
            {
                if (output != 0)
                {
                    output = 1;
                }
            }

            return output;
        }
    }
}