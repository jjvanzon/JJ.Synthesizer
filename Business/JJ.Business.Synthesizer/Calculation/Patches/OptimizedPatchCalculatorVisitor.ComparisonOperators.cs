using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    internal partial class OptimizedPatchCalculatorVisitor : OperatorVisitorBase
    {
        protected override void VisitEqual(Operator op)
        {
            OperatorCalculatorBase calculator;

            OperatorCalculatorBase calculatorA = _stack.Pop();
            OperatorCalculatorBase calculatorB = _stack.Pop();

            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

            double a = calculatorA.Calculate(0, 0);
            double b = calculatorB.Calculate(0, 0);

            bool aIsConst = calculatorA is Number_OperatorCalculator;
            bool bIsConst = calculatorB is Number_OperatorCalculator;

            if (aIsConst && bIsConst)
            {
                double value;

                if (a == b) value = 1.0;
                else value = 0.0;

                calculator = new Number_OperatorCalculator(value);
            }
            else if (!aIsConst && bIsConst)
            {
                calculator = new Equal_VarA_ConstB_OperatorCalculator(calculatorA, b);
            }
            else if (aIsConst && !bIsConst)
            {
                calculator = new Equal_ConstA_VarB_OperatorCalculator(a, calculatorB);
            }
            else if (!aIsConst && !bIsConst)
            {
                calculator = new Equal_VarA_VarB_OperatorCalculator(calculatorA, calculatorB);
            }
            else
            {
                throw new NoCalculatorException(MethodBase.GetCurrentMethod());
            }

            _stack.Push(calculator);
        }
    }
}
