using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class Loop_OperatorCalculator_Base : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        protected readonly int _dimensionIndex;
        protected double _origin;

        public Loop_OperatorCalculator_Base(
            OperatorCalculatorBase signalCalculator,
            DimensionEnum dimensionEnum,
            IList<OperatorCalculatorBase> childOperatorCalculators)
            : base(childOperatorCalculators)
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);

            _signalCalculator = signalCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        protected abstract double? TransformPosition(DimensionStack dimensionStack);

        public override void Reset(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            _origin = position;

            double? transformedPosition = TransformPosition(dimensionStack);
            if (!transformedPosition.HasValue)
            {
                // TODO: There is no meaningful value to fall back to.
                // What to do?
                transformedPosition = position;
            }

            dimensionStack.Push(_dimensionIndex, transformedPosition.Value);
            base.Reset(dimensionStack);
            dimensionStack.Pop(_dimensionIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate(DimensionStack dimensionStack)
        {
            double? transformedPosition = TransformPosition(dimensionStack);
            if (!transformedPosition.HasValue)
            {
                return 0;
            }

            dimensionStack.Push(_dimensionIndex, transformedPosition.Value);
            double value = _signalCalculator.Calculate(dimensionStack);
            dimensionStack.Pop(_dimensionIndex);

            return value;
        }
    }
}