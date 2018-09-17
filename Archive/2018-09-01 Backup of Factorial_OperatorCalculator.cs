//using System;
//using System.Runtime.CompilerServices;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Factorial_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _numberCalculator;

//        public Factorial_OperatorCalculator(OperatorCalculatorBase numberCalculator)
//            : base(new[] { numberCalculator })
//            => _numberCalculator = numberCalculator;

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double number = _numberCalculator.Calculate();
//            throw new NotImplementedException();
//        }
//    }
//}