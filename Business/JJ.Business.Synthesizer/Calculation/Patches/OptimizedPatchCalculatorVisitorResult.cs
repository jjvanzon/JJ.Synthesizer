using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    internal class OptimizedPatchCalculatorVisitorResult
    {
        public OptimizedPatchCalculatorVisitorResult(
            DimensionStackCollection dimensionStackCollection,
            OperatorCalculatorBase output_OperatorCalculator,
            IList<VariableInput_OperatorCalculator> input_OperatorCalculators,
            IList<ResettableOperatorTuple> resettableOperatorTuples)
        {
            if (dimensionStackCollection == null) throw new NullException(() => dimensionStackCollection);
            if (output_OperatorCalculator == null) throw new NullException(() => output_OperatorCalculator);
            if (input_OperatorCalculators == null) throw new NullException(() => input_OperatorCalculators);
            if (resettableOperatorTuples == null) throw new NullException(() => resettableOperatorTuples);

            DimensionStackCollection = dimensionStackCollection;
            Output_OperatorCalculator = output_OperatorCalculator;
            Input_OperatorCalculators = input_OperatorCalculators;
            ResettableOperatorTuples = resettableOperatorTuples;
        }

        public DimensionStackCollection DimensionStackCollection { get; }
        public OperatorCalculatorBase Output_OperatorCalculator { get; }
        public IList<VariableInput_OperatorCalculator> Input_OperatorCalculators { get; }
        public IList<ResettableOperatorTuple> ResettableOperatorTuples { get; }
    }
}
