using System.Diagnostics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    [DebuggerDisplay("{DebuggerDisplay}")]
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

        private string DebuggerDisplay
        {
            get { return _number.ToString(); }
        }
    }
}
