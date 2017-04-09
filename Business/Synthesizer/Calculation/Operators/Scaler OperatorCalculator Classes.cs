using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Scaler_OperatorCalculator_AllVars : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _sourceValueACalculator;
        private readonly OperatorCalculatorBase _sourceValueBCalculator;
        private readonly OperatorCalculatorBase _targetValueACalculator;
        private readonly OperatorCalculatorBase _targetValueBCalculator;

        public Scaler_OperatorCalculator_AllVars(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase sourceValueACalculator,
            OperatorCalculatorBase sourceValueBCalculator,
            OperatorCalculatorBase targetValueACalculator,
            OperatorCalculatorBase targetValueBCalculator)
            : base(new[] {
                signalCalculator,
                sourceValueACalculator,
                sourceValueBCalculator,
                targetValueACalculator,
                targetValueBCalculator })
        {
            _signalCalculator = signalCalculator ?? throw new NullException(() => signalCalculator);
            _sourceValueACalculator = sourceValueACalculator ?? throw new NullException(() => sourceValueACalculator);
            _sourceValueBCalculator = sourceValueBCalculator ?? throw new NullException(() => sourceValueBCalculator);
            _targetValueACalculator = targetValueACalculator ?? throw new NullException(() => targetValueACalculator);
            _targetValueBCalculator = targetValueBCalculator ?? throw new NullException(() => targetValueBCalculator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double signal = _signalCalculator.Calculate();
            double sourceValueA = _sourceValueACalculator.Calculate();
            double sourceValueB = _sourceValueBCalculator.Calculate();
            double targetValueA = _targetValueACalculator.Calculate();
            double targetValueB = _targetValueBCalculator.Calculate();

            double result = MathHelper.ScaleLinearly(signal, sourceValueA, sourceValueB, targetValueA, targetValueB);

            return result;
        }
    }

    internal class Scaler_OperatorCalculator_ManyConsts : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _sourceValueA;
        private readonly double _targetValueA;
        private readonly double _slope;

        public Scaler_OperatorCalculator_ManyConsts(
            OperatorCalculatorBase signalCalculator,
            double sourceValueA,
            double sourceValueB,
            double targetValueA,
            double targetValueB)
            : base(new[] { signalCalculator })
        {
            _signalCalculator = signalCalculator ?? throw new NullException(() => signalCalculator);
            _sourceValueA = sourceValueA;
            _targetValueA = targetValueA;

            double sourceRange = sourceValueB - _sourceValueA;
            double targetRange = targetValueB - _targetValueA;
            _slope = targetRange / sourceRange;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double signal = _signalCalculator.Calculate();

            double result = MathHelper.ScaleLinearly(signal, _sourceValueA, _targetValueA, _slope);

            return result;
        }
    }
}