//using System.Runtime.CompilerServices;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Divide_OperatorCalculator_VarA_ConstB : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _aCalculator;
//        private readonly double _b;

//        public Divide_OperatorCalculator_VarA_ConstB(
//            OperatorCalculatorBase aCalculator,
//            double b)
//            : base(new[] { aCalculator })
//        {
//            _aCalculator = aCalculator ?? throw new NullException(() => aCalculator);
//            _b = b;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double a = _aCalculator.Calculate();
//            return a / _b;
//        }
//    }

//    internal class Divide_OperatorCalculator_ConstA_VarB : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly double _a;
//        private readonly OperatorCalculatorBase _bCalculator;

//        public Divide_OperatorCalculator_ConstA_VarB(
//            double a,
//            OperatorCalculatorBase bCalculator)
//            : base(new[] { bCalculator })
//        {
//            _a = a;
//            _bCalculator = bCalculator ?? throw new NullException(() => bCalculator);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double b = _bCalculator.Calculate();
//            return _a / b;
//        }
//    }

//    internal class Divide_OperatorCalculator_VarA_VarB : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _aCalculator;
//        private readonly OperatorCalculatorBase _bCalculator;

//        public Divide_OperatorCalculator_VarA_VarB(
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

//            return a / b;
//        }
//    }

//    internal class Divide_OperatorCalculator_ConstA_ConstB : OperatorCalculatorBase
//    {
//        private readonly double _a;
//        private readonly double _b;

//        public Divide_OperatorCalculator_ConstA_ConstB(
//            double a,
//            double b)
//        {
//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            if (b == 0) throw new ZeroException(() => b);

//            _a = a;
//            _b = b;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            return _a / _b;
//        }
//    }
//}
