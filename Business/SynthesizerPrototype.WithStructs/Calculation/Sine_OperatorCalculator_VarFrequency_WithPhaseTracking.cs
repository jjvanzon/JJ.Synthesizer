using System.Diagnostics;
using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.WithStructs.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Business.SynthesizerPrototype.WithStructs.Helpers;

namespace JJ.Business.SynthesizerPrototype.WithStructs.Calculation
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public struct Sine_OperatorCalculator_VarFrequency_WithPhaseTracking<TFrequencyCalculator>
        : ISine_OperatorCalculator_VarFrequency
        where TFrequencyCalculator : IOperatorCalculator
    {
        private TFrequencyCalculator _frequencyCalculator;
        public IOperatorCalculator FrequencyCalculator
        {
            get => _frequencyCalculator;
            set => _frequencyCalculator = (TFrequencyCalculator)value;
        }

        public DimensionStack DimensionStack { get; set; }

        private double _phase;
        private double _previousPosition;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            double position = DimensionStack.Get();

            double frequency = _frequencyCalculator.Calculate();

            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * frequency;
            double value = SineCalculator.Sin(_phase);

            _previousPosition = position;

            return value;
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}