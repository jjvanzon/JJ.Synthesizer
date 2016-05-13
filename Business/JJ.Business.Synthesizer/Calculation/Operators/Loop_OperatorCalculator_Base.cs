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
        protected double _origin;
        protected readonly DimensionStack _dimensionStack;

        public Loop_OperatorCalculator_Base(
            OperatorCalculatorBase signalCalculator,
            DimensionStack dimensionStack,
            IList<OperatorCalculatorBase> childOperatorCalculators)
            : base(childOperatorCalculators)
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
        }

        protected abstract double? GetTransformedPosition();

        public override void Reset()
        {
            double position = _dimensionStack.Get();

            _origin = position;

            double? transformedPosition = GetTransformedPosition();
            if (!transformedPosition.HasValue)
            {
                // TODO: There is no meaningful value to fall back to.
                // What to do?
                transformedPosition = position;
            }

            _dimensionStack.Push(transformedPosition.Value);
            base.Reset();
            _dimensionStack.Pop();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double? transformedPosition = GetTransformedPosition();
            if (!transformedPosition.HasValue)
            {
                return 0;
            }

            _dimensionStack.Push(transformedPosition.Value);

            double value = _signalCalculator.Calculate();

            _dimensionStack.Pop();

            return value;
        }
    }
}