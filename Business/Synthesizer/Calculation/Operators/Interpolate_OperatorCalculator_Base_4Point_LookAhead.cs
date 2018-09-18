namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class Interpolate_OperatorCalculator_Base_4Point_LookAhead : Interpolate_OperatorCalculator_Base_4Point
    {
        private readonly VariableInput_OperatorCalculator _positionOutputCalculator;

        public Interpolate_OperatorCalculator_Base_4Point_LookAhead(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator,
            OperatorCalculatorBase positionInputCalculator,
            VariableInput_OperatorCalculator positionOutputCalculator)
            : base(signalCalculator, samplingRateCalculator, positionInputCalculator)
            => _positionOutputCalculator = positionOutputCalculator;

        protected sealed override void SetNextSample()
        {
            _x2 += Dx();

            double originalPosition = _positionOutputCalculator._value;
            _positionOutputCalculator._value = _x2;

            _y2 = _signalCalculator.Calculate();

            _positionOutputCalculator._value = originalPosition;
        }

        protected sealed override void SetPreviousSample()
        {
            _xMinus1 -= Dx();

            double originalPosition = _positionOutputCalculator._value;
            _positionOutputCalculator._value = _xMinus1;

            _yMinus1 = _signalCalculator.Calculate();

            _positionOutputCalculator._value = originalPosition;
        }

        protected sealed override void ResetNonRecursive()
        {
            double dx = Dx();

            _x0 = _positionInputCalculator.Calculate();
            _y0 = _signalCalculator.Calculate();

            double originalPosition = _positionOutputCalculator._value;

            _xMinus1 = _x0 - dx;
            _positionOutputCalculator._value = _xMinus1;
            _yMinus1 = _signalCalculator.Calculate();

            _x1 = _x0 + dx;
            _positionOutputCalculator._value = _x1;
            _y1 = _signalCalculator.Calculate();

            _x2 = _x1 + dx;
            _positionOutputCalculator._value = _x2;
            _y2 = _signalCalculator.Calculate();

            _positionOutputCalculator._value = originalPosition;

            Precalculate();
        }
    }
}