using JJ.Framework.Exceptions;
using System;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Interpolate_OperatorCalculator_Hermite_LagBehind : OperatorCalculatorBase_WithChildCalculators
    {
        private const double MINIMUM_SAMPLING_RATE = 1.0 / 60.0; // Once a minute

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _samplingRateCalculator;
        private readonly OperatorCalculatorBase _positionInputCalculator;
        private readonly VariableInput_OperatorCalculator _positionOutputCalculator;

        private double _xMinus1;
        private double _x0;
        private double _x1;
        private double _x2;
        private double _dx1;
        private double _yMinus1;
        private double _y0;
        private double _y1;
        private double _y2;

        public Interpolate_OperatorCalculator_Hermite_LagBehind(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator,
            OperatorCalculatorBase positionInputCalculator,
            VariableInput_OperatorCalculator positionOutputCalculator)
            : base(new[] { signalCalculator, samplingRateCalculator, positionInputCalculator, positionOutputCalculator })
        {
            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
            _positionInputCalculator = positionInputCalculator;
            _positionOutputCalculator = positionOutputCalculator;

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double x = _positionInputCalculator.Calculate();
     
            // TODO: What if position goes in reverse?
            // TODO: What if _x0 or _x1 are way off? How will it correct itself?
            // When x goes past _x1 you must shift things.
            if (x > _x1)
            {
                // Shift the samples to the left.
                _xMinus1 = _x0;
                _x0 = _x1;
                _x1 = _x2;
                _yMinus1 = _y0;
                _y0 = _y1;
                _y1 = _y2;

                // Determine next sample
                _positionOutputCalculator._value = _x1;

                double samplingRate1 = GetSamplingRate();

                _dx1 = 1.0 / samplingRate1;
                _x2 = _x1 + _dx1;

                _positionOutputCalculator._value = _x2;

                _y2 = _signalCalculator.Calculate();
            }

            double t = (x - _x0) / _dx1;

            double y = Interpolator.Interpolate_Hermite_4pt3oX(_yMinus1, _y0, _y1, _y2, t);
            return y;
        }

        /// <summary> Gets the sampling rate, converts it to an absolute number and ensures a minimum value. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            base.Reset();

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ResetNonRecursive()
        {
            double x = _positionInputCalculator.Calculate();

            double y = _signalCalculator.Calculate();
            double samplingRate = GetSamplingRate();

            double dx = 1.0 / samplingRate;

            _xMinus1 = x - dx - dx;
            _x0 = x - dx;
            _x1 = x;
            _x2 = x + dx;
            _dx1 = dx;

            // Y's are just set at a more practical default than 0.
            _yMinus1 = y;
            _y0 = y;
            _y1 = y;
            _y2 = y;
        }
    }
}
