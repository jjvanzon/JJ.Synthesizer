﻿using JJ.Framework.Reflection.Exceptions;
using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Power_OperatorCalculator_VarBase_VarExponent : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _baseCalculator;
        private readonly OperatorCalculatorBase _exponentCalculator;

        public Power_OperatorCalculator_VarBase_VarExponent(OperatorCalculatorBase baseCalculator, OperatorCalculatorBase exponentCalculator)
            : base(new OperatorCalculatorBase[] { baseCalculator, exponentCalculator })
        {
            if (baseCalculator == null) throw new NullException(() => baseCalculator);
            if (baseCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => baseCalculator);
            if (exponentCalculator == null) throw new NullException(() => exponentCalculator);
            if (exponentCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => exponentCalculator);

            _baseCalculator = baseCalculator;
            _exponentCalculator = exponentCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double @base = _baseCalculator.Calculate();
            double exponent = _exponentCalculator.Calculate();
            return Math.Pow(@base, exponent);
        }
    }

    internal class Power_OperatorCalculator_ConstBase_VarExponent : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _baseValue;
        private readonly OperatorCalculatorBase _exponentCalculator;

        public Power_OperatorCalculator_ConstBase_VarExponent(double baseValue, OperatorCalculatorBase exponentCalculator)
            : base(new OperatorCalculatorBase[] { exponentCalculator })
        {
            if (exponentCalculator == null) throw new NullException(() => exponentCalculator);
            if (exponentCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => exponentCalculator);

            _baseValue = baseValue;
            _exponentCalculator = exponentCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double exponent = _exponentCalculator.Calculate();
            return Math.Pow(_baseValue, exponent);
        }
    }

    internal class Power_OperatorCalculator_VarBase_ConstExponent : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _baseCalculator;
        private readonly double _exponentValue;

        public Power_OperatorCalculator_VarBase_ConstExponent(OperatorCalculatorBase baseCalculator, double exponentValue)
            : base(new OperatorCalculatorBase[] { baseCalculator })
        {
            if (baseCalculator == null) throw new NullException(() => baseCalculator);
            if (baseCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => baseCalculator);

            _baseCalculator = baseCalculator;
            _exponentValue = exponentValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double @base = _baseCalculator.Calculate();
            return Math.Pow(@base, _exponentValue);
        }
    }
}