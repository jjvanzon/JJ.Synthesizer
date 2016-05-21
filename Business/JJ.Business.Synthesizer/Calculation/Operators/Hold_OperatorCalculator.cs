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

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            return _value;
        }

        public override void Reset()
        {
            ResetNonRecursive();

            base.Reset();
        }

        private void ResetNonRecursive()
        {
            _value = _signalCalculator.Calculate();
        }
    }
}