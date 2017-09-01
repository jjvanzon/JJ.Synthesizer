// MIND THE HACKS IN THIS FILE! IT MAY BE THE CAUSE OF YOUR PROBLEMS!

using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SumOverDimension_OperatorCalculator_ByDimensionToOutletsAndAdd : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _addCalculator;

        public SumOverDimension_OperatorCalculator_ByDimensionToOutletsAndAdd(
            OperatorCalculatorBase signalCalculator,
            double till,
            VariableInput_OperatorCalculator positionOutputCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);

            if (!ConversionHelper.CanCastToNonNegativeInt32(till))
            {
                throw new Exception($"{new {till}} cannot be casted to non-negative Int32.");
            }
            int tillInt = (int)till;

            // HACK in a piece of patch to dimensionToOutlets and add.

            var dimensionToOutletsCalculators = new List<OperatorCalculatorBase>(tillInt);
            for (int i = 0; i <= tillInt; i++)
            {
                var dimensionToOutletsCalculator = new DimensionToOutlets_OperatorCalculator_WithSignalOutput(signalCalculator, i, positionOutputCalculator);
                dimensionToOutletsCalculators.Add(dimensionToOutletsCalculator);
            }

            _addCalculator = new Add_OperatorCalculator(dimensionToOutletsCalculators);
        }

        public override double Calculate()
        {
            return _addCalculator.Calculate();
        }
    }
}