using System.Collections.Generic;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class ToCalculatorResult
    {
        public ToCalculatorResult(
            OperatorCalculatorBase output_OperatorCalculator,
            DimensionStackCollection dimensionStackCollection,
            IList<VariableInput_OperatorCalculator> input_OperatorCalculators,
            IList<ResettableOperatorTuple> resettableOperatorTuples)
        {
            if (output_OperatorCalculator == null) throw new NullException(() => output_OperatorCalculator);
            if (dimensionStackCollection == null) throw new NullException(() => dimensionStackCollection);
            if (input_OperatorCalculators == null) throw new NullException(() => input_OperatorCalculators);
            if (resettableOperatorTuples == null) throw new NullException(() => resettableOperatorTuples);

            Output_OperatorCalculator = output_OperatorCalculator;
            DimensionStackCollection = dimensionStackCollection;
            Input_OperatorCalculators = input_OperatorCalculators;
            ResettableOperatorTuples = resettableOperatorTuples;
        }

        public OperatorCalculatorBase Output_OperatorCalculator { get; }
        public DimensionStackCollection DimensionStackCollection { get; internal set; }
        public IList<VariableInput_OperatorCalculator> Input_OperatorCalculators { get; }
        public IList<ResettableOperatorTuple> ResettableOperatorTuples { get; }
    }
}
