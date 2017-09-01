namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class RangeOverOutlets_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _fromCalculator;
        private readonly OperatorCalculatorBase _stepCalculator;
        private readonly double _position;

        public RangeOverOutlets_OperatorCalculator(
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase stepCalculator,
            double position)
            : base(new[] { fromCalculator, stepCalculator })
        {
            _fromCalculator = fromCalculator;
            _stepCalculator = stepCalculator;
            _position = position;
        }

        public override double Calculate()
        {
            double from = _fromCalculator.Calculate();
            double step = _stepCalculator.Calculate();

            double result = from + step * _position;

            return result;
        }
    }
}
