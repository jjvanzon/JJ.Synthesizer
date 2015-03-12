using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorCalculatorVisitor : OperatorVisitorBase
    {
        private Stack<OperatorCalculatorBase> _stack;

        public OperatorCalculatorBase Execute(Operator rootOperator)
        {
            _stack = new Stack<OperatorCalculatorBase>();

            Visit(rootOperator);

            return _stack.Pop();
        }

        protected override void VisitAdd(Operator op)
        {
            OperatorCalculatorBase operandACalculator = _stack.Pop();
            OperatorCalculatorBase operandBCalculator = _stack.Pop();

            if (operandACalculator == null || operandBCalculator == null)
            {
                OperatorCalculatorBase valueCalculator = new ValueCalculator(0);
                _stack.Push(valueCalculator);
            }
            else
            {
                OperatorCalculatorBase calculator = new AddCalculator(operandACalculator, operandBCalculator);
                _stack.Push(calculator);
            }

            base.VisitAdd(op);
        }

        protected override void VisitInlet(Inlet inlet)
        {
            if (inlet.Input == null)
            {
                _stack.Push(null);
            }

            base.VisitInlet(inlet);
        }

        protected override void VisitValueOperator(Operator op)
        {
            double value = op.AsValueOperator.Value;

            OperatorCalculatorBase valueCalculator = new ValueCalculator(value);

            _stack.Push(valueCalculator);
        }
    }
}
