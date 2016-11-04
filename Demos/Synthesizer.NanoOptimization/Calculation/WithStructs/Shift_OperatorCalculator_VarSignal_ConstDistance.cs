using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.WithStructs
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal struct Shift_OperatorCalculator_VarSignal_ConstDistance<TSignalCalculator> 
        : IShift_OperatorCalculator_VarSignal_ConstDistance
        where TSignalCalculator : IOperatorCalculator
    {
        private TSignalCalculator _signalCalculator;
        public IOperatorCalculator SignalCalculator
        {
            get { return _signalCalculator; }
            set { _signalCalculator = (TSignalCalculator)value; }
        }

        private double _distance;
        public double Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        private DimensionStack _dimensionStack;
        public DimensionStack DimensionStack
        {
            get { return _dimensionStack; }
            set { _dimensionStack = value; }
        }

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

            // IMPORTANT: To shift to the right in the output, you have shift to the left in the input.
            double transformedPosition = position - _distance;

            return transformedPosition;
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
