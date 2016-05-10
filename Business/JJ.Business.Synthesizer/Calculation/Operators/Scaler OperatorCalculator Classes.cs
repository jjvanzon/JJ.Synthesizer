using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Scaler_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _sourceValueACalculator;
        private readonly OperatorCalculatorBase _sourceValueBCalculator;
        private readonly OperatorCalculatorBase _targetValueACalculator;
        private readonly OperatorCalculatorBase _targetValueBCalculator;

        public Scaler_OperatorCalculator(
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
        public override double Calculate(DimensionStack dimensionStack)
        {
            double signal = _signalCalculator.Calculate(dimensionStack);
            double sourceValueA = _sourceValueACalculator.Calculate(dimensionStack);
            double sourceValueB = _sourceValueBCalculator.Calculate(dimensionStack);
            double targetValueA = _targetValueACalculator.Calculate(dimensionStack);
            double targetValueB = _targetValueBCalculator.Calculate(dimensionStack);

            double sourceRange = sourceValueB - sourceValueA;
            double targetRange = targetValueB - targetValueA;
            double between0And1 = (signal - sourceValueA) / sourceRange;
            double result = between0And1 * targetRange + targetValueA;

            return result;
        }
    }
}