using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class Loop_OperatorCalculator_Base : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        protected double _origin;
        protected readonly DimensionStack _dimensionStack;
        protected readonly int _currentDimensionStackIndex;
        protected readonly int _previousDimensionStackIndex;

        public Loop_OperatorCalculator_Base(
            OperatorCalculatorBase signalCalculator,
            DimensionStack dimensionStack,
            IList<OperatorCalculatorBase> childOperatorCalculators)
            : base(childOperatorCalculators)
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract double? GetTransformedPosition();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double? transformedPosition = GetTransformedPosition();
            if (!transformedPosition.HasValue)
            {
                return 0;
            }

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition.Value);

            double value = _signalCalculator.Calculate();

            return value;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            _origin = position;

            double? transformedPosition = GetTransformedPosition();
            if (!transformedPosition.HasValue)
            {
                // TODO: There is no meaningful value to fall back to.
                // What to do?
                transformedPosition = position;
            }

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition.Value);

            base.Reset();
        }
    }
}