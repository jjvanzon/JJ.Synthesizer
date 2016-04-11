using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary>
    /// This variation on the Resample_OperatorCalculator
    /// does give some sense of a filter, but when looking at the wave output,
    /// I see peaks, that I cannot explain, but my hunch it that it has to do
    /// with t catching up with t1 too quickly.
    /// </summary>
    internal class Resample_OperatorCalculator_LineRememberT0 : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _samplingRateCalculator;

        public Resample_OperatorCalculator_LineRememberT0(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase samplingRateCalculator)
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
        }

        private double _t0;
        private double _x0;

        public override double Calculate(DimensionStack dimensionStack)
        {
            double t = dimensionStack.Get(DimensionEnum.Time);

            double samplingRate = _samplingRateCalculator.Calculate(dimensionStack);
            if (samplingRate == 0)
            {
                // TODO: Set fields if sampling rate is 0?
                return 0;
            }
            double dt = 1.0 / samplingRate;

            double t1 = _t0 + dt;
            if (t >= t1)
            {
                _t0 = t1;

                dimensionStack.Push(DimensionEnum.Time, _t0);
                _x0 = _signalCalculator.Calculate(dimensionStack);
                dimensionStack.Pop(DimensionEnum.Time);

                t1 = _t0 + dt;
            }

            dimensionStack.Push(DimensionEnum.Time, t1);
            double x1 = _signalCalculator.Calculate(dimensionStack);
            dimensionStack.Pop(DimensionEnum.Time);

            double dx = x1 - _x0;
            double a = dx / dt;

            double x = _x0 + a * (t - _t0);
            return x;
        }
    }
}
