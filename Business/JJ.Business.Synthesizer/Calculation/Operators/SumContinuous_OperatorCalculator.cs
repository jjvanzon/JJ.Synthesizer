using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SumContinuous_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _fromCalculator;
        private readonly OperatorCalculatorBase _tillCalculator;
        protected readonly OperatorCalculatorBase _sampleCountCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        protected double _value;

        public SumContinuous_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase sampleCountCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator, fromCalculator, tillCalculator, sampleCountCalculator })
        {
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _fromCalculator = fromCalculator;
            _tillCalculator = tillCalculator;
            _sampleCountCalculator = sampleCountCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            ResetNonRecursive();
        }

        public override double Calculate()
        {
            return _value;
        }

        public override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        protected virtual void ResetNonRecursive()
        {
            double from = _fromCalculator.Calculate();
            double till = _tillCalculator.Calculate();
            double sampleCount = _sampleCountCalculator.Calculate();

            if (sampleCount <= 0)
            {
                _value = 0;
                return;
            }

            double length = till - from;
            double step = length / sampleCount;
            bool isForward = length > 0.0;

            double position = from;

#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
#if !USE_INVAR_INDICES
            _dimensionStack.Set(position);
#else
            _dimensionStack.Set(_dimensionStackIndex, position);
#endif
            double sum = _signalCalculator.Calculate();
            position += step;

            if (isForward)
            {
                while (position <= till)
                {

#if !USE_INVAR_INDICES
                    _dimensionStack.Set(position);
#else
                    _dimensionStack.Set(_dimensionStackIndex, position);
#endif
                    double value = _signalCalculator.Calculate();
                    sum += value;

                    position += step;
                }
            }
            else
            {
                // Is backwards
                while (position >= till)
                {

#if !USE_INVAR_INDICES
                    _dimensionStack.Set(position);
#else
                    _dimensionStack.Set(_dimensionStackIndex, position);
#endif
                    double newValue = _signalCalculator.Calculate();
                    double value = _signalCalculator.Calculate();
                    sum += value;

                    position += step;
                }
            }

            _value = sum;
        }
    }
}
