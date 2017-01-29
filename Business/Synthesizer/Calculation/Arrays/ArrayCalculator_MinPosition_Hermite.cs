using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal class ArrayCalculator_MinPosition_Hermite : ArrayCalculatorBase_Hermite
    {
        public ArrayCalculator_MinPosition_Hermite(
            double[] array, double rate, double minPosition)
            : base(array, rate, minPosition)
        { }

        public ArrayCalculator_MinPosition_Hermite(
            double[] array, double rate, double minPosition, double valueBefore, double valueAfter)
            : base(array, rate, minPosition, valueBefore, valueAfter)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new double Calculate(double position)
        {
            // Return if sample not in range.
            // Execute it on the doubles, to prevent integer overflow.
            if (position < _minPosition) return _valueBefore;
            if (position > _maxPosition) return _valueAfter;
            if (double.IsNaN(position)) return 0.0;
            if (double.IsInfinity(position)) return 0.0;

            double t = (position - _minPosition) * _rate;

            return base.Calculate(t);
        }
    }
}