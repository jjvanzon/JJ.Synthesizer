using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class Loop_OperatorCalculator_Base : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        protected double _origin;

        public Loop_OperatorCalculator_Base(
            OperatorCalculatorBase signalCalculator,
            IList<OperatorCalculatorBase> childOperatorCalculators)
            : base(childOperatorCalculators)
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);

            _signalCalculator = signalCalculator;
        }

        protected abstract double? TransformTime(DimensionStack dimensionStack);

        public override void Reset(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            _origin = time;

            double? transformedTime = TransformTime(dimensionStack);
            if (!transformedTime.HasValue)
            {
                // TODO: There is no meaningful value to fall back to.
                // What to do?
                transformedTime = time;
            }

            dimensionStack.Push(DimensionEnum.Time, transformedTime.Value);
            base.Reset(dimensionStack);
            dimensionStack.Pop(DimensionEnum.Time);
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double? transformedTime = TransformTime(dimensionStack);
            if (!transformedTime.HasValue)
            {
                return 0;
            }

            dimensionStack.Push(DimensionEnum.Time, transformedTime.Value);
            double value = _signalCalculator.Calculate(dimensionStack);
            dimensionStack.Pop(DimensionEnum.Time);

            return value;
        }
    }
}