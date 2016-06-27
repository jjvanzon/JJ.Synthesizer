using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class ClosestExp_OperatorCalculator_VarInput_2ConstItems : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _inputCalculator;
        private readonly double _item1;
        private readonly double _item2;

        public ClosestExp_OperatorCalculator_VarInput_2ConstItems(
            OperatorCalculatorBase inputCalculator,
            double item1,
            double item2)
            : base(new OperatorCalculatorBase[] { inputCalculator })
        {
            if (inputCalculator == null) throw new NullException(() => inputCalculator);

            _inputCalculator = inputCalculator;
            _item1 = item1;
            _item2 = item2;
        }

        public override double Calculate()
        {
            double input = _inputCalculator.Calculate();

            double result = AggregateCalculator.ClosestExp(input, _item1, _item2);

            return result;
        }
    }
}