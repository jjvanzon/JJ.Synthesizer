using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Calculation.Curves;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Curve_MinTime_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinTime _curveCalculator;
        private double _origin;

        public Curve_MinTime_OperatorCalculator(CurveCalculator_MinTime curveCalculator)
        {
            if (curveCalculator == null) throw new NullException(() => curveCalculator);

            _curveCalculator = curveCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double transformedTime = time - _origin;
            double result = _curveCalculator.CalculateValue(transformedTime);
            return result;
        }

        public override void Reset(double time, int channelIndex)
        {
            // HACK: Temporary hack to have some sort of response to a NoteStart event's changing the origin.
            // It does not behave well when time is abused as a different domain, e.g. Harmonic Number.
            _origin = time;

            base.Reset(time, channelIndex);
        }
    }

    internal class Curve_MinTimeZero_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly CurveCalculator_MinTimeZero _curveCalculator;
        private double _origin;

        public Curve_MinTimeZero_OperatorCalculator(CurveCalculator_MinTimeZero curveCalculator)
        {
            if (curveCalculator == null) throw new NullException(() => curveCalculator);

            _curveCalculator = curveCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double transformedTime = time - _origin;
            double result = _curveCalculator.CalculateValue(transformedTime);
            return result;
        }

        public override void Reset(double time, int channelIndex)
        {
            // HACK: Temporary hack to have some sort of response to a NoteStart event's changing the origin.
            // It does not behave well when time is abused as a different domain, e.g. Harmonic Number.
            _origin = time;

            base.Reset(time, channelIndex);
        }
    }
}
