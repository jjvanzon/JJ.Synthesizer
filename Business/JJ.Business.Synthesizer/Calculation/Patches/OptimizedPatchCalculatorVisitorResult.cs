using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Calculation.Patches;

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
