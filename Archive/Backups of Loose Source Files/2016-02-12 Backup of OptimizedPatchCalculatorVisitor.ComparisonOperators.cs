//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using JJ.Business.Synthesizer.Calculation.Operators;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Data.Synthesizer;

//namespace JJ.Business.Synthesizer.Calculation.Patches
//{
//    internal partial class OptimizedPatchCalculatorVisitor : OperatorVisitorBase
//    {
//        protected override void VisitAnd(Operator op)
//        {
//            OperatorCalculatorBase calculator;

//            OperatorCalculatorBase calculatorA = _stack.Pop();
//            OperatorCalculatorBase calculatorB = _stack.Pop();

//            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
//            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

//            double a = calculatorA.Calculate(0, 0);
//            double b = calculatorB.Calculate(0, 0);

//            bool aIsConst = calculatorA is Number_OperatorCalculator;
//            bool bIsConst = calculatorB is Number_OperatorCalculator;

//            if (aIsConst && bIsConst)
//            {
//                double value;

//                bool aIsTrue = a != 0.0;
//                bool bIsTrue = b != 0.0;

//                if (aIsTrue && bIsTrue) value = 1.0;
//                else value = 0.0;

//                calculator = new Number_OperatorCalculator(value);
//            }
//            else if (!aIsConst && bIsConst)
//            {
//                calculator = new And_VarA_ConstB_OperatorCalculator(calculatorA, b);
//            }
//            else if (aIsConst && !bIsConst)
//            {
//                calculator = new And_ConstA_VarB_OperatorCalculator(a, calculatorB);
//            }
//            else if (!aIsConst && !bIsConst)
//            {
//                calculator = new And_VarA_VarB_OperatorCalculator(calculatorA, calculatorB);
//            }
//            else
//            {
//                throw new NoCalculatorException(MethodBase.GetCurrentMethod());
//            }

//            _stack.Push(calculator);
//        }

//        protected override void VisitEqual(Operator op)
//        {
//            OperatorCalculatorBase calculator;

//            OperatorCalculatorBase calculatorA = _stack.Pop();
//            OperatorCalculatorBase calculatorB = _stack.Pop();

//            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
//            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

//            double a = calculatorA.Calculate(0, 0);
//            double b = calculatorB.Calculate(0, 0);

//            bool aIsConst = calculatorA is Number_OperatorCalculator;
//            bool bIsConst = calculatorB is Number_OperatorCalculator;

//            if (aIsConst && bIsConst)
//            {
//                double value;

//                if (a == b) value = 1.0;
//                else value = 0.0;

//                calculator = new Number_OperatorCalculator(value);
//            }
//            else if (!aIsConst && bIsConst)
//            {
//                calculator = new Equal_VarA_ConstB_OperatorCalculator(calculatorA, b);
//            }
//            else if (aIsConst && !bIsConst)
//            {
//                calculator = new Equal_ConstA_VarB_OperatorCalculator(a, calculatorB);
//            }
//            else if (!aIsConst && !bIsConst)
//            {
//                calculator = new Equal_VarA_VarB_OperatorCalculator(calculatorA, calculatorB);
//            }
//            else
//            {
//                throw new NoCalculatorException(MethodBase.GetCurrentMethod());
//            }

//            _stack.Push(calculator);
//        }

//        protected override void VisitGreaterThan(Operator op)
//        {
//            OperatorCalculatorBase calculator;

//            OperatorCalculatorBase calculatorA = _stack.Pop();
//            OperatorCalculatorBase calculatorB = _stack.Pop();

//            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
//            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

//            double a = calculatorA.Calculate(0, 0);
//            double b = calculatorB.Calculate(0, 0);

//            bool aIsConst = calculatorA is Number_OperatorCalculator;
//            bool bIsConst = calculatorB is Number_OperatorCalculator;

//            if (aIsConst && bIsConst)
//            {
//                double value;

//                if (a > b) value = 1.0;
//                else value = 0.0;

//                calculator = new Number_OperatorCalculator(value);
//            }
//            else if (!aIsConst && bIsConst)
//            {
//                calculator = new GreaterThan_VarA_ConstB_OperatorCalculator(calculatorA, b);
//            }
//            else if (aIsConst && !bIsConst)
//            {
//                calculator = new GreaterThan_ConstA_VarB_OperatorCalculator(a, calculatorB);
//            }
//            else if (!aIsConst && !bIsConst)
//            {
//                calculator = new GreaterThan_VarA_VarB_OperatorCalculator(calculatorA, calculatorB);
//            }
//            else
//            {
//                throw new NoCalculatorException(MethodBase.GetCurrentMethod());
//            }

