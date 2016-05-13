using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary>
    /// Not used.
    /// 
    /// This backup class is the variation on Resample_OperatorCalculator_LineRememberT1
    /// that does not take going in reverse into consideration.
    /// 
    /// It seems to work, except for the artifacts that linear interpolation gives us.
    /// A weakness though is, that the sampling rate is remembered until the next sample,
    /// which may work poorly when a very low sampling rate is provided.
    /// </summary>
    internal class Resample_OperatorCalculator_LineRememberT1_Org : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _samplingRateCalculator;
        private readonly int _dimensionIndex;
        private readonly DimensionStack _dimensionStack;

        public Resample_OperatorCalculator_LineRememberT1_Org(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase samplingRateCalculator,
            DimensionEnum dimensionEnum,
            DimensionStack dimensionStack)
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

        private double _x0;
        private double _x1;
        private double _y0;
        private double _y1;
        private double _a;

        public override double Calculate()
        {
            double x = _dimensionStack.Get(_dimensionIndex);

            if (x >= _x1)
            {
                _x0 = _x1;
                _y0 = _y1;

                _dimensionStack.Push(_dimensionIndex, _x1);
                double samplingRate = _samplingRateCalculator.Calculate();
                _dimensionStack.Pop(_dimensionIndex);

                if (samplingRate == 0)
                {
                    _a = 0;
                }
                else
                {
                    double dx = 1.0 / samplingRate;

                    _x1 += dx;

                    _dimensionStack.Push(_dimensionIndex, _x1);
                    _y1 = _signalCalculator.Calculate();
                    _dimensionStack.Pop(_dimensionIndex);

                    double dy = _y1 - _y0;

                    _a = dy / dx;
                }
            }

            double y = _y0 + _a * (x - _x0);
            return y;
        }
    }
}
