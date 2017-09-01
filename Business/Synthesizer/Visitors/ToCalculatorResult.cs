using System.Collections.Generic;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class ToCalculatorResult
    {
        public ToCalculatorResult(
            OperatorCalculatorBase output_OperatorCalculator,
            IList<VariableInput_OperatorCalculator> input_OperatorCalculators,
            IList<ResettableOperatorTuple> resettableOperatorTuples)
        {
            Output_OperatorCalculator = output_OperatorCalculator ?? throw new NullException(() => output_OperatorCalculator);
            Input_OperatorCalculators = input_OperatorCalculators ?? throw new NullException(() => input_OperatorCalculators);
            ResettableOperatorTuples = resettableOperatorTuples ?? throw new NullException(() => resettableOperatorTuples);
        }

        public OperatorCalculatorBase Output_OperatorCalculator { get; }
        public IList<VariableInput_OperatorCalculator> Input_OperatorCalculators { get; }
        public IList<ResettableOperatorTuple> ResettableOperatorTuples { get; }
    }
}
