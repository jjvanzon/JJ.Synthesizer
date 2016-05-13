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
        private readonly DimensionStack _dimensionStack;

        private double _x0;
        private double _y0;

        public Resample_OperatorCalculator_LineRememberT0(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase samplingRateCalculator,
            DimensionStack dimensionStack)
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
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
            _dimensionStack = dimensionStack;
        }

        public override double Calculate()
        {
            double x = _dimensionStack.Get();

            double samplingRate = _samplingRateCalculator.Calculate();
            if (samplingRate == 0)
            {
                // TODO: Set fields if sampling rate is 0?
                return 0;
            }
            double dx = 1.0 / samplingRate;

            double x1 = _x0 + dx;
            if (x >= x1)
            {
                _x0 = x1;

                _dimensionStack.Push(_x0);
                _y0 = _signalCalculator.Calculate();
                _dimensionStack.Pop();

                x1 = _x0 + dx;
            }

            _dimensionStack.Push(x1);
            double y1 = _signalCalculator.Calculate();
            _dimensionStack.Pop();

            double dy = y1 - _y0;
            double a = dy / dx;

            double y = _y0 + a * (x - _x0);
            return y;
        }
    }
}
