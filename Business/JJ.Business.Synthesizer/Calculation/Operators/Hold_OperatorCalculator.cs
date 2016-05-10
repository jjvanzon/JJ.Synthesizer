using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Hold_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private double _value;

        public Hold_OperatorCalculator(OperatorCalculatorBase signalCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);

            _signalCalculator = signalCalculator;

            Reset(new DimensionStack());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate(DimensionStack dimensionStack)
        {
            return _value;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            _value = _signalCalculator.Calculate(dimensionStack);

            base.Reset(dimensionStack);
        }
    }
}