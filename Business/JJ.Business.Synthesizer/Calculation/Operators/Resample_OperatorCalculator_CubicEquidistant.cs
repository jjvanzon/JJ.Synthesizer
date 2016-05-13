using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Business.Synthesizer.Enums;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary>
    /// A weakness though is, that the sampling rate is remembered until the next sample,
    /// which may work poorly when a very low sampling rate is provided.
    /// </summary>
    internal class Resample_OperatorCalculator_CubicEquidistant : OperatorCalculatorBase_WithChildCalculators
    {
        private const double MINIMUM_SAMPLING_RATE = 0.01666666666666667; // Once a minute

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _samplingRateCalculator;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        public Resample_OperatorCalculator_CubicEquidistant(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase samplingRateCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                samplingRateCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            // TODO: Resample with constant sampling rate does not have specialized calculators yet. Reactivate code line after those specialized calculators have been programmed.
            //if (samplingRateCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => samplingRateCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        // TODO: These are meaningless defaults.
        private double _x0 = 0.0;
        private double _x1 = 0.2;
        private double _dx = 0.2; 
        private double _yMinus1 = 0.0;
        private double _y0 = 0.0;
        private double _y1 = 12000.0;
        private double _y2 = -24000.0;

        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            double x = position;
            if (x > _x1)
            {
                _dimensionStack.Push(_dimensionIndex, _x1);

                double samplingRate = GetSamplingRate();

                _dx = 1.0 / samplingRate;
                _x1 += _dx;

                // x'es must be equidistant for the interpolation we use.
                _x0 = _x1 - _dx;
                double xMinus1 = _x0 - _dx;
                double x2 = _x1 + _dx;

                _dimensionStack.Set(_dimensionIndex, xMinus1);

                _yMinus1 = _signalCalculator.Calculate();

                _dimensionStack.Set(_dimensionIndex, _x0);

                _y0 = _signalCalculator.Calculate();

                _dimensionStack.Set(_dimensionIndex, _x1);

                _y1 = _signalCalculator.Calculate();

                _dimensionStack.Set(_dimensionIndex, x2);

                _y2 = _signalCalculator.Calculate();

                _dimensionStack.Pop(_dimensionIndex);
            }

            double t = (x - _x0) / _dx;

            double y = Interpolator.Interpolate_Cubic_Equidistant(_yMinus1, _y0, _y1, _y2, t);
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
    }
}
