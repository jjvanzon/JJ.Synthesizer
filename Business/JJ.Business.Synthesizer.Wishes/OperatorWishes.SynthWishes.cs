using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable AssignmentInsteadOfDiscard

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes : OperatorFactory
    {
        private void InitializeOperatorWishes()
            => _ = new ValueIndexer(this);

        public ChannelEnum Channel { get; set; }

        // Redefine extensions methods here, or they don't show up in case of inheritance.

        /// <inheritdoc cref="docs._add"/>
        public new Outlet Add(Outlet operandA, Outlet operandB)
            => OperatorExtensionsWishes.Add(this, operandA, operandB);

        /// <inheritdoc cref="docs._multiply"/>
        public new Outlet Multiply(Outlet operandA, Outlet operandB, Outlet origin)
            => OperatorExtensionsWishes.Multiply(this, operandA, operandB, origin);

        /// <inheritdoc cref="docs._sum"/>
        public Outlet Sum(params Outlet[] operands) 
            => OperatorExtensionsWishes.Sum(this, operands);
        
        /// <inheritdoc cref="docs._sum"/>
        public Outlet Sum(IList<Outlet> operands) 
            => OperatorExtensionsWishes.Sum(this, operands);
        
        [Obsolete("Use Sum instead.")]
        public new Adder Adder(params Outlet[] operands) 
            => base.Adder(operands);

        [Obsolete("Use Sum instead.")]
        public new Adder Adder(IList<Outlet> operands) 
            => base.Adder(operands);

        /// <inheritdoc cref="docs._default" />
        public Outlet Stretch(Outlet signal, Outlet timeFactor)
            => OperatorExtensionsWishes.Stretch(this, signal, timeFactor);

        /// <inheritdoc cref="docs._sine" />
        public Outlet Sine(Outlet pitch) 
            => OperatorExtensionsWishes.Sine(this, pitch);

        /// <inheritdoc cref="docs._default" />
        public Outlet StrikeNote(
            Outlet sound, Outlet delay = null, Outlet volume = null)
            => OperatorExtensionsWishes.StrikeNote(this, sound, delay, volume);

        /// <inheritdoc cref="docs._vibrato" />
        public Outlet VibratoOverPitch(
            Outlet freq, (Outlet speed, Outlet depth) vibrato = default)
            => OperatorExtensionsWishes.VibratoOverPitch(this, freq, vibrato);

        /// <inheritdoc cref="docs._tremolo" />
        public Outlet Tremolo(
            Outlet sound, (Outlet speed, Outlet depth) tremolo = default)
            => OperatorExtensionsWishes.Tremolo(this, sound, tremolo);

        /// <inheritdoc cref="docs._panning" />
        public Outlet Panning(
            Outlet sound, Outlet panning) 
            => this.Panning(sound, panning, Channel);
        
        /// <inheritdoc cref="docs._panbrello" />
        public Outlet Panbrello(
            Outlet sound, (Outlet speed, Outlet depth) panbrello = default)
            => this.Panbrello(sound, panbrello, Channel);

        /// <inheritdoc cref="docs._pitchpan" />
        public Outlet PitchPan(
            Outlet actualFrequency, Outlet centerFrequency,
            Outlet referenceFrequency, Outlet referencePanning)
            => OperatorExtensionsWishes.PitchPan(
                this, actualFrequency, centerFrequency, referenceFrequency, referencePanning);

        /// <inheritdoc cref="docs._pitchpan" />
        public double PitchPan(
            double actualFrequency, double centerFrequency,
            double referenceFrequency, double referencePanning)
            => OperatorExtensionsWishes.PitchPan(
                this, actualFrequency, centerFrequency, referenceFrequency, referencePanning);

        public Outlet Echo(Outlet signal, Outlet magnitude = default, Outlet delay = default, int count = 16)
            => OperatorExtensionsWishes.Echo(this, signal, magnitude, delay, count);

        public Outlet EchoAdditive(Outlet signal, Outlet magnitude = default, Outlet delay = default, int count = 16)
            => OperatorExtensionsWishes.EchoAdditive(this, signal, magnitude, delay, count);

        public Outlet EchoFeedBack(Outlet signal, Outlet magnitude = default, Outlet delay = default, int count = 16)
            => OperatorExtensionsWishes.EchoFeedBack(this, signal, magnitude, delay, count);

        // ValueIndexer

        /// <inheritdoc cref="docs._valueindexer" />
        public ValueIndexer _;

        /// <inheritdoc cref="docs._valueindexer" />
        public class ValueIndexer
        {
            private readonly OperatorFactory _parent;

            /// <inheritdoc cref="docs._valueindexer" />
            internal ValueIndexer(OperatorFactory parent)
            {
                _parent = parent;
            }

            /// <inheritdoc cref="docs._valueindexer" />
            public ValueOperatorWrapper this[double value] => _parent.Value(value);
        }
 
     
        public double Calculate(Outlet outlet, double time)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return outlet.Calculate(time, ChannelIndex);
        }

        public int ChannelIndex
        {
            get
            {
                switch (Channel)
                {
                    case ChannelEnum.Single: return 0;
                    case ChannelEnum.Left:   return 0;
                    case ChannelEnum.Right:  return 1;
                    
                    default: throw new InvalidValueException(Channel);
                }
            }
        }   
    }
}