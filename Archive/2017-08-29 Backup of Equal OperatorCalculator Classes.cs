//using System;
//using System.Linq.Expressions;
//using System.Runtime.CompilerServices;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Equal_OperatorCalculator_VarA_VarB : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _aCalculator;
//        private readonly OperatorCalculatorBase _bCalculator;
        
//        public Equal_OperatorCalculator_VarA_VarB(
//            OperatorCalculatorBase aCalculator,
//            OperatorCalculatorBase bCalculator)
//            : base(new[] { aCalculator, bCalculator })
//        {
//            _aCalculator = aCalculator ?? throw new NullException(() => aCalculator);
//            _bCalculator = bCalculator ?? throw new NullException(() => bCalculator);
//        }
         
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double a = _aCalculator.Calculate();
//            double b = _bCalculator.Calculate();

//            if (a == b) return 1.0;
//            else return 0.0;
//        }
//    }

//    internal class Equal_OperatorCalculator_VarA_ConstB : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _aCalculator;
//        private readonly double _b;

//        public Equal_OperatorCalculator_VarA_ConstB(OperatorCalculatorBase aCalculator, double b)
//            : base(new[] { aCalculator })
//        {
//            _aCalculator = aCalculator ?? throw new NullException(() => aCalculator);
//            _b = b;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double a = _aCalculator.Calculate();

//            if (a == _b) return 1.0;
//            else return 0.0;
//        }
//    }
//}