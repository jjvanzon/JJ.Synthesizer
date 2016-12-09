using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Hold_OperatorCalculator_VarSignal : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private double _value;

        public Hold_OperatorCalculator_VarSignal(OperatorCalculatorBase signalCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);

            _signalCalculator = signalCalculator;

            ResetPrivate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            return _value;
        }

        public override void Reset()
        {
            ResetPrivate();

            // Do not call base.Reset,
            // because the Hold operator is special,
            // in that it does not reset the calculation,
            // but gets a value from it upon reset.
        }

        private void ResetPrivate()
        {
            _value = _signalCalculator.Calculate();
        }
    }
}