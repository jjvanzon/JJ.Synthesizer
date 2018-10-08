using System;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class Interpolate_OperatorCalculator_Base_2X1Y_LookAhead
        : Interpolate_OperatorCalculator_Base_2X1Y
    {
        private readonly VariableInput_OperatorCalculator _positionOutputCalculator;

        protected Interpolate_OperatorCalculator_Base_2X1Y_LookAhead(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator,
            OperatorCalculatorBase positionInputCalculator,
            VariableInput_OperatorCalculator positionOutputCalculator)
            : base(signalCalculator, samplingRateCalculator, positionInputCalculator)
            => _positionOutputCalculator =
                   positionOutputCalculator ?? throw new ArgumentNullException(nameof(positionOutputCalculator));

        protected sealed override void SetNextSample()
        {
            _x1 += Dx();

            double originalPosition = _positionOutputCalculator._value;
            _positionOutputCalculator._value = _x0;

            _y0 = _signalCalculator.Calculate();

            _positionOutputCalculator._value = originalPosition;
        }

        protected sealed override void SetPreviousSample()
        {
            _x0 -= Dx();

            double originalPosition = _positionOutputCalculator._value;
            _positionOutputCalculator._value = _x0;

            _y0 = _signalCalculator.Calculate();

            _positionOutputCalculator._value = originalPosition;
        }
    }
}