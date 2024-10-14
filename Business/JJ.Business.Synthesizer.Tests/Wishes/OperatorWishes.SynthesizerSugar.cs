using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Persistence.Synthesizer;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable AssignmentInsteadOfDiscard

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthesizerSugar : OperatorFactory
    {
        private void InitializeOperatorWishes()
            => _ = new ValueIndexer(this);

        public ChannelEnum Channel { get; set; }

        // ReSharper disable once InconsistentNaming
        public const ChannelEnum Mono = ChannelEnum.Single;

        // Redefine extensions methods here, or they don't show up in case of inheritance.
        
        /// <inheritdoc cref="docs._default" />
        public Outlet Stretch(
            Outlet signal, Outlet timeFactor)
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

        ///// <inheritdoc cref="docs._panning" />
        //public Outlet Panning(
        //    Outlet sound, double panning) 
        //    => this.Panning(sound, panning, Channel);
        
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
    }
}