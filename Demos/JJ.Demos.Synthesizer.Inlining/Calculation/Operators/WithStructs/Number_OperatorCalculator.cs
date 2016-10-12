using System.Runtime.CompilerServices;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs
{
    internal struct Number_OperatorCalculator : IOperatorCalculator
    {
        public double _number;

        public Number_OperatorCalculator(double number)
        {
            _number = number;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            return _number;
        }
    }
}
