using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    internal class ResettableOperatorTuple
    {
        public ResettableOperatorTuple(OperatorCalculatorBase operatorCalculator, string name, int? position)
        {
            // Name is optional.

            Name = name;
            Position = position;
            OperatorCalculator = operatorCalculator ?? throw new NullException(() => operatorCalculator);
        }

        public OperatorCalculatorBase OperatorCalculator { get; }
        public string Name { get; }
        public int? Position { get; }
    }
}
