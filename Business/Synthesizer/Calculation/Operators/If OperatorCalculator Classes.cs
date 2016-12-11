using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class If_OperatorCalculator_VarCondition_VarThen_VarElse : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _conditionCalculator;
        private readonly OperatorCalculatorBase _thenCalculator;
        private readonly OperatorCalculatorBase _elseCalculator;

        public If_OperatorCalculator_VarCondition_VarThen_VarElse(
            OperatorCalculatorBase conditionCalculator,
            OperatorCalculatorBase thenCalculator,
            OperatorCalculatorBase elseCalculator)
            : base(new OperatorCalculatorBase[] { conditionCalculator, thenCalculator, elseCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(conditionCalculator, () => conditionCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(thenCalculator, () => thenCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(elseCalculator, () => elseCalculator);

            _conditionCalculator = conditionCalculator;
            _thenCalculator = thenCalculator;
            _elseCalculator = elseCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double condition = _conditionCalculator.Calculate();

            bool conditionIsTrue = condition != 0.0;

            if (conditionIsTrue)
            {
                double then = _thenCalculator.Calculate();
                return then;
            }
            else
            {
                double @else = _elseCalculator.Calculate();
                return @else;
            }
        }
    }

    internal class If_OperatorCalculator_VarCondition_ConstThen_VarElse : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _conditionCalculator;
        private readonly double _then;
        private readonly OperatorCalculatorBase _elseCalculator;

        public If_OperatorCalculator_VarCondition_ConstThen_VarElse(
            OperatorCalculatorBase conditionCalculator,
            double then,
            OperatorCalculatorBase elseCalculator)
            : base(new OperatorCalculatorBase[] { conditionCalculator, elseCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(conditionCalculator, () => conditionCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(elseCalculator, () => elseCalculator);

            _conditionCalculator = conditionCalculator;
            _then = then;
            _elseCalculator = elseCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double condition = _conditionCalculator.Calculate();

            bool conditionIsTrue = condition != 0.0;

            if (conditionIsTrue)
            {
                return _then;
            }
            else
            {
                double @else = _elseCalculator.Calculate();
                return @else;
            }
        }
    }

    internal class If_OperatorCalculator_VarCondition_VarThen_ConstElse : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _conditionCalculator;
        private readonly OperatorCalculatorBase _thenCalculator;
        private readonly double _else;

        public If_OperatorCalculator_VarCondition_VarThen_ConstElse(
            OperatorCalculatorBase conditionCalculator,
            OperatorCalculatorBase thenCalculator,
            double @else)
            : base(new OperatorCalculatorBase[] { conditionCalculator, thenCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(conditionCalculator, () => conditionCalculator);
            OperatorCalculatorHelper.AssertChildOperatorCalculator(thenCalculator, () => thenCalculator);

            _conditionCalculator = conditionCalculator;
            _thenCalculator = thenCalculator;
            _else = @else;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double condition = _conditionCalculator.Calculate();

            bool conditionIsTrue = condition != 0.0;

            if (conditionIsTrue)
            {
                double then = _thenCalculator.Calculate();
                return then;
            }
            else
            {
                return _else;
            }
        }
    }

    internal class If_OperatorCalculator_VarCondition_ConstThen_ConstElse : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _conditionCalculator;
        private readonly double _then;
        private readonly double _else;

        public If_OperatorCalculator_VarCondition_ConstThen_ConstElse(
            OperatorCalculatorBase conditionCalculator,
            double then,
            double @else)
            : base(new OperatorCalculatorBase[] { conditionCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(conditionCalculator, () => conditionCalculator);

            _conditionCalculator = conditionCalculator;
            _then = then;
            _else = @else;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double condition = _conditionCalculator.Calculate();

            bool conditionIsTrue = condition != 0.0;

            if (conditionIsTrue)
            {
                return _then;
            }
            else
            {
                return _else;
            }
        }
    }
}
