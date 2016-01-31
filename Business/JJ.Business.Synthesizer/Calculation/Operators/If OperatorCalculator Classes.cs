using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class If_VarCondition_VarThen_VarElse_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _conditionCalculator;
        private readonly OperatorCalculatorBase _elseCalculator;
        private readonly OperatorCalculatorBase _thenCalculator;

        public If_VarCondition_VarThen_VarElse_OperatorCalculator(
            OperatorCalculatorBase conditionCalculator,
            OperatorCalculatorBase thenCalculator,
            OperatorCalculatorBase elseCalculator)
            : base(new OperatorCalculatorBase[] { conditionCalculator, thenCalculator, elseCalculator })
        {
            // TODO: Make strict again, when there are multiple IfOperatorCalculators
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(conditionCalculator, () => conditionCalculator);
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(thenCalculator, () => thenCalculator);
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(elseCalculator, () => elseCalculator);

            _conditionCalculator = conditionCalculator;
            _thenCalculator = thenCalculator;
            _elseCalculator = elseCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double condition = _conditionCalculator.Calculate(time, channelIndex);
            double then = _thenCalculator.Calculate(time, channelIndex);
            double @else = _elseCalculator.Calculate(time, channelIndex);

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
}
