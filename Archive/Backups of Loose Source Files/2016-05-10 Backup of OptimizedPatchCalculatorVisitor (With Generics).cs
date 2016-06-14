//using System;
//using System.Linq;
//using JJ.Business.Synthesizer.Calculation.Operators;
//using JJ.Data.Synthesizer;
//using JJ.Framework.Reflection;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation.Patches
//{
//    internal class OptimizedPatchCalculatorVisitor_Backup : OperatorVisitorBase
//    {
//        protected override void VisitAdd(Operator op)
//        {
//            OperatorCalculatorBase calculator;

//            OperatorCalculatorBase aCalculator = _stack.Pop();
//            OperatorCalculatorBase bCalculator = _stack.Pop();

//            aCalculator = aCalculator ?? new Zero_OperatorCalculator();
//            bCalculator = bCalculator ?? new Zero_OperatorCalculator();

//            double a = aCalculator.Calculate(_defaultDimensionStack);
//            double b = bCalculator.Calculate(_defaultDimensionStack);
//            bool aIsConst = aCalculator is Number_OperatorCalculator;
//            bool bIsConst = bCalculator is Number_OperatorCalculator;
//            bool aIsConstZero = aIsConst && a == 0;
//            bool bIsConstZero = bIsConst && b == 0;

//            if (aIsConstZero && bIsConstZero)
//            {
//                calculator = new Zero_OperatorCalculator();
//            }
//            else if (aIsConstZero)
//            {
//                calculator = bCalculator;
//            }
//            else if (bIsConstZero)
//            {
//                calculator = aCalculator;
//            }
//            else if (aIsConst && bIsConst)
//            {
//                calculator = new Number_OperatorCalculator(a + b);
//            }
//            else if (aIsConst)
//            {
//                if (_aggressiveInlining)
//                {
//                    calculator = CreateGenericOperatorCalculator(typeof(Add_OperatorCalculator_ConstA_VarB<>), a, bCalculator);
//                }
//                else
//                {
//                    calculator = new Add_OperatorCalculator_ConstA_VarB(a, bCalculator);
//                }
//            }
//            else if (bIsConst)
//            {
//                if (_aggressiveInlining)
//                {
//                    calculator = CreateGenericOperatorCalculator(typeof(Add_OperatorCalculator_VarA_ConstB<>), aCalculator, b);
//                }
//                else
//                {
//                    calculator = new Add_OperatorCalculator_VarA_ConstB(aCalculator, b);
//                }
//            }
//            else
//            {
//                if (_aggressiveInlining)
//                {
//                    calculator = CreateGenericOperatorCalculator(typeof(Add_OperatorCalculator_VarA_VarB<,>), aCalculator, bCalculator);
//                }
//                else
//                {
//                    calculator = new Add_OperatorCalculator_VarA_VarB(aCalculator, bCalculator);
//                }
//            }

//            _stack.Push(calculator);
//        }

//        // TODO: Make setting.
//        private bool _aggressiveInlining = true;

//        private OperatorCalculatorBase CreateGenericOperatorCalculator(Type openGenericType, params object[] constructorArgumentsIncludingChildCalculators)
//        {
//            if (openGenericType.IsGenericTypeDefinition == false) throw new EqualException(() => openGenericType.IsGenericTypeDefinition, false);

//            OperatorCalculatorBase[] childCalculators = constructorArgumentsIncludingChildCalculators.OfType<OperatorCalculatorBase>().ToArray();
//            Type[] typeArguments = ReflectionHelper.TypesFromObjects(childCalculators);
//            Type closedGenericType = openGenericType.MakeGenericType(typeArguments);

//            OperatorCalculatorBase operatorCalculator = (OperatorCalculatorBase)Activator.CreateInstance(closedGenericType, constructorArgumentsIncludingChildCalculators);

//            return operatorCalculator;
//        }

//        protected override void VisitMultiply(Operator op)
//        {
//            OperatorCalculatorBase calculator;

//            OperatorCalculatorBase aCalculator = _stack.Pop();
//            OperatorCalculatorBase bCalculator = _stack.Pop();
//            OperatorCalculatorBase originCalculator = _stack.Pop();

