using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculator_MinPosition_Stripe : ArrayCalculatorBase_Stripe
    {
        public ArrayCalculator_MinPosition_Stripe(
            double[] array, double rate, double minPosition) 
            : base(array, rate, minPosition)
        { }

        public ArrayCalculator_MinPosition_Stripe(
            double[] array, double rate, double minPosition, double valueBefore, double valueAfter)
            : base(array, rate, minPosition, valueBefore, valueAfter)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double CalculateValue(double position)
        {
            // Return if sample not in range.
            // Execute it on the doubles, to prevent integer overflow.
            if (position < _minPosition) return _valueBefore;
            if (position > _maxPosition) return _valueAfter;
            if (double.IsNaN(position)) return 0.0;
            if (double.IsInfinity(position)) return 0.0;

            double t = (position - _minPosition) * _rate;

            return base.CalculateValue(t);
        }
    }
}
