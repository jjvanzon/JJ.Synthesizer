using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary>
    /// Not used.
    /// This variation on the Resample_OperatorCalculator
    /// does not work when the sampling rate gradually changes,
    /// because the alignment of sampling changes with the gradual change.
    /// </summary>
    internal class Resample_OperatorCalculator_LineWithVaryingAlignment : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _samplingRateCalculator;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        public Resample_OperatorCalculator_LineWithVaryingAlignment(
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

        public override double Calculate()
        {
            double x = _dimensionStack.Get(_dimensionIndex);

            double samplingRate = _samplingRateCalculator.Calculate();

            double sampleLength = 1.0 / samplingRate;

            double remainder = x % sampleLength;

            double x0 = x - remainder;
            double x1 = x0 + sampleLength;
            double dx = x1 - x0;

            _dimensionStack.Push(_dimensionIndex, x0);
            double y0 = _signalCalculator.Calculate();

            _dimensionStack.Set(_dimensionIndex, x1);

            double y1 = _signalCalculator.Calculate();
            _dimensionStack.Pop(_dimensionIndex);

            double dy = y1 - y0;

            double a = dy / dx;

            double y = y0 + a * (x - x0);
            return y;
        }
    }
}