//            _stack.Push(calculator);
//        }

//        protected override void VisitGreaterThanOrEqual(Operator op)
//        {
//            OperatorCalculatorBase calculator;

//            OperatorCalculatorBase calculatorA = _stack.Pop();
//            OperatorCalculatorBase calculatorB = _stack.Pop();

//            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
//            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

//            double a = calculatorA.Calculate(0, 0);
//            double b = calculatorB.Calculate(0, 0);

//            bool aIsConst = calculatorA is Number_OperatorCalculator;
//            bool bIsConst = calculatorB is Number_OperatorCalculator;

//            if (aIsConst && bIsConst)
//            {
//                double value;

//                if (a >= b) value = 1.0;
//                else value = 0.0;

//                calculator = new Number_OperatorCalculator(value);
//            }
//            else if (!aIsConst && bIsConst)
//            {
//                calculator = new GreaterThanOrEqual_VarA_ConstB_OperatorCalculator(calculatorA, b);
//            }
//            else if (aIsConst && !bIsConst)
//            {
//                calculator = new GreaterThanOrEqual_ConstA_VarB_OperatorCalculator(a, calculatorB);
//            }
//            else if (!aIsConst && !bIsConst)
//            {
//                calculator = new GreaterThanOrEqual_VarA_VarB_OperatorCalculator(calculatorA, calculatorB);
//            }
//            else
//            {
//                throw new NoCalculatorException(MethodBase.GetCurrentMethod());
//            }

//            _stack.Push(calculator);
//        }

//        protected override void VisitLessThan(Operator op)
//        {
//            OperatorCalculatorBase calculator;

//            OperatorCalculatorBase calculatorA = _stack.Pop();
//            OperatorCalculatorBase calculatorB = _stack.Pop();

//            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
//            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

//            double a = calculatorA.Calculate(0, 0);
//            double b = calculatorB.Calculate(0, 0);

//            bool aIsConst = calculatorA is Number_OperatorCalculator;
//            bool bIsConst = calculatorB is Number_OperatorCalculator;

//            if (aIsConst && bIsConst)
//            {
//                double value;

//                if (a < b) value = 1.0;
//                else value = 0.0;

//                calculator = new Number_OperatorCalculator(value);
//            }
//            else if (!aIsConst && bIsConst)
//            {
//                calculator = new LessThan_VarA_ConstB_OperatorCalculator(calculatorA, b);
//            }
//            else if (aIsConst && !bIsConst)
//            {
//                calculator = new LessThan_ConstA_VarB_OperatorCalculator(a, calculatorB);
//            }
//            else if (!aIsConst && !bIsConst)
//            {
//                calculator = new LessThan_VarA_VarB_OperatorCalculator(calculatorA, calculatorB);
//            }
//            else
//            {
//                throw new NoCalculatorException(MethodBase.GetCurrentMethod());
//            }

//            _stack.Push(calculator);
//        }

//        protected override void VisitLessThanOrEqual(Operator op)
//        {
//            OperatorCalculatorBase calculator;

//            OperatorCalculatorBase calculatorA = _stack.Pop();
//            OperatorCalculatorBase calculatorB = _stack.Pop();

//            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
//            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

//            double a = calculatorA.Calculate(0, 0);
//            double b = calculatorB.Calculate(0, 0);

//            bool aIsConst = calculatorA is Number_OperatorCalculator;
//            bool bIsConst = calculatorB is Number_OperatorCalculator;

//            if (aIsConst && bIsConst)
//            {
//                double value;

//                if (a <= b) value = 1.0;
//                else value = 0.0;

//                calculator = new Number_OperatorCalculator(value);
//            }
//            else if (!aIsConst && bIsConst)
//            {
//                calculator = new LessThanOrEqual_VarA_ConstB_OperatorCalculator(calculatorA, b);
//            }
//            else if (aIsConst && !bIsConst)
//            {
//                calculator = new LessThanOrEqual_ConstA_VarB_OperatorCalculator(a, calculatorB);
//            }
//            else if (!aIsConst && !bIsConst)
//            {
//                calculator = new LessThanOrEqual_VarA_VarB_OperatorCalculator(calculatorA, calculatorB);
//            }
//            else
//            {
//                throw new NoCalculatorException(MethodBase.GetCurrentMethod());
//            }

