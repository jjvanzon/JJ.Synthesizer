using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    internal class ResettableOperatorTuple
    {
        public ResettableOperatorTuple(OperatorCalculatorBase operatorCalculator, string name, int? listIndex)
        {
            // Name is optional.

            Name = name;
            ListIndex = listIndex;
            OperatorCalculator = operatorCalculator ?? throw new NullException(() => operatorCalculator);
        }

        public OperatorCalculatorBase OperatorCalculator { get; }
        public string Name { get; }
        public int? ListIndex { get; }
    }
}
