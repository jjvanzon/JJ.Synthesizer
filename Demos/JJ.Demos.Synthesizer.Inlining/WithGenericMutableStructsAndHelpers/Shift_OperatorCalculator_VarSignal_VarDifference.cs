using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.Inlining.Shared;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.WithGenericMutableStructsAndHelpers
{
    internal class Shift_OperatorCalculator_VarSignal_VarDifference<TSignalCalculator, TDistanceCalculator> : IOperatorCalculator
        where TSignalCalculator : IOperatorCalculator
        where TDistanceCalculator : IOperatorCalculator
    {
        public TSignalCalculator _signalCalculator;
        public TDistanceCalculator _distanceCalculator;
        public DimensionStack _dimensionStack;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
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
