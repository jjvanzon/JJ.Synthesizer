using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class If_VarCondition_VarThen_VarElse_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _conditionCalculator;
        private readonly OperatorCalculatorBase _thenCalculator;
        private readonly OperatorCalculatorBase _elseCalculator;

        public If_VarCondition_VarThen_VarElse_OperatorCalculator(
            OperatorCalculatorBase conditionCalculator,
            OperatorCalculatorBase thenCalculator,
            OperatorCalculatorBase elseCalculator)
            : base(new OperatorCalculatorBase[] { conditionCalculator, thenCalculator, elseCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(conditionCalculator, () => conditionCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(thenCalculator, () => thenCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(elseCalculator, () => elseCalculator);

            _conditionCalculator = conditionCalculator;
            _thenCalculator = thenCalculator;
            _elseCalculator = elseCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double condition = _conditionCalculator.Calculate(dimensionStack);
            double then = _thenCalculator.Calculate(dimensionStack);
            double @else = _elseCalculator.Calculate(dimensionStack);

            bool conditionIsTrue = condition != 0.0;

            if (conditionIsTrue)
            {
                return then;
            }
            else
            {
                return @else;
            }
        }
    }

    internal class If_VarCondition_ConstThen_VarElse_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _conditionCalculator;
        private readonly double _then;
        private readonly OperatorCalculatorBase _elseCalculator;

        public If_VarCondition_ConstThen_VarElse_OperatorCalculator(
            OperatorCalculatorBase conditionCalculator,
            double then,
            OperatorCalculatorBase elseCalculator)
            : base(new OperatorCalculatorBase[] { conditionCalculator, elseCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(conditionCalculator, () => conditionCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(elseCalculator, () => elseCalculator);

            _conditionCalculator = conditionCalculator;
            _then = then;
            _elseCalculator = elseCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double condition = _conditionCalculator.Calculate(dimensionStack);
            double @else = _elseCalculator.Calculate(dimensionStack);

            bool conditionIsTrue = condition != 0.0;

            if (conditionIsTrue)
            {
                return _then;
            }
            else
            {
                return @else;
            }
        }
    }

    internal class If_VarCondition_VarThen_ConstElse_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _conditionCalculator;
        private readonly OperatorCalculatorBase _thenCalculator;
        private readonly double _else;

        public If_VarCondition_VarThen_ConstElse_OperatorCalculator(
            OperatorCalculatorBase conditionCalculator,
            OperatorCalculatorBase thenCalculator,
            double @else)
            : base(new OperatorCalculatorBase[] { conditionCalculator, thenCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(conditionCalculator, () => conditionCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(thenCalculator, () => thenCalculator);

            _conditionCalculator = conditionCalculator;
            _thenCalculator = thenCalculator;
            _else = @else;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double condition = _conditionCalculator.Calculate(dimensionStack);
            double then = _thenCalculator.Calculate(dimensionStack);

            bool conditionIsTrue = condition != 0.0;

            if (conditionIsTrue)
            {
                return then;
            }
            else
            {
                return _else;
            }
        }
    }

    internal class If_VarCondition_ConstThen_ConstElse_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _conditionCalculator;
        private readonly double _then;
        private readonly double _else;

        public If_VarCondition_ConstThen_ConstElse_OperatorCalculator(
            OperatorCalculatorBase conditionCalculator,
            double then,
            double @else)
            : base(new OperatorCalculatorBase[] { conditionCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(conditionCalculator, () => conditionCalculator);

            _conditionCalculator = conditionCalculator;
            _then = then;
            _else = @else;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double condition = _conditionCalculator.Calculate(dimensionStack);

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