//            aCalculator = aCalculator ?? new One_OperatorCalculator();
//            bCalculator = bCalculator ?? new One_OperatorCalculator();
//            originCalculator = originCalculator ?? new Zero_OperatorCalculator();

//            double a = aCalculator.Calculate(_defaultDimensionStack);
//            double b = bCalculator.Calculate(_defaultDimensionStack);
//            double origin = originCalculator.Calculate(_defaultDimensionStack);
//            bool aIsConst = aCalculator is Number_OperatorCalculator;
//            bool bIsConst = bCalculator is Number_OperatorCalculator;
//            bool originIsConst = originCalculator is Number_OperatorCalculator;
//            bool aIsConstZero = aIsConst && a == 0;
//            bool bIsConstZero = bIsConst && b == 0;
//            bool originIsConstZero = originIsConst && origin == 0;
//            bool aIsConstOne = aIsConst && a == 1;
//            bool bIsConstOne = bIsConst && b == 1;

//            if (aIsConstZero || bIsConstZero)
//            {
//                calculator = new Zero_OperatorCalculator();
//            }
//            else if (aIsConstOne)
//            {
//                calculator = bCalculator;
//            }
//            else if (bIsConstOne)
//            {
//                calculator = aCalculator;
//            }
//            else if (originIsConstZero && aIsConst && bIsConst)
//            {
//                calculator = new Number_OperatorCalculator(a * b);
//            }
//            else if (aIsConst && bIsConst && originIsConst)
//            {
//                double value = (a - origin) * b + origin;
//                calculator = new Number_OperatorCalculator(value);
//            }
//            else if (aIsConst && !bIsConst && originIsConstZero)
//            {
//                if (_aggressiveInlining)
//                {
//                    calculator = CreateGenericOperatorCalculator(typeof(Multiply_OperatorCalculator_ConstA_VarB_NoOrigin<>), a, bCalculator);
//                }
//                else
//                {
//                    calculator = new Multiply_OperatorCalculator_ConstA_VarB_NoOrigin(a, bCalculator);
//                }
//            }
//            else if (!aIsConst && bIsConst && originIsConstZero)
//            {
//                if (_aggressiveInlining)
//                {
//                    calculator = CreateGenericOperatorCalculator(typeof(Multiply_OperatorCalculator_VarA_ConstB_NoOrigin<>), aCalculator, b);
//                }
//                else
//                {
//                    calculator = new Multiply_OperatorCalculator_VarA_ConstB_NoOrigin(aCalculator, b);
//                }
//            }
//            else if (!aIsConst && !bIsConst && originIsConstZero)
//            {
//                if (_aggressiveInlining)
//                {
//                    calculator = CreateGenericOperatorCalculator(typeof(Multiply_OperatorCalculator_VarA_VarB_NoOrigin<,>), aCalculator, bCalculator);
//                }
//                else
//                {
//                    calculator = new Multiply_OperatorCalculator_VarA_VarB_NoOrigin(aCalculator, bCalculator);
//                }
//            }
//            else if (aIsConst && !bIsConst && originIsConst)
//            {
//                if (_aggressiveInlining)
//                {
//                    calculator = CreateGenericOperatorCalculator(typeof(Multiply_OperatorCalculator_ConstA_VarB_ConstOrigin<>), a, bCalculator, origin);
//                }
//                else
//                {
//                    calculator = new Multiply_OperatorCalculator_ConstA_VarB_ConstOrigin(a, bCalculator, origin);
//                }
//            }
//            else if (!aIsConst && bIsConst && originIsConst)
//            {
//                if (_aggressiveInlining)
//                {
//                    calculator = CreateGenericOperatorCalculator(typeof(Multiply_OperatorCalculator_VarA_ConstB_ConstOrigin<>), aCalculator, b, origin);
//                }
//                else
//                {
//                    calculator = new Multiply_OperatorCalculator_VarA_ConstB_ConstOrigin(aCalculator, b, origin);
//                }
//            }
//            else if (!aIsConst && !bIsConst && originIsConst)
//            {
//                if (_aggressiveInlining)
//                {
//                    calculator = CreateGenericOperatorCalculator(typeof(Multiply_OperatorCalculator_VarA_VarB_ConstOrigin<,>), aCalculator, bCalculator, origin);
//                }
//                else
//                {
//                    calculator = new Multiply_OperatorCalculator_VarA_VarB_ConstOrigin(aCalculator, bCalculator, origin);
//                }
//            }
//            else if (aIsConst && bIsConst && !originIsConst)
//            {
//                if (_aggressiveInlining)
//                {
//                    calculator = CreateGenericOperatorCalculator(typeof(Multiply_OperatorCalculator_ConstA_ConstB_VarOrigin<>), a, b, originCalculator);
//                }
//                else
//                {
//                    calculator = new Multiply_OperatorCalculator_ConstA_ConstB_VarOrigin(a, b, originCalculator);
//                }
//            }
//            else if (aIsConst && !bIsConst && !originIsConst)
//            {
//                if (_aggressiveInlining)
//                {
//                    calculator = CreateGenericOperatorCalculator(typeof(Multiply_OperatorCalculator_ConstA_VarB_VarOrigin<,>), a, bCalculator, originCalculator);
//                }
//                else
//                {
//                    calculator = new Multiply_OperatorCalculator_ConstA_VarB_VarOrigin(a, bCalculator, originCalculator);
//                }
//            }
//            else if (!aIsConst && bIsConst && !originIsConst)
//            {
//                if (_aggressiveInlining)
//                {
//                    calculator = CreateGenericOperatorCalculator(typeof(Multiply_OperatorCalculator_VarA_ConstB_VarOrigin<,>), aCalculator, b, originCalculator);
//                }
//                else
//                {
//                    calculator = new Multiply_OperatorCalculator_VarA_ConstB_VarOrigin(aCalculator, b, originCalculator);
//                }
//            }
//            else
//            {
//                if (_aggressiveInlining)
//                {
//                    calculator = CreateGenericOperatorCalculator(typeof(Multiply_OperatorCalculator_VarA_VarB_VarOrigin<,,>), aCalculator, bCalculator, originCalculator);
//                }
//                else
//                {
//                    calculator = new Multiply_OperatorCalculator_VarA_VarB_VarOrigin(aCalculator, bCalculator, originCalculator);
//                }
//            }

