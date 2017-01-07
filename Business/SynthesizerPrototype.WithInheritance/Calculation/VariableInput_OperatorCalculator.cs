using System.Runtime.CompilerServices;

namespace JJ.Business.SynthesizerPrototype.WithInheritance.Calculation
{
    internal class VariableInput_OperatorCalculator : OperatorCalculatorBase
    {
        public VariableInput_OperatorCalculator(double defaultValue)
        {
            _value = defaultValue;
        }

        /// <summary> Public field for performance. </summary>
        public double _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            return _value;
        }
    }
}
