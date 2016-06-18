using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class MaxContinuous_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _fromCalculator;
        private readonly OperatorCalculatorBase _tillCalculator;
        private readonly OperatorCalculatorBase _stepCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _value;

        public MaxContinuous_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator, fromCalculator, tillCalculator, stepCalculator })
        {
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _fromCalculator = fromCalculator;
            _tillCalculator = tillCalculator;
            _stepCalculator = stepCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;
        }

        public override double Calculate()
        {
            return _value;
        }

        public override void Reset()
        {
            base.Reset();

            double from = _fromCalculator.Calculate();
            double till = _tillCalculator.Calculate();
            double step = _stepCalculator.Calculate();

            double t = from;

            _dimensionStack.Set(t);
            double value = _signalCalculator.Calculate();

            t += step;

            while (t <= till)
            {
                _dimensionStack.Set(t);
                double value2 = _signalCalculator.Calculate();

                if (value2 > value)
                {
                    value = value2;
                }

                t += step;
            }

            _value = value;

//#if !USE_INVAR_INDICES
//            double position = _dimensionStack.Get();
//#else
//            double position = _dimensionStack.Get(_dimensionStackIndex);
//#endif
//#if ASSERT_INVAR_INDICES
//            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
//#endif

            throw new NotImplementedException();
        }
    }
}
