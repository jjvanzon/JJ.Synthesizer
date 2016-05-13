using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Resample_OperatorCalculator_Hermite : OperatorCalculatorBase_WithChildCalculators
    {
        private const double MINIMUM_SAMPLING_RATE = 0.01666666666666667; // Once a minute

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _samplingRateCalculator;
        private readonly DimensionStack _dimensionStack;

        private double _xMinus1;
        private double _x0;
        private double _x1;
        private double _x2;
        private double _dx;
        private double _yMinus1;
        private double _y0;
        private double _y1;
        private double _y2;

        public Resample_OperatorCalculator_Hermite(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase samplingRateCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator, samplingRateCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            // TODO: Resample with constant sampling rate does not have specialized calculators yet. Reactivate code line after those specialized calculators have been programmed.
            //if (samplingRateCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => samplingRateCalculator);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
            _dimensionStack = dimensionStack;

            Reset();
        }

        public override double Calculate()
        {
            double position = _dimensionStack.Get();

            // TODO: What if position goes in reverse?
            // TODO: What if _x0 or _x1 are way off? How will it correct itself?
            double x = position;
            if (x > _x1)
            {
                _dimensionStack.Push(_x1);
                double samplingRate = GetSamplingRate();
                _dimensionStack.Pop();

                _dx = 1.0 / samplingRate;
                _x1 += _dx;

                // x'es must be equidistant for the interpolation we use.
                _x0 = _x1 - _dx;
                _xMinus1 = _x0 - _dx;
                _x2 = _x1 + _dx;

                _dimensionStack.Push(_xMinus1);

                _yMinus1 = _signalCalculator.Calculate();

                _dimensionStack.Set(_x0);

                _y0 = _signalCalculator.Calculate();

                _dimensionStack.Set(_x1);

                _y1 = _signalCalculator.Calculate();

                _dimensionStack.Set(_x2);

                _y2 = _signalCalculator.Calculate();

                _dimensionStack.Pop();
            }

            double t = (x - _x0) / _dx;

            double y = Interpolator.Interpolate_Hermite_4pt3oX(_yMinus1, _y0, _y1, _y2, t);
            return y;
        }

        /// <summary> Gets the sampling rate, converts it to an absolute number and ensures a minimum value. </summary>
        private double GetSamplingRate()
        {
            double samplingRate = _samplingRateCalculator.Calculate();

            samplingRate = Math.Abs(samplingRate);

            if (samplingRate < MINIMUM_SAMPLING_RATE)
            {
                samplingRate = MINIMUM_SAMPLING_RATE;
            }

            return samplingRate;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get();

            _xMinus1 = CalculationHelper.VERY_LOW_VALUE;
            _x0 = position - Double.Epsilon;
            _x1 = position;
            _x2 = position + Double.Epsilon;
            _dx = Double.Epsilon;

            // Assume values begin at 0
            _yMinus1 = 0;
            _y0 = 0;
            _y1 = 0;
            _y2 = 0;

            base.Reset();
        }
    }
}
