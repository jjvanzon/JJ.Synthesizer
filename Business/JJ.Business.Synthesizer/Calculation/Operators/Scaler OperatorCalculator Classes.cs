using System;
using System.Collections.Generic;
using System.Linq;
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

            // TODO: Do stricter assertions once you have specialized calculators.
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(sourceValueACalculator, () => sourceValueACalculator);
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(sourceValueBCalculator, () => sourceValueBCalculator);
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(targetValueACalculator, () => targetValueACalculator);
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(targetValueBCalculator, () => targetValueBCalculator);

            _signalCalculator = signalCalculator;
            _sourceValueACalculator = sourceValueACalculator;
            _sourceValueBCalculator = sourceValueBCalculator;
            _targetValueACalculator = targetValueACalculator;
            _targetValueBCalculator = targetValueBCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double signal = _signalCalculator.Calculate(time, channelIndex);
            double sourceValueA = _sourceValueACalculator.Calculate(time, channelIndex);
            double sourceValueB = _sourceValueBCalculator.Calculate(time, channelIndex);
            double targetValueA = _targetValueACalculator.Calculate(time, channelIndex);
            double targetValueB = _targetValueBCalculator.Calculate(time, channelIndex);

            double sourceRange = sourceValueB - sourceValueA;
            double between0And1 = (signal - sourceValueA) / sourceRange;

            double targetRange = targetValueB - targetValueA;
            double result = between0And1 * targetRange + targetValueA;

            return result;
        }
    }
}