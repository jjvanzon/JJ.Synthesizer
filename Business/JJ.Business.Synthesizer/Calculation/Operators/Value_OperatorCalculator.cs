using System.Diagnostics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    [DebuggerDisplay("{_value}")]
    internal class Value_OperatorCalculator : OperatorCalculatorBase
    {
        private double _value;

        public Value_OperatorCalculator(double value)
        {
            _value = value;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _value;
        }
    }
}
