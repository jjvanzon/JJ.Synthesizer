using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SineWithRate1_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly DimensionStack _dimensionStack;

        public SineWithRate1_OperatorCalculator(DimensionStack dimensionStack)
        {
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get();
            double value = SineCalculator.Sin(position);
            return value;
        }
    }
}
