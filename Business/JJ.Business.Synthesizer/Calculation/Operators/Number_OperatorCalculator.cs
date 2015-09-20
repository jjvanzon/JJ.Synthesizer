using System.Diagnostics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    [DebuggerDisplay("{_value}")]
    internal class Number_OperatorCalculator : OperatorCalculatorBase
    {
        private double _number;

        public Number_OperatorCalculator(double number)
        {
            _number = number;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _number;
        }
    }
}
