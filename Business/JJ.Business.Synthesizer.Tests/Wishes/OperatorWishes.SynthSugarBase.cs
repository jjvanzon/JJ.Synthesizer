using System.Collections.Generic;
using System.Security.AccessControl;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Tests.Wishes.Notes;

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

        /// <summary>
        /// Applies panning to a stereo signal by adjusting the left and right
        /// channel volumes based on the specified panning value.
        /// </summary>
        /// <param name="channels">
        /// A tuple containing the left and right channels of the stereo signal.
        /// </param>
        /// <param name="panning">
        /// An <see cref="Outlet"/> representing the panning value,
        /// where 0 is fully left, 1 is fully right, and 0.5 is centered.
        /// </param>
        /// <returns>
        /// A tuple containing the adjusted left and right channels
        /// after applying the panning.
        /// </returns>
        public (Outlet left, Outlet right) Panning(
            (Outlet left, Outlet right) channels, 
            Outlet panning)
        {
            var leftPan = Multiply(channels.left, Substract(_[1], panning));
            var rightPan = Multiply(channels.right, panning);
            return (leftPan, rightPan);
        }

        /// <summary>
        /// Applies a panbrello effect to a stereo signal by modulating the panning
        /// with a sine wave based on the specified speed and depth.
        /// </summary>
        /// <param name="channels">
        /// A tuple containing the left and right channels of the stereo signal.
        /// </param>
        /// <param name="panbrello">
        /// A tuple containing the speed and depth of the panbrello effect.
        /// If not provided, default values will be used.
        /// </param>
        /// <returns>
        /// A tuple containing the adjusted left and right channels
        /// after applying the panbrello effect.
        /// </returns>
        public (Outlet left, Outlet right) Panbrello(
            (Outlet left, Outlet right) channels,
            (Outlet speed, Outlet depth) panbrello = default)
        {
            panbrello.speed = panbrello.speed ?? _[3];
            panbrello.depth = panbrello.depth ?? _[0.33];

            // 0.5 is in the middle. 0 is left, 1 is right.
            var sine = Sine(panbrello.depth, panbrello.speed); // [-1,+1]
            var halfSine = Multiply(_[0.5], sine); // [-0.5,+0.5]
            var zeroToOne = Add(_[0.5], halfSine); // [0,1]

            return Panning(channels, zeroToOne);
        }

        /// <summary>
        /// If the frequency is the referenceFrequency,
        /// then the new panning is the referencePanning.
        /// Calculates the new panning by extrapolating.
        /// </summary>
        /// <param name="actualFrequency"></param>
        /// <param name="centerFrequency"></param>
        /// <param name="referenceFrequency"></param>
        /// <param name="referencePanning"></param>
        /// <returns></returns>
        public Outlet PitchPan(
            Outlet actualFrequency, Outlet centerFrequency,
            Outlet referenceFrequency, Outlet referencePanning)
        {
            // Defaults
            centerFrequency = centerFrequency ?? _[A4];
            referenceFrequency = referenceFrequency ?? _[E4];
            referencePanning = referencePanning ?? _[0.6];

            Outlet centerPanning = _[0.5];

            // Calculate intervals relative to the center frequency
            Outlet referenceInterval = Divide(referenceFrequency, centerFrequency);
            Outlet actualInterval = Divide(actualFrequency, centerFrequency);

            Outlet factor = Multiply(actualInterval, referenceInterval);

            Outlet newPanningDeviation = Multiply(Substract(referencePanning, centerPanning), factor);
            // AI's correction:
            //Outlet newPanningDeviation = Multiply(Substract(referencePanning, centerPanning), Substract(factor, _[1]));
            Outlet newPanning = Add(centerPanning, newPanningDeviation);


            return newPanning;
        }

        //public (Outlet left, Outlet right) PitchPan(
        //    (Outlet left, Outlet right) channels, 
        //    (Outlet centerPitch, Outlet pitchOffset, Outlet panningOffset) pitchPan = default)
        //{
        //    // Defaults
        //    pitchPan.centerPitch = pitchPan.centerPitch ?? _[A4]; 
        //    pitchPan.pitchOffset = pitchPan.pitchOffset ?? _[E4]; 
        //    pitchPan.panningOffset = pitchPan.panningOffset ?? _[0.1];

        //    // Calculate panning, shifted in accordance to pitch
        //    var pitchModulation = Add(pitchPan.centerPitch, 
        //                              Multiply(pitchPan.pitchOffset, panningOffset ));


        //    //var pitchModulation = Add(pitchPan.centerPitch, Multiply(pitchPan.pitchOffset, Sine(_[1], _[0.5])));
        //    //var pannedChannels = Panning(channels, pitchPan.panningOffset);

        //    //return (Multiply(pannedChannels.left, pitchModulation), Multiply(pannedChannels.right, pitchModulation));
        //}

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