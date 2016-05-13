using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Round_VarStep_VarOffSet_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _stepCalculator;
        private readonly OperatorCalculatorBase _offsetCalculator;

        public Round_VarStep_VarOffSet_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase stepCalculator,
            OperatorCalculatorBase offsetCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator, stepCalculator, offsetCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(stepCalculator, () => stepCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(offsetCalculator, () => offsetCalculator);

            _signalCalculator = signalCalculator;
            _stepCalculator = stepCalculator;
            _offsetCalculator = offsetCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double signal = _signalCalculator.Calculate();
            double step = _stepCalculator.Calculate();
            double offset = _offsetCalculator.Calculate();

            double result = Maths.RoundWithStep(signal, step, offset);
            return result;
        }
    }

    internal class Round_VarStep_ConstOffSet_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _stepCalculator;
        private readonly double _offset;

        public Round_VarStep_ConstOffSet_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase stepCalculator,
            double offset)
            : base(new OperatorCalculatorBase[] { signalCalculator, stepCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(stepCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertRoundOffset(offset);

            _signalCalculator = signalCalculator;
            _stepCalculator = stepCalculator;
            _offset = offset;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double signal = _signalCalculator.Calculate();
            double step = _stepCalculator.Calculate();

            double result = Maths.RoundWithStep(signal, step, _offset);
            return result;
        }
    }

    internal class Round_VarStep_ZeroOffSet_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _stepCalculator;

        public Round_VarStep_ZeroOffSet_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase stepCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator, stepCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(stepCalculator, () => stepCalculator);

            _signalCalculator = signalCalculator;
            _stepCalculator = stepCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double signal = _signalCalculator.Calculate();
            double step = _stepCalculator.Calculate();

            double result = Maths.RoundWithStep(signal, step);
            return result;
        }
    }

    internal class Round_ConstStep_VarOffSet_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _step;
        private readonly OperatorCalculatorBase _offsetCalculator;

        public Round_ConstStep_VarOffSet_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double step,
            OperatorCalculatorBase offsetCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator, offsetCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertRoundStep(step);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(offsetCalculator, () => offsetCalculator);

            _signalCalculator = signalCalculator;
            _step = step;
            _offsetCalculator = offsetCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double signal = _signalCalculator.Calculate();
            double offset = _offsetCalculator.Calculate();

            double result = Maths.RoundWithStep(signal, _step, offset);
            return result;
        }
    }

    internal class Round_ConstStep_ConstOffSet_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _step;
        private readonly double _offset;

        public Round_ConstStep_ConstOffSet_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double step,
            double offset)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertRoundStep(step);
            OperatorCalculatorHelper.AssertRoundOffset(offset);

            _signalCalculator = signalCalculator;
            _step = step;
            _offset = offset;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double signal = _signalCalculator.Calculate();

            double result = Maths.RoundWithStep(signal, _step, _offset);
            return result;
        }
    }

    internal class Round_ConstStep_ZeroOffSet_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _step;

        public Round_ConstStep_ZeroOffSet_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double step)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertRoundStep(step);

            _signalCalculator = signalCalculator;
            _step = step;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double signal = _signalCalculator.Calculate();

            double result = Maths.RoundWithStep(signal, _step);
            return result;
        }
    }

    // Special cases

    internal class Round_ConstSignal_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _signal;
        private readonly OperatorCalculatorBase _stepCalculator;
        private readonly OperatorCalculatorBase _offsetCalculator;

        public Round_ConstSignal_OperatorCalculator(
            double signal,
            OperatorCalculatorBase stepCalculator,
            OperatorCalculatorBase offsetCalculator)
            : base(new OperatorCalculatorBase[] { offsetCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(stepCalculator, () => stepCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(offsetCalculator, () => offsetCalculator);

            _signal = signal;
            _stepCalculator = stepCalculator;
            _offsetCalculator = offsetCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double step = _stepCalculator.Calculate();
            double offset = _offsetCalculator.Calculate();

            double result = Maths.RoundWithStep(_signal, step, offset);
            return result;
        }
    }

    internal class Round_VarSignal_StepOne_OffsetZero : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;

        public Round_VarSignal_StepOne_OffsetZero(OperatorCalculatorBase signalCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);

            _signalCalculator = signalCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double signal = _signalCalculator.Calculate();

            double result = Math.Round(signal, MidpointRounding.AwayFromZero);
            return result;
        }
    }
}