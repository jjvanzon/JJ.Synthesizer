using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable PossibleNullReferenceException

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class SynthesizerTests_Additive : SynthesizerSugarBase
    {
        [UsedImplicitly]
        public SynthesizerTests_Additive()
        { }

        public SynthesizerTests_Additive(IContext context)
            : base(context, beat: 0.45, bar: 4 * 0.45)
        { }

        #region Tests

        /// <inheritdoc cref="JitterDocs" />
        [TestMethod]
        public void Test_Synthesizer_Modulation_JitterBurstChord()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_Additive(context).Test_Modulation_JitterBurstChord();
        }

        /// <inheritdoc cref="JitterDocs" />
        private void Test_Modulation_JitterBurstChord()
            => SaveWav(MildEcho(JitterBurstChord), volume: 0.30, duration: 1 + MILD_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_Modulation_LongNoteComposition_DoesNotWork()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_Additive(context).Test_Modulation_LongNoteComposition_DoesNotWork();
        }

        private void Test_Modulation_LongNoteComposition_DoesNotWork()
            => SaveWav(MildEcho(LongNotesComposition_DoesNotWork()),
                       volume: 0.30,
                       duration: t[bar: 9, beat: 2] + MILD_ECHO_TIME);

        #endregion

        #region Composition

        /// <inheritdoc cref="JitterDocs" />
        private Outlet JitterBurstChord => Adder
        (
            Multiply(_[0.80], JitterNote(_[Notes.A4])),
            Multiply(_[0.70], JitterNote(_[Notes.B4])),
            Multiply(_[0.85], JitterNote(_[Notes.C5])),
            Multiply(_[0.75], JitterNote(_[Notes.D5])),
            Multiply(_[0.90], JitterNote(_[Notes.E5]))
        );

        private Outlet LongNotesComposition_DoesNotWork()
        {
            var detuneDepth = _[0.02];
            var vibratoDepth = _[0.005];
            var tremoloDepth = _[0.25];

            var melody = Adder
            (
                Multiply(_[0.80], LongModulatedNote(_[Notes.A4], detuneDepth, vibratoDepth, tremoloDepth)),
                Multiply(_[0.70], LongModulatedNote(_[Notes.B4], detuneDepth, vibratoDepth, tremoloDepth)),
                Multiply(_[0.85], LongModulatedNote(_[Notes.C5], detuneDepth, vibratoDepth, tremoloDepth)),
                Multiply(_[0.75], LongModulatedNote(_[Notes.D5], detuneDepth, vibratoDepth, tremoloDepth)),
                Multiply(_[0.90], LongModulatedNote(_[Notes.E5], detuneDepth, vibratoDepth, tremoloDepth))
            );

            return melody;
        }

        #endregion

        #region Instruments

        /// <inheritdoc cref="JitterDocs" />
        private Outlet JitterNote(Outlet freq, Outlet depthAdjust1 = null, Outlet depthAdjust2 = null)
        {
            var waveForm = SemiSaw(freq);
            var jittered = Jitter(waveForm, depthAdjust1, depthAdjust2);
            var sound = Multiply(jittered, CurveIn(VolumeCurve));
            return sound;
        }

        private Outlet LongModulatedNote(Outlet freq, Outlet detuneDepth, Outlet vibratoDepth, Outlet tremoloDepth)
        {
            // Base additive synthesis with harmonic content
            var harmonicContent = Adder
            (
                Sine(_[1], freq),
                Sine(_[0.5], Multiply(freq, _[2])),
                Sine(_[0.3], Multiply(freq, _[3])),
                Sine(_[0.2], Multiply(freq, _[4]))
            );

            // Apply detune by modulating frequencies slightly
            var detunedContent = Adder
            (
                Sine(_[1], Multiply(freq, Add(_[1], detuneDepth))),
                Sine(_[1], Multiply(freq, Add(_[2], detuneDepth))),
                Sine(_[1], Multiply(freq, Add(_[3], detuneDepth))),
                Sine(_[1], Multiply(freq, Add(_[4], detuneDepth)))
            );

            // Apply vibrato by modulating frequency over time using an oscillator
            var vibrato = Sine(Add(_[1], vibratoDepth), _[5.5]); // 5.5 Hz vibrato
            var soundWithVibrato = Multiply(harmonicContent, vibrato);

            // Apply tremolo by modulating amplitude over time using an oscillator
            var tremolo = Sine(Add(_[1], tremoloDepth), _[4]); // 4 Hz tremolo
            var soundWithTremolo = Multiply(soundWithVibrato, tremolo);

            // Stretch and apply modulation over time
            var noteWithEnvelope = Multiply(soundWithTremolo, CurveIn(VolumeCurve));

            return noteWithEnvelope;
        }

        #endregion

        #region Algorithms

        /// <inheritdoc cref="JitterDocs" />
        private Outlet Jitter(Outlet sound, Outlet depthAdjust1, Outlet depthAdjust2)
        {
            depthAdjust1 = depthAdjust1 ?? _[0.005];
            depthAdjust2 = depthAdjust2 ?? _[0.250];

            var tremolo1 = Sine(Add(_[1], depthAdjust1), _[5.5]); // 5.5 Hz tremolo
            var soundWithTremolo1 = Multiply(sound, tremolo1);
            var tremolo2 = Sine(Add(_[1], depthAdjust2), _[4]); // 4 Hz tremolo
            var soundWithTremolo2 = Multiply(soundWithTremolo1, tremolo2);
            return soundWithTremolo2;
        }

        /// <summary>
        /// Generates a mild sawtooth-like waveform by combining multiple sine waves with different frequencies.
        /// </summary>
        /// <param name="freq"> The base frequency for the waveform. </param>
        /// <returns> An <see cref="Outlet" /> representing the semi-sawtooth waveform. </returns>
        private Outlet SemiSaw(Outlet freq) => Adder
        (
            Sine(_[1.0], freq),
            Sine(_[0.5], Multiply(freq, _[2])),
            Sine(_[0.3], Multiply(freq, _[3])),
            Sine(_[0.2], Multiply(freq, _[4]))
        );

        private const double MILD_ECHO_TIME = 0.33 * 5;

        /// <summary>
        /// Applies a mild echo effect to the given sound.
        /// </summary>
        /// <param name="sound"> The original sound to which the echo effect will be applied. </param>
        /// <returns> An <see cref="Outlet" /> representing the sound with the applied echo effect. </returns>
        private Outlet MildEcho(Outlet sound)
            => EntityFactory.CreateEcho(this, sound, count: 6, denominator: 4, delay: 0.33);

        #endregion

        #region Curves

        private Curve VolumeCurve => CurveFactory.CreateCurve(
            "   o                 ",
            " o   o               ",
            "                     ",
            "           o         ",
            "o                   o");

        #endregion

        #region Docs

        /// <summary>
        /// Applies a jitter effect to notes, with adjustable depths. Basically with an extreme double tremolo effect.
        /// </summary>
        /// <param name="sound"> The sound to apply the jitter effect to. </param>
        /// <param name="freq"> The frequency of the note. </param>
        /// <param name="depthAdjust1"> The first depth adjustment for the jitter effect. Defaults to 0.005 if not provided. </param>
        /// <param name="depthAdjust2"> The second depth adjustment for the jitter effect. Defaults to 0.250 if not provided. </param>
        /// <returns> An <see cref="Outlet" /> representing the jittered note. </returns>
        [UsedImplicitly]
        private Outlet JitterDocs(Outlet freq, Outlet depthAdjust1 = null, Outlet depthAdjust2 = null)
            => throw new NotSupportedException();

        #endregion
    }
}