//using System;
//using System.Runtime.CompilerServices;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Add_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase[] _operandCalculators;

//        public Add_OperatorCalculator(OperatorCalculatorBase[] operandCalculators)
//            : base(operandCalculators)
//        {
//            if (operandCalculators == null) throw new NullException(() => operandCalculators);

//            _operandCalculators = operandCalculators;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double result = 0;

//            for (int i = 0; i < _operandCalculators.Length; i++)
//            {
//                double result2 = _operandCalculators[i].Calculate();

//                result += result2;
//            }

//            return result;
//        }
//    }
//}
