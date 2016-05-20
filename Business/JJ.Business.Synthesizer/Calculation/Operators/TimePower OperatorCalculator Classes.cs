using JJ.Framework.Reflection.Exceptions;
using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class TimePower_OperatorCalculator_WithOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _exponentCalculator;
        private readonly OperatorCalculatorBase _originCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public TimePower_OperatorCalculator_WithOrigin(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase exponentCalculator,
            OperatorCalculatorBase originCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator, exponentCalculator, originCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (exponentCalculator == null) throw new NullException(() => exponentCalculator);
            if (originCalculator == null) throw new NullException(() => originCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _exponentCalculator = exponentCalculator;
            _originCalculator = originCalculator;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            return result;
        }

        public override void Reset()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            base.Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);
            double origin = _originCalculator.Calculate();

            // IMPORTANT: 

            // To increase time in the output, you have to decrease time of the input. 
            // That is why the reciprocal of the exponent is used.

            // Furthermore, you can not use a fractional exponent on a negative number.
            // Time can be negative, that is why the sign is taken off the time 
            // before taking the power and then added to it again after taking the power.

            double positionAbs = Math.Abs(position - origin);

            double exponent = _exponentCalculator.Calculate();

            double transformedPosition = Math.Pow(positionAbs, 1 / exponent) + origin;

            // TODO: Not debugged yet.
            int positionSign = Math.Sign(position - origin);
            if (positionSign == -1)
            {
                transformedPosition = -transformedPosition;
            }

            return transformedPosition;
        }
    }

    internal class TimePower_OperatorCalculator_NoOrigin : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _exponentCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public TimePower_OperatorCalculator_NoOrigin(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase exponentCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator, exponentCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (exponentCalculator == null) throw new NullException(() => exponentCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _exponentCalculator = exponentCalculator;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            return result;
        }

        public override void Reset()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            base.Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

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

            return transformedPosition;
        }
    }
}
