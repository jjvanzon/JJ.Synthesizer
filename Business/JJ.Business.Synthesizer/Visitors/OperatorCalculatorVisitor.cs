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
            Outlet operandAOutlet = op.Inlets[Add.OPERAND_A_INDEX].Input;
            Outlet operandBOutlet = op.Inlets[Add.OPERAND_B_INDEX].Input;

            if (operandAOutlet == null || operandBOutlet == null)
            {
                OperatorCalculatorBase valueCalculator = new ValueCalculator(0);
                _stack.Push(valueCalculator);
            }

            Visit(operandAOutlet.Operator);
            OperatorCalculatorBase operandACalculator = _stack.Pop();

            Visit(operandBOutlet.Operator);
            OperatorCalculatorBase operandBCalculator = _stack.Pop();

            OperatorCalculatorBase calculator = new AddCalculator(operandACalculator, operandBCalculator);
            _stack.Push(calculator);

            // For now the base does nothing.
            base.VisitAdd(op);
        }

        protected override void VisitValueOperator(Operator op)
        {
            double value = op.AsValueOperator.Value;

            OperatorCalculatorBase valueCalculator = new ValueCalculator(value);

            _stack.Push(valueCalculator);
        }
    }
}
