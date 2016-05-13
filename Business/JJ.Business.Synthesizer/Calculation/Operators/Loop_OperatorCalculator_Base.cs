using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class Loop_OperatorCalculator_Base : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        protected readonly int _dimensionIndex;
        protected double _origin;
        protected readonly DimensionStacks _dimensionStack;

        public Loop_OperatorCalculator_Base(
            OperatorCalculatorBase signalCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack,
            IList<OperatorCalculatorBase> childOperatorCalculators)
            : base(childOperatorCalculators)
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        protected abstract double? GetTransformedPosition();

        public override void Reset()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            _origin = position;

            double? transformedPosition = GetTransformedPosition();
            if (!transformedPosition.HasValue)
            {
                // TODO: There is no meaningful value to fall back to.
                // What to do?
                transformedPosition = position;
            }

            _dimensionStack.Push(_dimensionIndex, transformedPosition.Value);
            base.Reset();
            _dimensionStack.Pop(_dimensionIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double? transformedPosition = GetTransformedPosition();
            if (!transformedPosition.HasValue)
            {
                return 0;
            }

            _dimensionStack.Push(_dimensionIndex, transformedPosition.Value);
            double value = _signalCalculator.Calculate();
            _dimensionStack.Pop(_dimensionIndex);

            return value;
        }
    }
}