//            _stack.Push(calculator);
//        }

//        protected override void VisitNot(Operator op)
//        {
//            OperatorCalculatorBase calculator;

//            OperatorCalculatorBase calculatorX = _stack.Pop();

//            calculatorX = calculatorX ?? new Zero_OperatorCalculator();

//            double x = calculatorX.Calculate(0, 0);

//            bool xIsConst = calculatorX is Number_OperatorCalculator;

//            if (xIsConst)
//            {
//                double value;

//                bool aIsFalse = x == 0.0;

//                if (aIsFalse) value = 1.0;
//                else value = 0.0;

//                calculator = new Number_OperatorCalculator(value);
//            }
//            else if (!xIsConst)
//            {
//                calculator = new Not_OperatorCalculator(calculatorX);
//            }
//            else
//            {
//                throw new NoCalculatorException(MethodBase.GetCurrentMethod());
//            }

//            _stack.Push(calculator);
//        }

//        protected override void VisitNotEqual(Operator op)
//        {
//            OperatorCalculatorBase calculator;

//            OperatorCalculatorBase calculatorA = _stack.Pop();
//            OperatorCalculatorBase calculatorB = _stack.Pop();

//            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
//            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

//            double a = calculatorA.Calculate(0, 0);
//            double b = calculatorB.Calculate(0, 0);

//            bool aIsConst = calculatorA is Number_OperatorCalculator;
//            bool bIsConst = calculatorB is Number_OperatorCalculator;

//            if (aIsConst && bIsConst)
//            {
//                double value;

//                if (a != b) value = 1.0;
//                else value = 0.0;

//                calculator = new Number_OperatorCalculator(value);
//            }
//            else if (!aIsConst && bIsConst)
//            {
//                calculator = new NotEqual_VarA_ConstB_OperatorCalculator(calculatorA, b);
//            }
//            else if (aIsConst && !bIsConst)
//            {
//                calculator = new NotEqual_ConstA_VarB_OperatorCalculator(a, calculatorB);
//            }
//            else if (!aIsConst && !bIsConst)
//            {
//                calculator = new NotEqual_VarA_VarB_OperatorCalculator(calculatorA, calculatorB);
//            }
//            else
//            {
//                throw new NoCalculatorException(MethodBase.GetCurrentMethod());
//            }

//            _stack.Push(calculator);
//        }

//        protected override void VisitOr(Operator op)
//        {
//            OperatorCalculatorBase calculator;

//            OperatorCalculatorBase calculatorA = _stack.Pop();
//            OperatorCalculatorBase calculatorB = _stack.Pop();

//            calculatorA = calculatorA ?? new Zero_OperatorCalculator();
//            calculatorB = calculatorB ?? new Zero_OperatorCalculator();

//            double a = calculatorA.Calculate(0, 0);
//            double b = calculatorB.Calculate(0, 0);

//            bool aIsConst = calculatorA is Number_OperatorCalculator;
//            bool bIsConst = calculatorB is Number_OperatorCalculator;

//            if (aIsConst && bIsConst)
//            {
//                double value;

//                bool aIsTrue = a != 0.0;
//                bool bIsTrue = b != 0.0;

//                if (aIsTrue || bIsTrue) value = 1.0;
//                else value = 0.0;

//                calculator = new Number_OperatorCalculator(value);
//            }
//            else if (!aIsConst && bIsConst)
//            {
//                calculator = new Or_VarA_ConstB_OperatorCalculator(calculatorA, b);
//            }
//            else if (aIsConst && !bIsConst)
//            {
//                calculator = new Or_ConstA_VarB_OperatorCalculator(a, calculatorB);
//            }
//            else if (!aIsConst && !bIsConst)
//            {
//                calculator = new Or_VarA_VarB_OperatorCalculator(calculatorA, calculatorB);
//            }
//            else
//            {
//                throw new NoCalculatorException(MethodBase.GetCurrentMethod());
//            }

//            _stack.Push(calculator);
//        }
//    }
//}