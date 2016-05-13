using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Business.Synthesizer.Enums;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class TimePower_WithOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _exponentCalculator;
        private readonly OperatorCalculatorBase _originCalculator;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        public TimePower_WithOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase exponentCalculator,
            OperatorCalculatorBase originCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator, exponentCalculator, originCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (exponentCalculator == null) throw new NullException(() => exponentCalculator);
            if (originCalculator == null) throw new NullException(() => originCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _exponentCalculator = exponentCalculator;
            _originCalculator = originCalculator;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            // IMPORTANT: 

            // To increase time in the output, you have to decrease time of the input. 
            // That is why the reciprocal of the exponent is used.

            // Furthermore, you can not use a fractional exponent on a negative number.
            // Time can be negative, that is why the sign is taken off the time 
            // before taking the power and then added to it again after taking the power.

            double origin = _originCalculator.Calculate();

            double positionAbs = Math.Abs(position - origin);

            double exponent = _exponentCalculator.Calculate();

            double transformedPosition = Math.Pow(positionAbs, 1 / exponent) + origin;

            // TODO: Not debugged yet.
            int positionSign = Math.Sign(position - origin);
            if (positionSign == -1)
            {
                transformedPosition = -transformedPosition;
            }

            _dimensionStack.Push(_dimensionIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            _dimensionStack.Pop(_dimensionIndex);

            return result;
        }
    }

    internal class TimePower_WithoutOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _exponentCalculator;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        public TimePower_WithoutOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase exponentCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator, exponentCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (exponentCalculator == null) throw new NullException(() => exponentCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _exponentCalculator = exponentCalculator;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            // IMPORTANT: 

            // To increase time in the output, you have to decrease time of the input. 
            // That is why the reciprocal of the exponent is used.

            // Furthermore, you can not use a fractional exponent on a negative number.
            // Time can be negative, that is why the sign is taken off the time 
            // before taking the power and then added to it again after taking the power.

            // (time: -4, exponent: 2) => -1 * Pow(4, 1/2)
            double positionAbs = Math.Abs(position);

            double exponent = _exponentCalculator.Calculate();

            double transformedPosition = Math.Pow(positionAbs, 1 / exponent);

            // TODO: Not debugged yet.
            int positionSign = Math.Sign(position);
            if (positionSign == -1)
            {
                transformedPosition = -transformedPosition;
            }

            _dimensionStack.Push(_dimensionIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            _dimensionStack.Pop(_dimensionIndex);

            return result;
        }
    }
}
