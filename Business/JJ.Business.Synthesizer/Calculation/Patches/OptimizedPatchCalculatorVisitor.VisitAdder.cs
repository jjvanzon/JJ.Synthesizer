

using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Operators;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    internal partial class OptimizedPatchCalculatorVisitor
    {
	    protected override void VisitAdder(Operator op)
        {
            OperatorCalculatorBase calculator;

            List<OperatorCalculatorBase> operandCalculators = new List<OperatorCalculatorBase>();

            for (int i = 0; i < op.Inlets.Count; i++)
            {
                OperatorCalculatorBase operandCalculator =  _stack.Pop();

                if (operandCalculator != null)
                {
                    operandCalculators.Add(operandCalculator);
                }
            }

            switch (operandCalculators.Count)
            {
                case 0:
                    calculator = new Zero_OperatorCalculator();
                    break;

                case 1:
                    calculator = operandCalculators[0];
                    break;

                case 2:
                    calculator = new Add_OperatorCalculator(operandCalculators[0], operandCalculators[1]);
                    break;


                default:
                    calculator = new Adder_OperatorCalculator(operandCalculators.ToArray());
                    break;
            }

            _stack.Push(calculator);
        }
	}
}
