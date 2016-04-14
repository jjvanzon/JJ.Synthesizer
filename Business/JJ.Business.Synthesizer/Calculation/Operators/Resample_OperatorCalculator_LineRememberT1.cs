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
    internal class Resample_OperatorCalculator_LineRememberT1 : OperatorCalculatorBase_WithChildCalculators
    {
        private const double MINIMUM_SAMPLING_RATE = 0.01666666666666667; // Once a minute

        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _samplingRateCalculator;
        private readonly int _dimensionIndex;

        private double _x0 = CalculationHelper.VERY_HIGH_VALUE;
        private double _x1 = CalculationHelper.VERY_LOW_VALUE;
        private double _y0;
        private double _y1;
        private double _a;

        public Resample_OperatorCalculator_LineRememberT1(
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
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double x = dimensionStack.Get(_dimensionIndex);

            if (x > _x1)
            {
                _x0 = _x1;
                _y0 = _y1;

                dimensionStack.Push(_dimensionIndex, _x1);
                double samplingRate = GetSamplingRate(dimensionStack);
                dimensionStack.Pop(_dimensionIndex);

                if (samplingRate == 0) // Minimum samplingRate value might become variable in the near future, so could be 0.
                {
                    _a = 0;
                }
                else
                {
                    double dx = 1.0 / samplingRate;

                    _x1 += dx;

                    dimensionStack.Push(_dimensionIndex, _x1);
                    _y1 = _signalCalculator.Calculate(dimensionStack);
                    dimensionStack.Pop(_dimensionIndex);

                    double dy = _y1 - _y0;
                    _a = dy / dx;
                }
            }
            else if (x < _x0)
            {
                // Going in reverse, take sample in reverse position.
                _x1 = _x0;
                _y1 = _y0;

                dimensionStack.Push(_dimensionIndex, _x0);
                double samplingRate = GetSamplingRate(dimensionStack);
                dimensionStack.Pop(_dimensionIndex);

                if (samplingRate == 0)
                {
                    _a = 0;
                }
                else
                {
                    double dx = 1.0 / samplingRate;

                    _x0 -= dx;

                    dimensionStack.Push(_dimensionIndex, _x0);
                    _y0 = _signalCalculator.Calculate(dimensionStack);
                    dimensionStack.Pop(_dimensionIndex);

                    double dy = _y1 - _y0;
                    _a = dy / dx;
                }
            }

            double y = _y0 + _a * (x - _x0);
            return y;
        }

        /// <summary>
        /// Gets the sampling rate, converts it to an absolute number
        /// and ensures a minimum value.
        /// </summary>
        private double GetSamplingRate(DimensionStack dimensionStack)
        {
            // _x1 was recently (2015-08-22) corrected to x which might make going in reverse work better.
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
