using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary>
    /// It seems to work, except for the artifacts that linear interpolation gives us.
    /// A weakness though is, that the sampling rate is remembered until the next sample,
    /// which may work poorly when a very low sampling rate is provided.
    /// </summary>
    internal class Resample_OperatorCalculator_LineRememberT1 : OperatorCalculatorBase
    {
        private const double MINIMUM_SAMPLING_RATE = 0.01666666666666667; // Once a minute

        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _samplingRateCalculator;

        private double _t0 = CalculationHelper.VERY_HIGH_VALUE;
        private double _t1 = CalculationHelper.VERY_LOW_VALUE;
        private double _x0;
        private double _x1;
        private double _a;

        public Resample_OperatorCalculator_LineRememberT1(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase samplingRateCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            // TODO: Resample with constant sampling rate does not have specialized calculators yet. Reactivate code line after those specialized calculators have been programmed.
            //if (samplingRateCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => samplingRateCalculator);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double t = dimensionStack.Get(DimensionEnum.Time);

            if (t > _t1)
            {
                _t0 = _t1;
                _x0 = _x1;

                dimensionStack.Push(DimensionEnum.Time, _t1);
                double samplingRate = GetSamplingRate(dimensionStack);
                dimensionStack.Pop(DimensionEnum.Time);

                if (samplingRate == 0) // Minimum samplingRate value might become variable in the near future, so could be 0.
                {
                    _a = 0;
                }
                else
                {
                    double dt = 1.0 / samplingRate;

                    _t1 += dt;

                    dimensionStack.Push(DimensionEnum.Time, _t1);
                    _x1 = _signalCalculator.Calculate(dimensionStack);
                    dimensionStack.Pop(DimensionEnum.Time);

                    double dx = _x1 - _x0;
                    _a = dx / dt;
                }
            }
            else if (t < _t0)
            {
                // Time going in reverse, take sample reverse in time.
                _t1 = _t0;
                _x1 = _x0;

                dimensionStack.Push(DimensionEnum.Time, _t0);
                double samplingRate = GetSamplingRate(dimensionStack);
                dimensionStack.Pop(DimensionEnum.Time);

                if (samplingRate == 0)
                {
                    _a = 0;
                }
                else
                {
                    double dt = 1.0 / samplingRate;

                    _t0 -= dt;

                    dimensionStack.Push(DimensionEnum.Time, _t0);
                    _x0 = _signalCalculator.Calculate(dimensionStack);
                    dimensionStack.Pop(DimensionEnum.Time);

                    double dx = _x1 - _x0;
                    _a = dx / dt;
                }
            }

            double x = _x0 + _a * (t - _t0);
            return x;
        }

        /// <summary>
        /// Gets the sampling rate, converts it to an absolute number
        /// and ensures a minimum value.
        /// </summary>
        private double GetSamplingRate(DimensionStack dimensionStack)
        {
            // _t1 was recently (2015-08-22) corrected to t which might make time going in reverse work better.
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
