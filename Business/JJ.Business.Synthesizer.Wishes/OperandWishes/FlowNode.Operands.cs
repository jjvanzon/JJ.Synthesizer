using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Wishes.OperandWishes;

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    // Operands in FlowNode

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._operand"/>
        public FlowNode A 
        { 
            get => _[_underlyingOutlet.A()]; 
            set => _underlyingOutlet.SetA(value); 
        }
        
        /// <inheritdoc cref="docs._operand"/>
        public FlowNode B
        {
            get => _[_underlyingOutlet.B()];
            set => _underlyingOutlet.SetB(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public FlowNode Frequency
        {
            get => _[_underlyingOutlet.Frequency()];
            set => _underlyingOutlet.SetFrequency(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public bool SupportsFrequency => _underlyingOutlet.FrequencyIsSupported();

        /// <inheritdoc cref="docs._operand"/>
        public FlowNode Pitch
        {
            get => _[_underlyingOutlet.Pitch()];
            set => _underlyingOutlet.SetPitch(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public bool SupportsPitch => _underlyingOutlet.PitchIsSupported();

        /// <inheritdoc cref="docs._operand"/>
        public FlowNode Signal
        {
            get => _[_underlyingOutlet.Signal()];
            set => _underlyingOutlet.SetSignal(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public bool SupportsSignal => _underlyingOutlet.SignalIsSupported();

        /// <inheritdoc cref="docs._operand"/>
        public FlowNode Base
        {
            get => _[_underlyingOutlet.Base()];
            set => _underlyingOutlet.SetBase(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public bool SupportsBase => _underlyingOutlet.BaseIsSupported();

        /// <inheritdoc cref="docs._operand"/>
        public FlowNode Exponent
        {
            get => _[_underlyingOutlet.Exponent()];
            set => _underlyingOutlet.SetExponent(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public bool SupportsExponent => _underlyingOutlet.ExponentIsSupported();

        /// <inheritdoc cref="docs._operand"/>
        public FlowNode TimeDifference
        {
            get => _[_underlyingOutlet.TimeDifference()];
            set => _underlyingOutlet.SetTimeDifference(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public bool SupportsTimeDifference => _underlyingOutlet.TimeDifferenceIsSupported();

        /// <inheritdoc cref="docs._operand"/>
        public FlowNode TimeScale
        {
            get => _[_underlyingOutlet.TimeScale()];
            set => _underlyingOutlet.SetTimeScale(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public bool SupportsTimeScale => _underlyingOutlet.TimeScaleIsSupported();

        /// <inheritdoc cref="docs._operand"/>
        public FlowNode SpeedFactor
        {
            get => _[_underlyingOutlet.SpeedFactor()];
            set => _underlyingOutlet.SetSpeedFactor(value);
        }

        /// <inheritdoc cref="docs._operand"/>
        public bool SupportsSpeedFactor => _underlyingOutlet.SpeedFactorIsSupported();
    
        // Origin (Obsolete)
        
        [Obsolete("Rarely used because default origin 0 usually works. " +
          "Otherwise consider use separate operators like Shift and Stretch instead.")]
        public FlowNode Origin => _[_underlyingOutlet.Operator?.Inlets.ElementAtOrDefault(2)?.Input];

    }
}
