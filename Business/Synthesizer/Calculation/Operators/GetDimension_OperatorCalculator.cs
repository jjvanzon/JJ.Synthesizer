using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class GetDimension_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _positionCalculator;

        public GetDimension_OperatorCalculator(OperatorCalculatorBase positionCalculator)
        {
            _positionCalculator = positionCalculator ?? throw new ArgumentNullException(nameof(positionCalculator));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate() => _positionCalculator.Calculate();
    }
}
