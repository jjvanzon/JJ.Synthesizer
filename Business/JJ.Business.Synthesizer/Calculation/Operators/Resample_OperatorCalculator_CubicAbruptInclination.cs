using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary>
    /// A weakness though is, that the sampling rate is remembered until the next sample,
    /// which may work poorly when a very low sampling rate is provided.
    /// </summary>
    internal class Resample_OperatorCalculator_CubicAbruptInclination : OperatorCalculatorBase_WithChildCalculators
    {
        private const double MINIMUM_SAMPLING_RATE = 0.01666666666666667; // Once a minute

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _samplingRateCalculator;
        private readonly int _dimensionIndex;

        public Resample_OperatorCalculator_CubicAbruptInclination(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase samplingRateCalculator,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator, 
                samplingRateCalculator
            })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            // TODO: Resample with constant sampling rate does not have specialized calculators yet. Reactivate code line after those specialized calculators have been programmed.
            //if (samplingRateCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => samplingRateCalculator);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        // HACK: These defaults are hacks that are meaningless in practice.
        private double _xMinus1 = -0.2;
        private double _x0 = 0;
        private double _x1 = 0.2;
        private double _x2 = 0.4;

        private double _yMinus1 = 0;
        private double _y0 = 0;
        private double _y1 = 12000;
        private double _y2 = -24000;

        private double _dx0 = 0.2;
        private double _dx1 = 0.2;
        private double _a0;
        private double _a1;

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double x = position;
            if (x > _x1)
            {
                _xMinus1 = _x0;
                _x0 = _x1;
                _x1 = _x2;

                _yMinus1 = _y0;
                _y0 = _y1;
                _y1 = _y2;

                _dx0 = _dx1;
                _a0 = _a1;

                dimensionStack.Push(_dimensionIndex, _x1);
                double samplingRate1 = GetSamplingRate(dimensionStack);
                dimensionStack.Pop(_dimensionIndex);
                // TODO: Handle SamplingRate 0.

                _dx1 = 1.0 / samplingRate1;
                _x2 += _dx1;

                dimensionStack.Push(_dimensionIndex, _x2);
                _y2 = _signalCalculator.Calculate(dimensionStack);
                dimensionStack.Pop(_dimensionIndex);

                _a1 = (_y2 - _y0) / (_x2 - _x0);

                if (Double.IsNaN(_a1))
                {
                    _a1 = 0;
                }
            }

            // TODO: What if _t1 exceeds _t2 already? What happens then?
            double dx = x - _x0;
            double t = 0;
            if (_dx0 != 0)
            {
                t = dx / _dx0;
            }

            // TODO: Figure out how to prevent t from becoming out of range.
            if (t > 1.0)
            {
                return 0;
            }
            else if (t < 0.0)
            {
                return 0;
            }

            double y = (1.0 - t) * (_y0 + _a0 * (x - _x0)) + t * (_y1 + _a1 * (x - _x1));
            return y;
        }

        /// <summary>
        /// Gets the sampling rate, converts it to an absolute number
        /// and ensures a minimum value.
        /// </summary>
        private double GetSamplingRate(DimensionStack dimensionStack)
        {
            double samplingRate = _samplingRateCalculator.Calculate(dimensionStack);

            samplingRate = Math.Abs(samplingRate);

            if (samplingRate < MINIMUM_SAMPLING_RATE)
            {
                samplingRate = MINIMUM_SAMPLING_RATE;
            }

            return samplingRate;
        }
    }
}
