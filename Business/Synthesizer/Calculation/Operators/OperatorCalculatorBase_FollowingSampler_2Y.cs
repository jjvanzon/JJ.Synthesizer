namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class OperatorCalculatorBase_FollowingSampler_2Y : OperatorCalculatorBase_FollowingSampler
    {
        protected double _yMinus1;
        protected double _y0;

        public OperatorCalculatorBase_FollowingSampler_2Y(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator,
            OperatorCalculatorBase positionCalculator,
            params OperatorCalculatorBase[] additionalChildCalculators)
            : base(signalCalculator, samplingRateCalculator, positionCalculator, additionalChildCalculators) { }

        protected override void ShiftForward() => Shift();
        protected override void ShiftBackward() => Shift();

        private void Shift() => _yMinus1 = _y0;

        protected override void SetNextSample() => SetSample();
        protected override void SetPreviousSample() => SetSample();

        private void SetSample() => _y0 = _signalCalculator.Calculate();
    }
}