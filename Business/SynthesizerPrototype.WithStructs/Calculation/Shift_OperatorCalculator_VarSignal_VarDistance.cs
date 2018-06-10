using System.Diagnostics;
using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.WithStructs.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Business.SynthesizerPrototype.WithStructs.Helpers;

namespace JJ.Business.SynthesizerPrototype.WithStructs.Calculation
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public struct Shift_OperatorCalculator_VarSignal_VarDistance<TSignalCalculator, TDistanceCalculator>
        : IShift_OperatorCalculator_VarSignal_VarDistance
        where TSignalCalculator : IOperatorCalculator
        where TDistanceCalculator : IOperatorCalculator
    {
        private TSignalCalculator _signalCalculator;
        public IOperatorCalculator SignalCalculator
        {
            get => _signalCalculator;
            set => _signalCalculator = (TSignalCalculator)value;
        }

        private TDistanceCalculator _distanceCalculator;
        public IOperatorCalculator DistanceCalculator
        {
            get => _distanceCalculator;
            set => _distanceCalculator = (TDistanceCalculator)value;
        }

        public DimensionStack DimensionStack { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            double transformedPosition = GetTransformedPosition();

            DimensionStack.Push(transformedPosition);

            double result = _signalCalculator.Calculate();

            DimensionStack.Pop();

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double GetTransformedPosition()
        {
            double position = DimensionStack.Get();

            double distance = _distanceCalculator.Calculate();

            // IMPORTANT: To shift to the right in the output, you have shift to the left in the input.
            double transformedPosition = position - distance;

            return transformedPosition;
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}