//            _stack.Push(calculator);
//        }

//        //private OperatorCalculatorBase CreateGenericOperatorCalculator(Type openGenericType, params OperatorCalculatorBase[] childCalculators)
//        //{
//        //    return CreateGenericOperatorCalculator(openGenericType, childCalculators, new object[0]);
//        //}

//        //private OperatorCalculatorBase CreateGenericOperatorCalculator(Type openGenericType, OperatorCalculatorBase[] childCalculators, params object[] otherArguments)
//        //{
//        //    Type[] typeArguments = ReflectionHelper.TypesFromObjects(childCalculators);
//        //    Type closedGenericType = openGenericType.MakeGenericType(typeArguments);

//        //    object[] arguments = Enumerable.Concat(childCalculators, otherArguments).ToArray();

//        //    OperatorCalculatorBase operatorCalculator = (OperatorCalculatorBase)Activator.CreateInstance(closedGenericType, arguments);

//        //    return operatorCalculator;
//        //}

//        // TODO: Remove outcommented code.
//        //private OperatorCalculatorBase CreateOperatorCalculator(
//        //    Type nonGenericType, 
//        //    Type openGenericType, 
//        //    OperatorCalculatorBase[] childCalculators, 
//        //    params object[] otherArguments)
//        //{
//        //    Type type = nonGenericType;

//        //    if (_aggressiveInlining)
//        //    {
//        //        Type[] typeArguments = ReflectionHelper.TypesFromObjects(childCalculators);
//        //        Type closedGenericType = openGenericType.MakeGenericType(typeArguments);
//        //        type = closedGenericType;
//        //    }

//        //    object[] arguments = Enumerable.Concat(childCalculators, otherArguments).ToArray();

//        //    OperatorCalculatorBase operatorCalculator = (OperatorCalculatorBase)Activator.CreateInstance(type, arguments);

//        //    return operatorCalculator;
//        //}

//    }
//}
