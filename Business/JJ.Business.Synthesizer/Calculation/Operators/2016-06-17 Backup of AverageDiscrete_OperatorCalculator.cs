//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    // You could imagine many more optimized calculations, such as first operand is const and several,
//    // that omit the loop, but future optimizations will just make that work obsolete again.

//    internal class AverageDiscrete_OperatorCalculator_MoreThanTwoOperands : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase[] _operandCalculators;
//        private readonly double _count;
        
//        public AverageDiscrete_OperatorCalculator_MoreThanTwoOperands(IList<OperatorCalculatorBase> operandCalculators)
//            : base(operandCalculators)
//        {
//            if (operandCalculators == null) throw new NullException(() => operandCalculators);
//            if (operandCalculators.Count <= 2) throw new LessThanOrEqualException(() => operandCalculators.Count, 2);

//            _operandCalculators = operandCalculators.ToArray();
//            _count = _operandCalculators.Length;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double sum = 0;
//            for (int i = 0; i < _count; i++)
//            {
//                double value = _operandCalculators[i].Calculate();
//                sum += value;
//            }

//            double average = sum / _count;

//            return average;
//        }
//    }
//}
