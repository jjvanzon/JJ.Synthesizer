using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Interpolate_OperatorCalculator_Block : OperatorCalculatorBase_WithChildCalculators
    {
        private const double MINIMUM_SAMPLING_RATE = 1.0 / 60.0; // Once a minute

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _samplingRateCalculator;
        private readonly OperatorCalculatorBase _positionInputCalculator;
        private readonly VariableInput_OperatorCalculator _positionOutputCalculator;

        private double _x0;
        private double _x1;
        private double _y0;

        public Interpolate_OperatorCalculator_Block(
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
            // TODO: What if _x1 is way off? How will it correct itself?
            // When x goes past _x1 you must shift things.
            if (x > _x1)
            {
                // Shift samples to the left
                _x0 = _x1;

                // Determine next sample
                _positionOutputCalculator._value = _x1;

                double samplingRate1 = GetSamplingRate();
                double dx1 = 1.0 / samplingRate1;
                _x1 += dx1;

                // It seems you should set x on the dimension stack
                // to _x0 here, but x on the dimension stack is the 'old' _x1, 
                // which is the new _x0. So x on the dimension stack is already _x0.
                _y0 = _signalCalculator.Calculate();
            }
            else if (x < _x0)
            {
                // Shift samples to the right.
                _x1 = _x0;

                // Determine next sample
                _positionOutputCalculator._value = _x0;

                double samplingRate0 = GetSamplingRate();
                double dx0 = 1.0 / samplingRate0;
                _x0 -= dx0;

                _positionOutputCalculator._value = _x0;

                _y0 = _signalCalculator.Calculate();
            }

            return _y0;
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

            _x0 = x;
            _x1 = x + dx;

            // Y's are just set at a slightly more practical default than 0.
            _y0 = y;
        }
    }
}