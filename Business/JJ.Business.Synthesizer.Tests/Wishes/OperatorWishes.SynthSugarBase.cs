using System.Collections.Generic;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Persistence.Synthesizer;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable AssignmentInsteadOfDiscard

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthSugarBase : OperatorFactory
    {
		private void InitializeOperatorWishes()
            => _ = new ValueIndexer(this);

        public Outlet StrikeNote(Outlet sound, Outlet delay = null, Outlet volume = null)
        {
            if (delay != null) sound = TimeAdd(sound, delay);
            if (volume != null) sound = Multiply(sound, volume);
            return sound;
        }

        /// <inheritdoc cref="docs._default" />
        public Outlet Stretch(Outlet signal, Outlet timeFactor)
            => TimeMultiply(signal, timeFactor ?? _[1]);

        /// <inheritdoc cref="docs._vibrato" />
        public Outlet VibratoOverPitch(Outlet freq, (Outlet speed, Outlet depth) vibrato = default)
        {
            vibrato.speed = vibrato.speed ?? _[5.5];
            vibrato.depth = vibrato.depth ?? _[0.0005];

            return Multiply(freq, Add(_[1], Sine(vibrato.depth, vibrato.speed)));
        }

        /// <inheritdoc cref="docs._tremolo" />
        public Outlet Tremolo(Outlet sound, (Outlet speed, Outlet depth) tremolo = default)
        {
            tremolo.speed = tremolo.speed ?? _[8];
            tremolo.depth = tremolo.depth ?? _[0.33];

            var modulator = Add(Sine(tremolo.depth, tremolo.speed), _[1]);
            
            return Multiply(sound, modulator);
        }

        public (Outlet left, Outlet right) Panning((Outlet left, Outlet right) channels, Outlet panning)
        {
            var leftPan = Multiply(channels.left, Substract(_[1], panning));
            var rightPan = Multiply(channels.right, panning);
            return (leftPan, rightPan);
        }

        public (Outlet left, Outlet right) Panbrello(
            (Outlet left, Outlet right) channels,
            (Outlet speed, Outlet depth) panbrello)
        {
            panbrello.speed = panbrello.speed ?? _[3];
            panbrello.depth = panbrello.depth ?? _[0.33];

            var modulator = Add(_[1], Sine(panbrello.depth, panbrello.speed));

            return Panning(channels, modulator);
        }

        /// <inheritdoc cref="ValueIndexer" />
        public ValueIndexer _;

        /// <summary>
        /// Shorthand for OperatorFactor.Value(123), x.Value(123) or Value(123). Allows using _[123] instead.
        /// Literal numbers need to be wrapped inside a Value Operator so they can always be substituted by
        /// a whole formula / graph / calculation / curve over time.
        /// </summary>
        /// <returns>
        /// ValueOperatorWrapper also usable as Outlet or double.
        /// </returns>
        public class ValueIndexer
        {
            private readonly OperatorFactory _parent;

            /// <inheritdoc cref="ValueIndexer"/>
            internal ValueIndexer(OperatorFactory parent)
                => _parent = parent;

            /// <inheritdoc cref="ValueIndexer"/>
            public ValueOperatorWrapper this[double value] => _parent.Value(value);
        }
    }
}