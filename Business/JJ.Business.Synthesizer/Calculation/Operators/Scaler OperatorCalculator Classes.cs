using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Scaler_OperatorCalculator_AllVariables : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _sourceValueACalculator;
        private readonly OperatorCalculatorBase _sourceValueBCalculator;
        private readonly OperatorCalculatorBase _targetValueACalculator;
        private readonly OperatorCalculatorBase _targetValueBCalculator;

        public Scaler_OperatorCalculator_AllVariables(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase sourceValueACalculator,
            OperatorCalculatorBase sourceValueBCalculator,
            OperatorCalculatorBase targetValueACalculator,
            OperatorCalculatorBase targetValueBCalculator)
            : base(new OperatorCalculatorBase[] {
                signalCalculator,
                sourceValueACalculator,
                sourceValueBCalculator,
                targetValueACalculator,
                targetValueBCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (sourceValueACalculator == null) throw new NullException(() => sourceValueACalculator);
            if (sourceValueBCalculator == null) throw new NullException(() => sourceValueBCalculator);
            if (targetValueACalculator == null) throw new NullException(() => targetValueACalculator);
            if (targetValueBCalculator == null) throw new NullException(() => targetValueBCalculator);

            _signalCalculator = signalCalculator;
            _sourceValueACalculator = sourceValueACalculator;
            _sourceValueBCalculator = sourceValueBCalculator;
            _targetValueACalculator = targetValueACalculator;
            _targetValueBCalculator = targetValueBCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double signal = _signalCalculator.Calculate();
            double sourceValueA = _sourceValueACalculator.Calculate();
            double sourceValueB = _sourceValueBCalculator.Calculate();
            double targetValueA = _targetValueACalculator.Calculate();
            double targetValueB = _targetValueBCalculator.Calculate();

            double sourceRange = sourceValueB - sourceValueA;
            double targetRange = targetValueB - targetValueA;
            double between0And1 = (signal - sourceValueA) / sourceRange;
            double result = between0And1 * targetRange + targetValueA;

            return result;
        }
    }

    internal class Scaler_OperatorCalculator_ManyConstants : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _sourceValueA;
        private readonly double _sourceValueB;
        private readonly double _targetValueA;
        private readonly double _targetValueB;
        private readonly double _slope;

        public Scaler_OperatorCalculator_ManyConstants(
            OperatorCalculatorBase signalCalculator,
            double sourceValueA,
            double sourceValueB,
            double targetValueA,
            double targetValueB)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);

            _signalCalculator = signalCalculator;
            _sourceValueA = sourceValueA;
            _sourceValueB = sourceValueB;
            _targetValueA = targetValueA;
            _targetValueB = targetValueB;

            double sourceRange = _sourceValueB - _sourceValueA;
            double targetRange = _targetValueB - _targetValueA;
            _slope = targetRange / sourceRange;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double signal = _signalCalculator.Calculate();

            double result = (signal - _sourceValueA) * _slope + _targetValueA;

            return result;
        }
    }
}