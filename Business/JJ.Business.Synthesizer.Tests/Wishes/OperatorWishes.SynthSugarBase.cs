using System;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Enums.ChannelEnum;
using static JJ.Business.Synthesizer.Tests.Wishes.Notes;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable AssignmentInsteadOfDiscard

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthSugarBase : OperatorFactory
    {
        public const double TWO_PI = Math.PI * 2;

        private void InitializeOperatorWishes()
            => _ = new ValueIndexer(this);

        public ChannelEnum Channel { get; set; }

        // ReSharper disable once InconsistentNaming
        public const ChannelEnum Mono = ChannelEnum.Single;

        /// <inheritdoc cref="docs._default" />
        public Outlet Stretch(Outlet signal, Outlet timeFactor)
            => TimeMultiply(signal, timeFactor ?? _[1]);

        /// <inheritdoc cref="_sinedocs" />
        public Outlet Sine(Outlet pitch) => Sine(_[1], pitch);

        public Outlet StrikeNote(Outlet sound, Outlet delay = null, Outlet volume = null)
        {
            if (delay != null) sound  = TimeAdd(sound, delay);
            if (volume != null) sound = Multiply(sound, volume);
            return sound;
        }

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

        // Panning

        /// <inheritdoc cref="_panningdocs" />
        public Outlet Panning(Outlet signal, Outlet panning)
        {
            switch (Channel)
            {
                case Mono:  return signal;
                case Left:  return Multiply(signal, Substract(_[1], panning));
                case Right: return Multiply(signal, panning);
                default:    throw new ValueNotSupportedException(Channel);
            }
        }

        /// <inheritdoc cref="_panningdocs" />
        public Outlet Panning(Outlet signal, double panning)
        {
            if (panning < 0) panning = 0;
            if (panning > 1) panning = 1;

            switch (Channel)
            {
                case Mono:  return signal;
                case Left:  return Multiply(signal, _[1 - panning]);
                case Right: return Multiply(signal, _[panning]);
                default:    throw new ValueNotSupportedException(Channel);
            }
        }

        /// <inheritdoc cref="_panningdocs" />
        public (Outlet Left, Outlet Right) Panning((Outlet left, Outlet right) channels, Outlet panning)
        {
            Channel = Left; Outlet  leftWithEffect  = Panning(channels.left,  panning);
            Channel = Right; Outlet rightWithEffect = Panning(channels.right, panning);

            return (leftWithEffect, rightWithEffect);
        }

        /// <inheritdoc cref="_panningdocs" />
        public (Outlet Left, Outlet Right) Panning((Outlet left, Outlet right) channels, double panning)
        {
            Channel = Left; Outlet  leftWithEffect  = Panning(channels.left,  panning);
            Channel = Right; Outlet rightWithEffect = Panning(channels.right, panning);

            return (leftWithEffect, rightWithEffect);
        }

        // Panbrello
        
        /// <inheritdoc cref="_panbrellodocs" />
        public Outlet Panbrello(Outlet signal, (Outlet speed, Outlet depth) panbrello)
        {
            panbrello.speed = panbrello.speed ?? _[1];
            panbrello.depth = panbrello.depth ?? _[1];

            // 0.5 is in the middle. 0 is left, 1 is right.
            var sine      = Sine(panbrello.depth, panbrello.speed); // [-1,+1]
            var halfSine  = Multiply(_[0.5], sine); // [-0.5,+0.5]
            var zeroToOne = Add(_[0.5], halfSine); // [0,1]

            return Panning(signal, zeroToOne);
        }
        
        /// <inheritdoc cref="_panbrellodocs" />
        public Outlet Panbrello(
            Outlet signal,
            (double speed, double depth) panbrello = default)
        {
            if (panbrello.speed == default) panbrello.speed = 1;
            if (panbrello.depth == default) panbrello.depth = 1;

            // 0.5 is in the middle. 0 is left, 1 is right.
            var halfSine  = Multiply(Sine(_[panbrello.speed]), _[panbrello.depth / 2]); // [-0.5,+0.5]
            var zeroToOne = Add(_[0.5], halfSine); // [0,1]

            return Panning(signal, zeroToOne);
        }

        /// <inheritdoc cref="_panbrellodocs" />
        public (Outlet Left, Outlet Right) Panbrello(
            (Outlet left, Outlet right) channels,
            (Outlet speed, Outlet depth) panbrello)
        {
            Channel = Left; Outlet  leftWithEffect  = Panbrello(channels.left,  panbrello);
            Channel = Right; Outlet rightWithEffect = Panbrello(channels.right, panbrello);
            
            return (leftWithEffect, rightWithEffect);
        }

        /// <inheritdoc cref="_panbrellodocs" />
        public (Outlet Left, Outlet Right) Panbrello(
            (Outlet left, Outlet right) channels,
            (double speed, double depth) panbrello = default)
        {
            Channel = Left; Outlet  leftWithEffect  = Panbrello(channels.left,  panbrello);
            Channel = Right; Outlet rightWithEffect = Panbrello(channels.right, panbrello);

            return (leftWithEffect, rightWithEffect);
        }

        // PitchPan
        
        /// <inheritdoc cref="_pitchpandocs" />
        public Outlet PitchPan(
            Outlet actualFrequency, Outlet centerFrequency,
            Outlet referenceFrequency, Outlet referencePanning)
        {
            // Defaults
            centerFrequency    = centerFrequency ?? _[A4];
            referenceFrequency = referenceFrequency ?? _[E4];
            referencePanning   = referencePanning ?? _[0.6];

            var centerPanning = _[0.5];

            // Calculate intervals relative to the center frequency
            var referenceInterval = Divide(referenceFrequency, centerFrequency);
            var actualInterval    = Divide(actualFrequency,    centerFrequency);

            var factor = Multiply(actualInterval, referenceInterval);

            // Calculate panning deviation
            //var newPanningDeviation = Multiply(Substract(referencePanning, centerPanning), factor);
            // AI's correction:
            var newPanningDeviation = Multiply(Substract(referencePanning, centerPanning), Substract(factor, _[1]));
            var newPanning          = Add(centerPanning, newPanningDeviation);

            return newPanning;
        }

        /// <inheritdoc cref="_pitchpandocs" />
        public double PitchPan(
            double actualFrequency, double centerFrequency,
            double referenceFrequency, double referencePanning)
        {
            // Defaults
            if (centerFrequency == default) centerFrequency       = A4;
            if (referenceFrequency == default) referenceFrequency = E4;
            if (referencePanning == default) referencePanning     = 0.6;

            double centerPanning = 0.5;

            // Calculate intervals relative to the center frequency
            double referenceInterval = referenceFrequency / centerFrequency;
            double actualInterval    = actualFrequency / centerFrequency;

            double factor = actualInterval * referenceInterval;

            // Calculate panning deviation
            //double newPanningDeviation = (referencePanning - centerPanning) * factor;
            // AI's correction:
            double newPanningDeviation = (referencePanning - centerPanning) * (factor - 1);
            double newPanning          = centerPanning + newPanningDeviation;

            return newPanning;
        }

        // ValueIndexer
        
        /// <inheritdoc cref="_valueindexerdocs" />
        public ValueIndexer _;

        /// <inheritdoc cref="_valueindexerdocs" />
        public class ValueIndexer
        {
            private readonly OperatorFactory _parent;

            /// <inheritdoc cref="_valueindexerdocs" />
            internal ValueIndexer(OperatorFactory parent)
            {
                _parent = parent;
            }

            /// <inheritdoc cref="_valueindexerdocs" />
            public ValueOperatorWrapper this[double value] => _parent.Value(value);
        }

        #region Docs

        #pragma warning disable CS0169 // Field is never used

        // ReSharper disable IdentifierTypo

        /// <summary>
        /// Generates a sine wave signal with the specified pitch.<br />
        /// Simpler variation on the one in the original OperatorFactory
        /// with pitch as the first and only parameter.
        /// </summary>
        /// <param name="pitch"> The frequency in Hz of the sine wave. </param>
        /// <returns> An <see cref="Outlet" /> representing the sine wave signal. </returns>
        object _sinedocs;

        /// <summary>
        /// Applies panning to a stereo signal by adjusting the left and right
        /// channel volumes based on the specified panning value.
        /// TODO: A variable panning might go into the negative. Should be clamped to 0-1.
        /// </summary>
        /// <param name="panning">
        /// An <see cref="Outlet" /> or <see cref="System.Double" />
        /// representing the panning value,
        /// where 0 is fully left, 1 is fully right, and 0.5 is centered.
        /// </param>
        /// <param name="channels">
        /// A tuple containing the left and right channels of the stereo signal.
        /// </param>
        /// <returns>
        /// A tuple containing the adjusted left and right channels
        /// after applying the panning.
        /// </returns>
        object _panningdocs;

        /// <summary>
        /// Applies a panbrello effect to a stereo signal by modulating the panning
        /// with a sine wave based on the specified speed and depth.
        /// </summary>
        /// <param name="channels">
        /// A tuple containing the left and right channels of the stereo signal.
        /// </param>
        /// <param name="panbrello">
        /// A tuple containing the speed and depth of the panbrello effect.
        /// If not provided, they will default to 1.
        /// </param>
        /// <returns>
        /// A tuple containing the adjusted left and right channels
        /// after applying the panbrello effect.
        /// </returns>
        object _panbrellodocs;

        /// <summary>
        /// Returns a panning based on the pitch,
        /// to spread different notes across a stereo field.
        /// (In other words: If the frequency is the referenceFrequency,
        /// then the panning is the referencePanning.
        /// Calculates the new panning for the supplied frequency by extrapolating.)
        /// </summary>
        /// <param name="actualFrequency">
        /// The frequency for which to calculate a panning value.
        /// </param>
        /// <param name="centerFrequency">
        /// The center frequency used as a reference point.
        /// Defaults to A4 if not provided.
        /// </param>
        /// <param name="referenceFrequency">
        /// The reference frequency to assign a specific panning value to.
        /// Defaults to E4 if not provided.
        /// </param>
        /// <param name="referencePanning">
        /// Panning value that the reference pitch would get.
        /// Defaults to 0.6 if not provided.
        /// </param>
        /// <returns> The adjusted panning value based on the pitch. </returns>
        object _pitchpandocs;

        /// <summary>
        /// Shorthand for OperatorFactor.Value(123), x.Value(123) or Value(123). Allows using _[123] instead.
        /// Literal numbers need to be wrapped inside a Value Operator so they can always be substituted by
        /// a whole formula / graph / calculation / curve over time.
        /// </summary>
        /// <returns>
        /// ValueOperatorWrapper also usable as Outlet or double.
        /// </returns>
        object _valueindexerdocs;

        #endregion
    }
}