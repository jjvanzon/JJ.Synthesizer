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
    public class Synthesizer_ModulationTests : SynthesizerSugarBase
    {
        [UsedImplicitly]
        public Synthesizer_ModulationTests()
        { }

        public Synthesizer_ModulationTests(IContext context)
            : base(context, beat: 0.5, bar: 2)
        { }

        #region Tests

        /// <inheritdoc cref="VibraphaseDocs" />
        [TestMethod]
        public void Test_Synthesizer_Modulation_Vibraphase_Chord()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_ModulationTests(context).Test_Modulation_Vibraphase_Chord();
        }

        /// <inheritdoc cref="VibraphaseDocs" />
        private void Test_Modulation_Vibraphase_Chord()
            => SaveWav(MildEcho(VibraphaseChord), volume: 0.30, duration: 1 + MILD_ECHO_TIME);

        /// <inheritdoc cref="VibraphaseDocs" />
        [TestMethod]
        public void Test_Synthesizer_Modulation_Vibraphase()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_ModulationTests(context).Test_Modulation_Vibraphase();
        }

        /// <inheritdoc cref="VibraphaseDocs" />
        private void Test_Modulation_Vibraphase()
            => SaveWav(MildEcho(Vibraphase(freq: _[Notes.E5])), duration: 1 + MILD_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_Modulation_Detunica_Jingle()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_ModulationTests(context).Test_Modulation_Detunica_Jingle();
        }

        private void Test_Modulation_Detunica_Jingle()
            => SaveWav(DeepEcho(DetunicaJingle), volume: 0.04, duration: 13 + DEEP_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_Modulation_Detunica1()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_ModulationTests(context).Test_Modulation_Detunica1();
        }
        
        private void Test_Modulation_Detunica1()
            => SaveWav(DeepEcho(Detunica1(freq: _[Notes.A3])), 1 + DEEP_ECHO_TIME, volume: 0.1);

        [TestMethod]
        public void Test_Synthesizer_Modulation_Detunica2()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_ModulationTests(context).Test_Modulation_Detunica2();
        }

        private void Test_Modulation_Detunica2()
            => SaveWav(DeepEcho(Detunica2(freq: _[Notes.B4])), 1 + DEEP_ECHO_TIME, volume: 0.1);

        [TestMethod]
        public void Test_Synthesizer_Modulation_Detunica3()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_ModulationTests(context).Test_Modulation_Detunica3();
        }

        private void Test_Modulation_Detunica3()
            => SaveWav(DeepEcho(Detunica3(freq: _[Notes.C5])), 1 + DEEP_ECHO_TIME, volume: 0.1);

        [TestMethod]
        public void Test_Synthesizer_Modulation_Detunica4()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_ModulationTests(context).Test_Modulation_Detunica4();
        }

        private void Test_Modulation_Detunica4()
            => SaveWav(DeepEcho(Detunica4(freq: _[Notes.D5])), 1 + DEEP_ECHO_TIME, volume: 0.1);

        [TestMethod]
        public void Test_Synthesizer_Modulation_Detunica5()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_ModulationTests(context).Test_Modulation_Detunica5();
        }

        private void Test_Modulation_Detunica5()
            => SaveWav(DeepEcho(Detunica5(freq: _[Notes.E5])), 1 + DEEP_ECHO_TIME, volume: 0.1);

        #endregion

        #region Jingles

        /// <inheritdoc cref="VibraphaseDocs" />
        private Outlet VibraphaseChord => Adder
        (
            Vibraphase(freq: _[Notes.A4], volume: _[0.80]),
            Vibraphase(freq: _[Notes.B4], volume: _[0.70]),
            Vibraphase(freq: _[Notes.C5], volume: _[0.85]),
            Vibraphase(freq: _[Notes.D5], volume: _[0.75]),
            Vibraphase(freq: _[Notes.E5], volume: _[0.90])
        );

        private Outlet DetunicaJingle => Adder
        (
            Detunica1(Bar[1], _[Notes.A3], _[0.80], duration: _[6]),
            Detunica2(Bar[2], _[Notes.B4], _[0.70], duration: _[2]),
            Detunica3(Bar[3], _[Notes.C5], _[0.85], duration: _[3]),
            Detunica4(Bar[4], _[Notes.D5], _[0.75], duration: _[3]),
            Detunica5(Bar[5], _[Notes.E5], _[0.90], duration: _[5])
        );

        /// <inheritdoc cref="Detunica" />
        private Outlet Detunica1(Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null) => Detunica(
            delay, freq, volume, duration,
            vibratoDepth: _[0.005], tremoloDepth: _[0.25], detuneDepth: Multiply(CurveIn(DetuneCurve1), _[0.03]));

        /// <inheritdoc cref="Detunica" />
        private Outlet Detunica2(Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null) => Detunica(
            delay, freq, volume, duration, 
            vibratoDepth: _[0.005], tremoloDepth: _[0.25], detuneDepth: Multiply(CurveIn(DetuneCurve2), _[0.10]));

        /// <inheritdoc cref="Detunica" />
        private Outlet Detunica3(Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null) => Detunica(
            delay, freq, volume, duration,
            vibratoDepth: _[0.005], tremoloDepth: _[0.25], detuneDepth: Multiply(CurveIn(DetuneCurve3), _[0.02]));

        /// <inheritdoc cref="Detunica" />
        private Outlet Detunica4(Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null) => Detunica(
            delay, freq, volume, duration,
            vibratoDepth: _[0.005], tremoloDepth: _[0.25], detuneDepth: Multiply(CurveIn(DetuneCurve2), _[0.03]));

        private Outlet Detunica5(Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null) => Detunica(
            delay, freq, volume, duration,
            vibratoDepth: _[0.005], tremoloDepth: _[0.25], detuneDepth: Multiply(CurveIn(DetuneCurve1), _[0.001]));

        #endregion

        #region Instruments

        /// <inheritdoc cref="VibraphaseDocs" />
        private Outlet Vibraphase(
            Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null,
            Outlet depthAdjust1 = null, Outlet depthAdjust2 = null)
        {
            var waveForm = SemiSaw(freq);
            var jittered = Jitter(waveForm, depthAdjust1, depthAdjust2);
            var sound = Multiply(jittered, StretchCurve(VibraphaseVolumeCurve, duration));
            var note = StrikeNote(sound, delay, volume);
            return note;
        }

        /// <inheritdoc cref="DocComments.Default" />
        private Outlet Detunica(
            Outlet delay = null, Outlet freq = null, Outlet volume = null, Outlet duration = null,
            Outlet vibratoDepth = null, Outlet tremoloDepth = null, Outlet detuneDepth = null)
        {
            duration = duration ?? _[1];
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
            sound = Multiply(sound, StretchCurve(DetunicaVolumeCurve, duration));

            // Apply velocity and delay
            var note = StrikeNote(sound, delay, volume);

            return note;
        }

        #endregion

        #region WaveForms

        /// <inheritdoc cref="SemiSawDocs" />
        private Outlet SemiSaw(Outlet freq)
        {
            freq = freq ?? _[440];

            return Adder
            (
                Sine(_[1.0], freq),
                Sine(_[0.5], Multiply(freq, _[2])),
                Sine(_[0.3], Multiply(freq, _[3])),
                Sine(_[0.2], Multiply(freq, _[4]))
            );
        }

        /// <inheritdoc cref="DetunedHarmonicsDocs" />
        private Outlet DetunedHarmonics(Outlet freq, Outlet harmonicDetuneDepth = null)
        {
            freq = freq ?? _[440];
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

        /// <inheritdoc cref="VibraphaseDocs" />
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

        /// <inheritdoc cref="MildEchoDocs" />
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

        private Curve VibraphaseVolumeCurve => CurveFactory.CreateCurve(@"

                o                   
              o   o                 
                                    
                        o           
             o                   o  ");

        private Curve DetunicaVolumeCurve => CurveFactory.CreateCurve(@"

                          o                             
               o      o       o                         
                                                        
                   o                o                   
             o                                       o  ");

        private Curve DetuneCurve1 => CurveFactory.CreateCurve(@"

                         o          
                                    
                                    
             o                   o  ");

        private Curve DetuneCurve2 => CurveFactory.CreateCurve(@"

                  o                 
                                    
                                    
             o                   o  ");

        private Curve DetuneCurve3 => CurveFactory.CreateCurve(@"

                       o            
                                    
                                    
             o                   o  ");

        #endregion

        #region Docs

        /// <summary>
        /// Applies a jitter effect to notes, with adjustable depths.
        /// Basically with an extreme double tremolo effect, that goes into the negative.
        /// </summary>
        /// <param name="sound"> The sound to apply the jitter effect to. </param>
        /// <param name="depthAdjust1"> The first depth adjustment for the jitter effect. Defaults to 0.005 if not provided. </param>
        /// <param name="depthAdjust2"> The second depth adjustment for the jitter effect. Defaults to 0.250 if not provided. </param>
        /// <inheritdoc cref="DocComments.Default" />
        [UsedImplicitly]
        private Outlet VibraphaseDocs(
            Outlet sound, Outlet freq, Outlet volume, Outlet delay, Outlet duration,
            Outlet depthAdjust1, Outlet depthAdjust2)
            => throw new NotSupportedException();

        /// <summary>
        /// Generates a mild sawtooth-like waveform by combining multiple sine waves with different frequencies.
        /// </summary>
        /// <returns> An <see cref="Outlet" /> representing the semi-sawtooth waveform. </returns>
        /// <inheritdoc cref="DocComments.Default" />
        [UsedImplicitly]
        private Outlet SemiSawDocs(Outlet freq) => throw new NotSupportedException();

        /// <summary> Generates a detuned harmonic sound by altering the frequencies slightly. </summary>
        /// <param name="harmonicDetuneDepth">
        /// The depth of the detuning applied to the harmonics.
        /// If not provided, a default value is used.
        /// </param>
        /// <inheritdoc cref="DocComments.Default" />
        [UsedImplicitly]
        private Outlet DetunedHarmonicsDocs(Outlet freq, Outlet harmonicDetuneDepth = null)
            => throw new NotSupportedException();

        /// <summary>
        /// Applies a mild echo effect to the given sound.
        /// </summary>
        /// <param name="sound"> The original sound to which the echo effect will be applied. </param>
        /// <returns> An <see cref="Outlet" /> representing the sound with the applied echo effect. </returns>
        [UsedImplicitly]
        private Outlet MildEchoDocs(Outlet sound) => throw new NotSupportedException();

        #endregion
    }
}