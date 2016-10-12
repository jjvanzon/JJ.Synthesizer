using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithInheritance
{
    internal class Shift_OperatorCalculator_VarSignal_VarDifference : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _distanceCalculator;
        private readonly DimensionStack _dimensionStack;

        public Shift_OperatorCalculator_VarSignal_VarDifference(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase distanceCalculator,
            DimensionStack dimensionStack)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (distanceCalculator == null) throw new NullException(() => distanceCalculator);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _distanceCalculator = distanceCalculator;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

            _dimensionStack.Push(transformedPosition);

            double result = _signalCalculator.Calculate();

            _dimensionStack.Pop();

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
            double position = _dimensionStack.Get();

            double distance = _distanceCalculator.Calculate();

            // IMPORTANT: To shift to the right in the output, you have shift to the left in the input.
            double transformedPosition = position - distance;

            return transformedPosition;
        }
    }
}
