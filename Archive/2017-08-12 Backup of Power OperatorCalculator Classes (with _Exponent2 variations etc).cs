//using System;
//using System.Runtime.CompilerServices;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Power_OperatorCalculator_VarBase_VarExponent : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _baseCalculator;
//        private readonly OperatorCalculatorBase _exponentCalculator;

//        public Power_OperatorCalculator_VarBase_VarExponent(OperatorCalculatorBase baseCalculator, OperatorCalculatorBase exponentCalculator)
//            : base(new[] { baseCalculator, exponentCalculator })
//        {
//            OperatorCalculatorHelper.AssertChildOperatorCalculator(baseCalculator, () => baseCalculator);
//            OperatorCalculatorHelper.AssertChildOperatorCalculator(exponentCalculator, () => exponentCalculator);

//            _baseCalculator = baseCalculator;
//            _exponentCalculator = exponentCalculator;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double @base = _baseCalculator.Calculate();
//            double exponent = _exponentCalculator.Calculate();
//            return Math.Pow(@base, exponent);
//        }
//    }

//    internal class Power_OperatorCalculator_ConstBase_VarExponent : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly double _baseValue;
//        private readonly OperatorCalculatorBase _exponentCalculator;

//        public Power_OperatorCalculator_ConstBase_VarExponent(double baseValue, OperatorCalculatorBase exponentCalculator)
//            : base(new[] { exponentCalculator })
//        {
//            OperatorCalculatorHelper.AssertChildOperatorCalculator(exponentCalculator, () => exponentCalculator);

//            _baseValue = baseValue;
//            _exponentCalculator = exponentCalculator;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double exponent = _exponentCalculator.Calculate();
//            return Math.Pow(_baseValue, exponent);
//        }
//    }

//    internal class Power_OperatorCalculator_VarBase_ConstExponent : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _baseCalculator;
//        private readonly double _exponent;

//        public Power_OperatorCalculator_VarBase_ConstExponent(OperatorCalculatorBase baseCalculator, double exponent)
//            : base(new[] { baseCalculator })
//        {
//            OperatorCalculatorHelper.AssertChildOperatorCalculator(baseCalculator, () => baseCalculator);

//            _baseCalculator = baseCalculator;
//            _exponent = exponent;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double @base = _baseCalculator.Calculate();
//            return Math.Pow(@base, _exponent);
//        }
//    }

//    internal class Power_OperatorCalculator_VarBase_Exponent2 : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _baseCalculator;

//        public Power_OperatorCalculator_VarBase_Exponent2(OperatorCalculatorBase baseCalculator)
//            : base(new[] { baseCalculator })
//        {
//            OperatorCalculatorHelper.AssertChildOperatorCalculator(baseCalculator, () => baseCalculator);

//            _baseCalculator = baseCalculator;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double @base = _baseCalculator.Calculate();

//            return @base * @base;
//        }
//    }

//    internal class Power_OperatorCalculator_VarBase_Exponent3 : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _baseCalculator;

//        public Power_OperatorCalculator_VarBase_Exponent3(OperatorCalculatorBase baseCalculator)
//            : base(new[] { baseCalculator })
//        {
//            OperatorCalculatorHelper.AssertChildOperatorCalculator(baseCalculator, () => baseCalculator);

//            _baseCalculator = baseCalculator;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double @base = _baseCalculator.Calculate();

//            return @base * @base * @base;
//        }
//    }

//    internal class Power_OperatorCalculator_VarBase_Exponent4 : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _baseCalculator;

//        public Power_OperatorCalculator_VarBase_Exponent4(OperatorCalculatorBase baseCalculator)
//            : base(new[] { baseCalculator })
//        {
//            OperatorCalculatorHelper.AssertChildOperatorCalculator(baseCalculator, () => baseCalculator);

//            _baseCalculator = baseCalculator;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double @base = _baseCalculator.Calculate();

//            double powerOf2 = @base * @base;
//            double powerOf4 = powerOf2 * powerOf2;
//            return powerOf4;
//        }
//    }
//}
