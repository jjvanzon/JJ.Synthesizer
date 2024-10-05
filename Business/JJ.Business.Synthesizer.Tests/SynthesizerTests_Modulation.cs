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
            : base(context, beat: 0.5, bar: 2.0)
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
        public void Test_Synthesizer_Modulation_DetuneJingle()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_Additive(context).Test_Modulation_DetuneJingle();
        }

        private void Test_Modulation_DetuneJingle()
            => SaveWav(DeepEcho(DetuneJingle()), volume: 0.04, duration: 13 + DEEP_ECHO_TIME);

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

        private Outlet DetuneJingle() => Adder
        (
            DetunedNote1(_[0]),
            DetunedNote2(_[2]),
            DetunedNote3(_[4]),
            DetunedNote4(_[6]),
            DetunedNote5(_[8])
        );

        private Outlet DetunedNote1(Outlet delay) =>
            DetunedNote(
                _[Notes.A3], delay, volume: _[0.80], duration: _[6],
                vibratoDepth: _[0.005], tremoloDepth: _[0.25], Multiply(CurveIn(DetuneCurve1), _[0.03]));

        private Outlet DetunedNote2(Outlet delay) =>
            DetunedNote(
                _[Notes.B4], delay, volume: _[0.70], duration: _[2],
                vibratoDepth: _[0.005], tremoloDepth: _[0.25], Multiply(CurveIn(DetuneCurve2), _[0.10]));

        private Outlet DetunedNote3(Outlet delay) =>
            DetunedNote(
                _[Notes.C5], delay, volume: _[0.85], duration: _[3],
                vibratoDepth: _[0.005], tremoloDepth: _[0.25], Multiply(CurveIn(DetuneCurve3), _[0.02]));

        private Outlet DetunedNote4(Outlet delay) =>
            DetunedNote(
                _[Notes.D5], delay, volume: _[0.75], duration: _[3],
                vibratoDepth: _[0.005], tremoloDepth: _[0.25], Multiply(CurveIn(DetuneCurve2), _[0.03]));

        private Outlet DetunedNote5(Outlet delay) =>
            DetunedNote(
                _[Notes.E5], delay, volume: _[0.90], duration: _[5],
                vibratoDepth: _[0.005], tremoloDepth: _[0.25], Multiply(CurveIn(DetuneCurve1), _[0.001]));

        #endregion

        #region Instruments

        /// <inheritdoc cref="JitterDocs" />
        private Outlet JitterNote(Outlet freq, Outlet depthAdjust1 = null, Outlet depthAdjust2 = null)
        {
            var waveForm = SemiSaw(freq);
            var jittered = Jitter(waveForm, depthAdjust1, depthAdjust2);
            var sound = Multiply(jittered, CurveIn(JitterNoteVolumeCurve));
            return sound;
        }

        private Outlet DetunedNote(
            Outlet freq = null, Outlet delay = null, Outlet volume = null, Outlet duration = null,
            Outlet vibratoDepth = null, Outlet tremoloDepth = null, Outlet detuneDepth = null)
        {
            vibratoDepth = vibratoDepth ?? _[0.005];
            tremoloDepth = tremoloDepth ?? _[0.25];

            // Base additive synthesis waveform
            var semiSaw = SemiSaw(freq);

            // Apply detune by modulating harmonic frequencies slightly
            var stretchedDetune = TimeMultiply(detuneDepth, duration);
            var detunedHarmonics = DetunedHarmonics(freq, stretchedDetune);

            // Mix them together
            Outlet sound = Add(semiSaw, detunedHarmonics);
            
            /*
            // Apply vibrato by speeding up and slowing down and oscillator over time.
            var vibratoOscillator = Sine(Add(_[1], vibratoDepth), _[5.5]); // 5.5 Hz vibrato
            sound = TimeMultiply(sound, vibratoOscillator);
            */
            
            // Apply tremolo by modulating amplitude over time using an oscillator
            /*
            var tremoloOscillator = Substract(_[1], Sine(tremoloDepth, _[4])); // 4 Hz tremolo
            sound = Multiply(sound, tremoloOscillator);
            */
            
            // Apply volume curve
            sound = Multiply(sound, StretchCurve(VolumeCurve, duration));

            // Apply velocity and delay
            var note = StrikeNote(sound, delay, volume);

            return note;
        }

        #endregion

        #region WaveForms

        /// <inheritdoc cref="SemiSawDocs"/>
        private Outlet SemiSaw(Outlet freq) => Adder
        (
            Sine(_[1.0], freq),
            Sine(_[0.5], Multiply(freq, _[2])),
            Sine(_[0.3], Multiply(freq, _[3])),
            Sine(_[0.2], Multiply(freq, _[4]))
        );

        /// <inheritdoc cref="DetunedHarmonicsDocs"/>
        private Outlet DetunedHarmonics(Outlet freq, Outlet harmonicDetuneDepth = null)
        {
            harmonicDetuneDepth = harmonicDetuneDepth ?? _[0.02];

            var sound = Adder
            (
                Sine(_[1], Multiply(freq, Add(_[1], harmonicDetuneDepth))),
                Sine(_[1], Multiply(freq, Add(_[2], harmonicDetuneDepth))),
                Sine(_[1], Multiply(freq, Add(_[3], harmonicDetuneDepth))),
                Sine(_[1], Multiply(freq, Add(_[4], harmonicDetuneDepth)))
            );
            return sound;
        }

        #endregion

        #region Effects

        /// <inheritdoc cref="JitterDocs" />
        private Outlet Jitter(Outlet sound, Outlet depthAdjust1 = null, Outlet depthAdjust2 = null)
        {
            depthAdjust1 = depthAdjust1 ?? _[0.005];
            depthAdjust2 = depthAdjust2 ?? _[0.250];

            var tremoloOscillator1 = Sine(Add(_[1], depthAdjust1), _[5.5]); // 5.5 Hz tremolo
            sound = Multiply(sound, tremoloOscillator1);
            var tremoloOscillator2 = Sine(Add(_[1], depthAdjust2), _[4]); // 4 Hz tremolo
            sound = Multiply(sound, tremoloOscillator2);
            
            return sound;
        }

        private const double MILD_ECHO_TIME = 0.33 * 5;

        /// <inheritdoc cref="EchoDocs"/>
        private Outlet MildEcho(Outlet sound) =>
            EntityFactory.CreateEcho(this, sound, count: 6, denominator: 4, delay: 0.33);


        private const double DEEP_ECHO_TIME = 0.5 * 5;

        /// <summary> Applies a deep echo effect to the specified sound. </summary>
        /// <param name="melody"> The original sound to which the echo effect will be applied. </param>
        /// <returns> An <see cref="Outlet" /> representing the sound with the deep echo effect applied. </returns>
        private Outlet DeepEcho(Outlet melody)
            => EntityFactory.CreateEcho(this, melody, count: 6, denominator: 2, delay: 0.5);

        #endregion

        #region Curves

        private Curve JitterNoteVolumeCurve => CurveFactory.CreateCurve(
            "   o                 ",
            " o   o               ",
            "                     ",
            "           o         ",
            "o                   o");

        private Curve VolumeCurve => CurveFactory.CreateCurve(
            "             o                           ",
            "  o      o       o                       ",
            "                                         ",
            "      o                o                 ",
            "o                                       o");

        private Curve DetuneCurve1 => CurveFactory.CreateCurve(
            "            o        ",
            "                     ",
            "                     ",
            "o                   o");

        private Curve DetuneCurve2 => CurveFactory.CreateCurve(
            "     o               ",
            "                     ",
            "                     ",
            "o                   o");

        private Curve DetuneCurve3 => CurveFactory.CreateCurve(
            "          o          ",
            "                     ",
            "                     ",
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

        /// <summary>
        /// Generates a mild sawtooth-like waveform by combining multiple sine waves with different frequencies.
        /// </summary>
        /// <param name="freq"> The base frequency for the waveform. </param>
        /// <returns> An <see cref="Outlet" /> representing the semi-sawtooth waveform. </returns>
        [UsedImplicitly]
        private Outlet SemiSawDocs(Outlet freq) => throw new NotSupportedException();

        /// <summary> Generates a detuned harmonic sound by altering the frequencies slightly. </summary>
        /// <param name="freq"> The base frequency for the harmonics. </param>
        /// <param name="harmonicDetuneDepth">
        /// The depth of the detuning applied to the harmonics.
        /// If not provided, a default value is used.
        /// </param>
        /// <returns> An <see cref="Outlet" /> instance representing the sound with the detuned harmonics. </returns>
        [UsedImplicitly]
        private Outlet DetunedHarmonicsDocs(Outlet freq, Outlet harmonicDetuneDepth = null) 
            => throw new NotSupportedException();

        /// <summary>
        /// Applies a mild echo effect to the given sound.
        /// </summary>
        /// <param name="sound"> The original sound to which the echo effect will be applied. </param>
        /// <returns> An <see cref="Outlet" /> representing the sound with the applied echo effect. </returns>
        [UsedImplicitly]
        private Outlet EchoDocs(Outlet sound) => throw new NotSupportedException();

        #endregion
    }
}