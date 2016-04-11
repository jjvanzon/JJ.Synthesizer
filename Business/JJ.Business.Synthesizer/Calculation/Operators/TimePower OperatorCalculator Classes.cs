using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class TimePower_WithOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _exponentCalculator;
        private readonly OperatorCalculatorBase _originCalculator;

        public TimePower_WithOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase exponentCalculator,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator, exponentCalculator, originCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (exponentCalculator == null) throw new NullException(() => exponentCalculator);
            if (originCalculator == null) throw new NullException(() => originCalculator);

            _signalCalculator = signalCalculator;
            _exponentCalculator = exponentCalculator;
            _originCalculator = originCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            // IMPORTANT: 

            // To increase time in the output, you have to decrease time of the input. 
            // That is why the reciprocal of the exponent is used.

            // Furthermore, you can not use a fractional exponent on a negative number.
            // Time can be negative, that is why the sign is taken off the time 
            // before taking the power and then added to it again after taking the power.

            double origin = _originCalculator.Calculate(dimensionStack);

            double timeAbs = Math.Abs(time - origin);

            double exponent = _exponentCalculator.Calculate(dimensionStack);

            double transformedTime = Math.Pow(timeAbs, 1 / exponent) + origin;

            // TODO: Not debugged yet.
            int timeSign = Math.Sign(time - origin);
            if (timeSign == -1)
            {
                transformedTime = -transformedTime;
            }

            dimensionStack.Push(DimensionEnum.Time, transformedTime);
            double result = _signalCalculator.Calculate(dimensionStack);
            dimensionStack.Pop(DimensionEnum.Time);

            return result;
        }
    }

    internal class TimePower_WithoutOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _exponentCalculator;

        public TimePower_WithoutOrigin_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase exponentCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator, exponentCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (exponentCalculator == null) throw new NullException(() => exponentCalculator);

            _signalCalculator = signalCalculator;
            _exponentCalculator = exponentCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            // IMPORTANT: 

            // To increase time in the output, you have to decrease time of the input. 
            // That is why the reciprocal of the exponent is used.

            // Furthermore, you can not use a fractional exponent on a negative number.
            // Time can be negative, that is why the sign is taken off the time 
            // before taking the power and then added to it again after taking the power.

            // (time: -4, exponent: 2) => -1 * Pow(4, 1/2)
            double timeAbs = Math.Abs(time);

            double exponent = _exponentCalculator.Calculate(dimensionStack);

            double transformedTime = Math.Pow(timeAbs, 1 / exponent);

            // TODO: Not debugged yet.
            int timeSign = Math.Sign(time);
            if (timeSign == -1)
            {
                transformedTime = -transformedTime;
            }

            dimensionStack.Push(DimensionEnum.Time, transformedTime);
            double result = _signalCalculator.Calculate(dimensionStack);
            dimensionStack.Pop(DimensionEnum.Time);

            return result;
        }
    }
}
